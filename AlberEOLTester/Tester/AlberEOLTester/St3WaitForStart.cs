using Alber.Eol.Hardware;
using AlberEOL.Base;
using AlberEOL.CustomClasses;
using AlberEOL.Devices;
using AlberEOL.Exceptions;
using AlberEOL.Properties;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TraceabilityHandler;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;
using Product = AlberEOL.Base.Product;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        public void WakeUp()
        {
            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.SetVoltage), "42.0");
                CPX.Command(CPX.GetCommand(Cpx400Function.SetCurrentLimit), "1.0");
                // Switch on Power Supply
                CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "1");
                Thread.Sleep(100);
            }
            // Switch on Charge Relay
            CommInterface.RelayStatus = RelayStatus.Charge;
            Thread.Sleep(4000);
            CommInterface.RelayStatus = RelayStatus.AllOFF;

            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "0");
            }
        }

        private void WaitForStart()
        {
            // THE PACK IS CONNECTED AND READY TO TEST, CHECK PACK STATUS BEFORE START
            #region statecontrol
            // Állapotot váltunk
            State = StationState.WaitForStart;

            DetectingTesterExceptions = new List<TesterError>
            {
                TesterError.ERR_APPCLOSE,
                TesterError.ERR_EMERGENCY
            };

            TesterSuperVise();
            #endregion

            mreDone.Reset();
            mreBack.Reset();
            TaskMessage = null;

            Operation = "Termékállapot ellenőrzés";

            int tries = 4;
            bool needWakeUp = false;
            for (int i = 1; i <= tries; i++)
            {
                try
                {
                    Subcommands.MANU_DATA.Read();
                    Subcommands.DASTATUS1.Read();
                    Subcommands.DASTATUS2.Read();
                    Subcommands.DASTATUS6.Read();
                    ReadDirectRamCustomBlocks();
                }
                catch (IOException ex)
                {
                    if (i < tries-1)
                    {
                        continue;
                    }
                    else if (i==tries-1)
                    {
                        needWakeUp = MessageBox.Show("Lehet, hogy a termék SHUTDOWN mode-ban van, élesszük fel?", "Megerősítés!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;

                        if (needWakeUp)
                        {
                            WakeUp();
                            Thread.Sleep(500);
                            continue;
                        }
                    }

                    throw new DeviceException("Kommunikációs hiba, ellenőrizze a csatlakozást!", ex);
                }
                break;
            }

            string serialNumber = ((VtepMANU_DATA)Subcommands.MANU_DATA).SerialNumber.ToString();

            // Create new Product
            Product tempProduct = new Product(serialNumber);

            if (tempProduct.TraceProduct == null)
            {
                ErrorCode = tempProduct.ErrorCode;
                Message = tempProduct.Message;
                State = StationState.AfterActions;
                return;
            }

            tempProduct.RefreshProductState(Settings.Default.EOLOpTraceName, Tester.ProductTypeID);

            if (!tempProduct.ProductState.OK)
            {
                ErrorCode = tempProduct.ErrorCode;
                Message = tempProduct.Message;
                State = StationState.AfterActions;
                return;
            }
            Product = tempProduct;

            Operation = "Indításra vár";
            Message = new GeneralMessage("Tesztelés indítására vár");
            TaskMessage = new GeneralMessage("Kérem indítsa el a tesztet a START gomb megnyomásával!");

            // Wait for Done/Start or Back Button to continue
            IsDoneButtonEnabled = true;
            IsBackButtonEnabled = true;
            WaitHandle[] waitHandles = { mreDone, mreBack };
            var waitHandle = WaitHandle.WaitAny(waitHandles);
            IsDoneButtonEnabled = false;
            IsBackButtonEnabled = false;
            if (waitHandle == 1)
            {
                BeforeActionsTask();
            }
        }
    }
}
