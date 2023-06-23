using AlberEOL.CustomClasses;
using AlberEOL.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
namespace AlberEOL.Base
{
    #region Station interface

    /// <summary>
    /// Az összes állomásnál közös tulajdonságok, eljárások
    /// </summary>
    public interface IStation
    {
        int StationIndex { get; set; }
        StationName StationName { get; set; }
        bool Enabled { get; set; }
        StationState State { get; set; }
        string Operation { get; set; }
        GeneralMessage Message { get; set; }
        GeneralMessage TaskMessage { get; set; }
        IProduct Product { get; set; }
        ErrorCode ErrorCode { get; set; }
        List<TesterError> DetectingTesterExceptions { get; set; }
        Thread StateMachineThread { get; set; }
        Tester Tester { get; set; }
        void ManageTasks();
        TesterException GenerateTesterException(TesterError Source, string detail);
        void Start();
        void Stop();
        void TesterSuperVise();
        event PropertyChangedEventHandler PropertyChanged;
    }
    #endregion

    #region Enumerators

    /// <summary>
    /// Az állapotgépek lehetséges állapotai
    /// </summary>
    public enum StationState
    {
        Nothing = 0,
        Initialize,
        BeforeActions,
        WaitForStart,
        Working,
        AfterActions,
        Finished,
        Reset,
        Error,
        Manual = 99
    }
    #endregion

    public abstract class StationBase : INotifyPropertyChanged, IStation
    {
        #region IStation implementation - properties
        /// <summary>
        /// Az állomás indexe
        /// </summary>
        public int StationIndex { get; set; }

        /// <summary>
        /// Station name
        /// </summary>
        public StationName StationName { get; set; }

        /// <summary>
        /// Engedélyezett-e?
        /// </summary>
        public bool Enabled { get; set; }

        private StationState _state;
        /// <summary>
        /// Aktuális állapot
        /// </summary>
        public StationState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _operation;
        /// <summary>
        /// Az aktuális állapoton belüli operáció
        /// </summary>
        public string Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
                OnPropertyChanged();
            }
        }

        private GeneralMessage _message;
        /// <summary>
        /// Station Message
        /// </summary>
        public GeneralMessage Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        private GeneralMessage _taskMessage;
        /// <summary>
        /// TaskMessage to Operator
        /// </summary>
        public GeneralMessage TaskMessage
        {
            get
            {
                return _taskMessage;
            }
            set
            {
                if (_taskMessage != value)
                {
                    _taskMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        private IProduct _product;
        /// <summary>
        /// The Product under test
        /// </summary>
        public IProduct Product
        {
            get
            {
                return _product;
            }
            set
            {
                if (_product != value)
                {
                    _product = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Error code handling
        /// </summary>
        private ErrorCode _errorCode;
        public ErrorCode ErrorCode
        {
            get => _errorCode;
            set
            {
                if (_errorCode != value)
                {
                    _errorCode = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// A figyelt interlock
        /// </summary>
        public List<TesterError> DetectingTesterExceptions { get; set; }

        public Thread StateMachineThread { get; set; }

        public Tester Tester { get; set; }
        #endregion

        #region Custom Properties
        public ManualResetEvent mreDone = new ManualResetEvent(false);
        public ManualResetEvent mreBack = new ManualResetEvent(false);

        public List<TesterError> StoredTesterExceptions { get; set; }

        private TestDetail _lastTestDetail;
        public TestDetail LastTestDetail
        {
            get
            {
                return _lastTestDetail;
            }
            set
            {
                _lastTestDetail = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Állapotgép változók
        public Ini Ini;

        public BitVector32 DetectedErrorBits;

        public DateTime StartTime, EndTime;

        public int DetectingErrors;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stationName">The Name of the Station</param>
        /// <param name="stationIndex">The Index of the Station</param>
        /// <param name="enabled">enabled or disabled</param>
        public StationBase(StationName stationName, int stationIndex, bool enabled)
        {
            this.StationIndex = stationIndex;
            this.StationName = stationName;
            this.Enabled = enabled;
            this.State = StationState.Nothing;
        }
        #endregion

        #region Start / Stop
        /// <summary>
        /// Start Station
        /// </summary>
        public virtual void Start()
        {
            // Állapotgép elindítása
            StateMachineThread = new Thread(new ThreadStart(ManageTasks))
            {
                Name = "ManageTask_" + StationName.ToString()
            };
            StateMachineThread.Start();
        }

        /// <summary>
        /// Stop Station
        /// </summary>
        public virtual void Stop()
        {
            StateMachineThread.Abort();
            StateMachineThread = null;
        }
        #endregion

        public abstract void ManageTasks();
        public abstract void TesterSuperVise();

        #region Tester errors
        public TesterException GenerateTesterException(TesterError Source, string detail)
        {
            TesterException ie = new TesterException(detail)
            {
                Source = Source.ToString()
            };
            ie.Data.Add("Detail", detail);

            return ie;
        }
        #endregion

        #region Events
        public virtual event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
        }

        protected virtual void OnPropertyChanged(params string[] propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
        #endregion
    }
}
