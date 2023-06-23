using AlberEOL.Properties;
using AlberEOL.Station;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using TraceabilityHandler;
using static AlberEOL.UI.MainScreen;
using Logger = AlberEOL.CustomClasses.Logger;

namespace AlberEOL.Base
{
    public class Tester : INotifyPropertyChanged
    {
        #region Tester
        public List<StationBase> Stations;
        public StationBase AlberEOLStation { get; set; }

        // User handling
        private UserState _user;
        public UserState User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public UserState AdminUser { get; set; }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            private set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        private OperationMode _OperationMode;
        public OperationMode OperationMode
        {
            get
            {
                return _OperationMode;
            }
            set
            {
                _OperationMode = value;
                OnPropertyChanged(nameof(OperationMode));
            }
        }

        public BitVector32 Errors = new BitVector32(0);

        private int _ProductTypeID;
        public int ProductTypeID
        {
            get
            {
                return _ProductTypeID;
            }
            set
            {
                _ProductTypeID = value;
                OnPropertyChanged(nameof(ProductTypeID));
            }
        }

        public bool stop;
        readonly Thread TesterCheckerThread;
        #endregion

        #region Constructor
        public Tester()
        {
            ProductTypeID = Settings.Default.LastProductTypeID;
            API.BaseUrl = Settings.Default.TraceBaseURL;

            TesterCheckerThread = new Thread(new ThreadStart(CheckingTesterExceptions))
            {
                Name = "TesterChecker"
            };
            TesterCheckerThread.Start();
            Message = "TesterCheckerThread started";
        }

        #endregion

        #region Start / Stop
        /// <summary>
        /// Teszter indítása
        /// </summary>
        public void StartTester(string SoftwareType)
        {
            switch (LauncherOptions.SoftwareType)
            {
                case "ALBER":
                    AlberEOLStation = new AlberEOLStation(StationName.AlberEOLStation, this);
                    break;
            }
            OperationMode = OperationMode.StandAlone;
            Logger.WriteGeneralLog("Állomások előkészítése", "Tester");
            Message = "Állomások előkészítése...";
            // Az állomásokat egy listába tesszük a könnyebb kezelhetőség érdekében
            Stations = new List<StationBase>
            {
                AlberEOLStation
            };
        }

        /// <summary>
        /// Teszter leállítása
        /// </summary>
        public void StopTester()
        {
            Logger.WriteGeneralLog("Állomások leállítása", "Tester");
            Message = "Állomások leállítása...";
            StopStations();
            Stations = null;
            TesterCheckerThread.Abort();
        }

        /// <summary>
        /// Állomások indítása
        /// </summary>
        public void StartStations()
        {
            foreach (StationBase Station in Stations)
            {
                Station.Start();
                Message = $"{Station.StationName} állomás indítása";
                Station.PropertyChanged += Station_PropertyChanged;
            }
        }

        /// <summary>
        /// Állomások leállítása
        /// </summary>
        public void StopStations()
        {
            stop = true;
            Errors[Err.ERR_APPCLOSE] = true;

            foreach (StationBase Station in Stations)
            {
                // Leiratkozás az állomás eseményeiről
                Message = $"{Station.StationName} állomás leállítása";
                Station.PropertyChanged -= Station_PropertyChanged;
                Station.StateMachineThread.Abort();
            }
        }
        #endregion

        #region Errors

        public void SetManualMode(bool manual)
        {
            if (manual)
            {
                OperationMode = OperationMode.Manual;
                Errors[Err.ERR_MANUAL] = true;
                Message = DateTime.Now + "Manuális üzemmód";
            }
            else
            {
                OperationMode = OperationMode.StandAlone;
                Errors[Err.ERR_MANUAL] = false;
                Message = DateTime.Now + "Automata üzemmód";
            }
        }

        private void CheckingTesterExceptions()
        {
            do
            {
                Thread.Sleep(100);

                if (OperationMode == OperationMode.StandAlone)
                {
                    ////// 24V FIGYELÉSE
                    //diHandler2.Read();
                    //if (diHandler2.GetChannel(DI.Channel2Name.EMERGENCY) == false)
                    //{
                    //    if (Errors[Err.ERR_EMERGENCY] != true) _Errors[Err.ERR_EMERGENCY] = true;
                    //    StateError = "EMERGENCY";
                    //}
                    //else
                    //{
                    //    if (Errors[Err.ERR_EMERGENCY] != false) _Errors[Err.ERR_EMERGENCY] = false;
                    //    StateError = "001000";
                    //}
                }
            } while (stop == false);
        }
        #endregion

        #region Station Events
        private void Station_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StationBase Station = (StationBase)sender;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
