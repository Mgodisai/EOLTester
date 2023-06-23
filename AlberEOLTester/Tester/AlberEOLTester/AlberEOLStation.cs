using Alber.Eol.Hardware;
using AlberEOL.Base;
using AlberEOL.CustomClasses;
using AlberEOL.Devices;
using AlberEOL.Exceptions;
using AlberEOL.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;
using Logger = AlberEOL.CustomClasses.Logger;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        public BIM BIM { get; private set; }
        public Cpx400sp CPX { get; private set; }
        public CustomZebraPrinter CZP { get; private set; }

        #region BMS
        public CommInterface CommInterface { get; private set; }
        public BQ76942_769142_76952 BMS { get; private set; }
        public DirectRam DirectRam { get; private set; }
        public DataMemory DataMemory { get; private set; }
        public Subcommands Subcommands { get; private set; }
        #endregion

        private bool _isDoneButtonEnabled;
        public bool IsDoneButtonEnabled
        {
            get
            {
                return _isDoneButtonEnabled;
            }
            set
            {
                if (_isDoneButtonEnabled != value)
                {
                    _isDoneButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isBackButtonEnabled;
        public bool IsBackButtonEnabled
        {
            get
            {
                return _isBackButtonEnabled;
            }
            set
            {
                if (_isBackButtonEnabled != value)
                {
                    _isBackButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public byte[] PrevDataMemory { get; set; }
        public bool[] MemDiffs { get; set; }

        readonly TOTimer CustomTimer = new TOTimer();

        #region Constructor
        public AlberEOLStation(StationName stationName, Tester tester) : base(stationName, 0, true)
        {
            // Teszter
            Tester = tester;
            Ini = new Ini(Directory.GetCurrentDirectory() + "\\" + Settings.Default.IniFileName);
            CZP = new CustomZebraPrinter();
            DetectingErrors = Err.ERR_APPCLOSE;
            StoredTesterExceptions = new List<TesterError>();
            CPX = new Cpx400sp();
            CommInterface = new CommInterface();
            BIM = new BIM();
            BMS = CommInterface.BMS;
            DirectRam = BMS.DirectRam;
            Subcommands = BMS.Subcommands;
            Subcommands.MANU_DATA = new VtepMANU_DATA(Subcommands);
            DataMemory = BMS.DataMemory;
            IsDoneButtonEnabled = false;
            IsBackButtonEnabled = false;
        }
        #endregion

        #region State machine
        /// <summary>
        /// Állapotgép
        /// </summary>
        public override void ManageTasks()
        {
            bool stop = false;
            do
            {
                try
                {
                    TesterSuperVise();

                    #region NormalFlow
                    switch (State)
                    {
                        case StationState.Nothing:
                        case StationState.Reset:
                        case StationState.Manual:
                        case StationState.Error:
                            Initialize();
                            break;
                        case StationState.Initialize:
                            BeforeActionsTask();
                            break;
                        case StationState.BeforeActions:
                            WaitForStart();
                            break;
                        case StationState.WaitForStart:
                            Test();
                            break;
                        case StationState.Working:
                            AfterActionsTask();
                            break;
                        case StationState.AfterActions:
                            TestFinishedTask();
                            break;
                        case StationState.Finished:
                            BeforeActionsTask();
                            break;
                    }
                    #endregion
                }
                catch (TesterException ex)
                {
                    CommInterface.RelayStatus = RelayStatus.AllOFF;
                    if (ex.Message == "ERR_APPCLOSE")
                    {
                        stop = true;
                    }
                    else
                    {
                        ErrorCode = new ErrorCode("TESTER", ex.Message);
                        Logger.WriteErrorLog(ex.Message, "TesterException", null);
                        ErrorTask();
                    }
                }
                catch (StationException ex) // Állomást érintő hiba
                {
                    CommInterface.RelayStatus = RelayStatus.AllOFF;
                    ErrorCode = new ErrorCode("STATION", ex.Message);
                    Logger.WriteErrorLog(ex.Message, "StationException", null);
                    ErrorTask();
                }
                catch (DeviceException ex) // Eszközt érintő hiba
                {
                    CommInterface.RelayStatus = RelayStatus.AllOFF;
                    ErrorCode = new ErrorCode("DEVICE", ex.Message);
                    Logger.WriteErrorLog(ex.Message, "DeviceException", null);
                    ErrorTask();
                }
                catch (Exception ex) // Általános hibakezelés minden másra
                {
                    CommInterface.RelayStatus = RelayStatus.AllOFF;
                    ErrorCode = new ErrorCode("EXCEPTION", ex.Message);
                    Logger.WriteErrorLog(ex.Message, "Exception", null);
                    ErrorTask();
                }
                finally // Ha van valami ami mindig le kell hogy fusson
                {
                    CommInterface.RelayStatus = RelayStatus.AllOFF;
                }
            }
            while (!stop);
        }
        #endregion

        #region Tester ERRORS
        public override void TesterSuperVise()
        {
            DetectedErrorBits = new BitVector32(Tester.Errors);

            do
            {
                if (DetectedErrorBits[Err.ERR_APPCLOSE] && DetectingTesterExceptions.Contains(TesterError.ERR_APPCLOSE))
                {
                    if (!StoredTesterExceptions.Contains(TesterError.ERR_APPCLOSE))
                    {
                        StoredTesterExceptions.Add(TesterError.ERR_APPCLOSE);
                    }
                    throw GenerateTesterException(TesterError.ERR_APPCLOSE, "ERR_APPCLOSE");
                }

                if (DetectedErrorBits[Err.ERR_EMERGENCY] && DetectingTesterExceptions.Contains(TesterError.ERR_EMERGENCY))
                {
                    if (!StoredTesterExceptions.Contains(TesterError.ERR_EMERGENCY))
                    {
                        StoredTesterExceptions.Add(TesterError.ERR_EMERGENCY);
                    }
                    throw GenerateTesterException(TesterError.ERR_EMERGENCY, "ERR_EMERGENCY");
                }

            } while ((Tester.Errors.Data & DetectingErrors) != 0);
        }
        #endregion
    }
}
