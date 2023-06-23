using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public class Subcommands : INotifyPropertyChanged, ISubcommandExecutor
    {
        private readonly BQ76942_769142_76952 BMS;

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DEVICE_NUMBER DEVICE_NUMBER { get; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MANUFACTURING_STATUS MANUFACTURING_STATUS { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public MANU_DATA MANU_DATA { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DASTATUS1 DASTATUS1 { get; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DASTATUS2 DASTATUS2 { get; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DASTATUS3 DASTATUS3 { get; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DASTATUS6 DASTATUS6 { get; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public CB_ACTIVE_CELLS CB_ACTIVE_CELLS { get; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public OTP_WR_CHECK OTP_WR_CHECK { get; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public OTP_WR OTP_WR { get; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public READ_CAL1 READ_CAL1 { get; }

        public SHUTDOWN SHUTDOWN { get; }

        public RESET RESET { get; }

        public FUSE_TOGGLE FUSE_TOGGLE { get; }

        public PF_ENABLE PF_ENABLE { get; }

        public RESET_PASSQ RESET_PASSQ { get; }

        public SET_CFGUPDATE SET_CFGUPDATE { get; }

        public EXIT_CFGUPDATE EXIT_CFGUPDATE { get; }

        public DSG_PDSG_OFF DSG_PDSG_OFF { get; }

        public ALL_FETS_OFF ALL_FETS_OFF { get; }

        public ALL_FETS_ON ALL_FETS_ON { get; }

        public SLEEP_ENABLE SLEEP_ENABLE { get; }

        public SLEEP_DISABLE SLEEP_DISABLE { get; }

        public SWAP_COMM_MODE SWAP_COMM_MODE { get; }

        public Subcommands(BQ76942_769142_76952 bms)
        {
            BMS = bms;

            DEVICE_NUMBER = new DEVICE_NUMBER(this);

            MANUFACTURING_STATUS = new MANUFACTURING_STATUS(this);
            MANU_DATA = new MANU_DATA(this);

            DASTATUS1 = new DASTATUS1(this);
            DASTATUS2 = new DASTATUS2(this);
            DASTATUS3 = new DASTATUS3(this);
            DASTATUS6 = new DASTATUS6(this);

            CB_ACTIVE_CELLS = new CB_ACTIVE_CELLS(this);

            OTP_WR_CHECK = new OTP_WR_CHECK(this);
            OTP_WR = new OTP_WR(this);
            READ_CAL1 = new READ_CAL1(this);

            //VoidSubcommands
            SHUTDOWN = new SHUTDOWN(this);

            RESET = new RESET(this);

            FUSE_TOGGLE = new FUSE_TOGGLE(this);

            PF_ENABLE = new PF_ENABLE(this);

            RESET_PASSQ = new RESET_PASSQ(this);

            SET_CFGUPDATE = new SET_CFGUPDATE(this);
            EXIT_CFGUPDATE = new EXIT_CFGUPDATE(this);

            DSG_PDSG_OFF = new DSG_PDSG_OFF(this);

            ALL_FETS_OFF = new ALL_FETS_OFF(this);
            ALL_FETS_ON = new ALL_FETS_ON(this);
            SLEEP_ENABLE = new SLEEP_ENABLE(this);
            SLEEP_DISABLE = new SLEEP_DISABLE(this);
            SWAP_COMM_MODE = new SWAP_COMM_MODE(this);
        }

        public void Send(ushort code)
        {
            BMS.DirectRam.WriteSubcommand(code);
            OnPropertyChanged("");
        }

        public void Read(ushort code, byte[] outBuffer)
        {
            byte[] commandBytes = BMS.DirectRam.WriteSubcommand(code);
            byte[] buffer = BMS.DirectRam.ReadBuffer(commandBytes, (byte)outBuffer.Length, false);
            Array.Copy(buffer, outBuffer, outBuffer.Length);
            OnPropertyChanged("");
        }

        public void Write(ushort code, byte[] buffer)
        {
            BMS.DirectRam.WriteBuffer(BitConverter.GetBytes(code), buffer);
            OnPropertyChanged("");
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (BMS.SendPropertyChanged && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
