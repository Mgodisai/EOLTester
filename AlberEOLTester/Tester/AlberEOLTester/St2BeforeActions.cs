using AlberEOL.Base;
using AlberEOL.CustomClasses;
using AlberEOL.Devices;
using System.Collections.Generic;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        private void BeforeActionsTask()
        {
            // BMS RESET, POWER SUPPLY RESET, CONNECT PACK TO THE PINS
            #region statecontrol
            // Állapotot váltunk
            State = StationState.BeforeActions;

            DetectingTesterExceptions = new List<TesterError>
            {
                TesterError.ERR_APPCLOSE,
                TesterError.ERR_EMERGENCY
            };

            TesterSuperVise();
            #endregion

            mreDone.Reset();

            ResetStationVariables();

            Message = new GeneralMessage("Teszt előkészítése");
            Operation = "Előkészítési fázis";

            //Tápegység reset
            string CPXBaseVoltage = Ini.IniReadValue("DEVICES", "CPX.BaseVoltage");
            string CPXBaseCurrentLimit = Ini.IniReadValue("DEVICES", "CPX.BaseCurrentLimit");

            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "0");
                CPX.Command(CPX.GetCommand(Cpx400Function.Reset));
                CPX.Command(CPX.GetCommand(Cpx400Function.SetVoltage), CPXBaseVoltage);
                CPX.Command(CPX.GetCommand(Cpx400Function.SetCurrentLimit), CPXBaseCurrentLimit);
            }

            TaskMessage = new GeneralMessage("Kérem csatlakoztassa a tesztelni kívánt terméket a megfelelő pontokra!");
            TaskMessage = new GeneralMessage("Ha végzett, kérem nyugtázza!");

            // Done/Start megnyomására vár a folytatáshoz
            IsDoneButtonEnabled = true;
            mreDone.WaitOne();
            IsDoneButtonEnabled = false;
        }
    }
}
