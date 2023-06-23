using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VTEP.Arrays;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public class DataMemory : INotifyPropertyChanged
    {
        public class Block
        {
            private KeyValuePair<ushort, byte> kvp;

            public ushort Address => kvp.Key;

            public DataMemoryRegister Register => (DataMemoryRegister)Address;

            public byte Size => kvp.Value;

            public Block(ushort address, byte size)
            {
                kvp = new KeyValuePair<ushort, byte>(address, size);
            }

            public Block(DataMemoryRegister register, byte size) 
                : this((ushort)register, size) { }
        }

        public const ushort START = 0x9180;
        public const ushort SIZE = 0x01CA;
        public const ushort END = START + SIZE;
        public const byte BLOCK_SIZE = 0x20;

        private int GetIndex(ushort address) => address - START;

        private readonly BQ76942_769142_76952 BMS;

        #region RamRead
        private byte[] ramRead;

        [Browsable(false)]
        public byte[] RamRead => ramRead.Clone() as byte[];

        internal bool GetBit(ushort address, byte bit) => ArrayMapper.GetBit(ramRead, GetIndex(address), bit);

        internal byte GetBits(ushort address, byte startBit, byte width) => ArrayMapper.GetBits(ramRead, GetIndex(address), startBit, width);

        internal byte GetByte(ushort address) => ramRead[GetIndex(address)];

        internal sbyte GetSbyte(ushort address) => ArrayMapper.GetSbyte(ramRead, GetIndex(address));

        internal ushort GetUshort(ushort address) => ArrayMapper.GetUshort(ramRead, GetIndex(address));

        internal short GetShort(ushort address) => ArrayMapper.GetShort(ramRead, GetIndex(address));

        internal float GetFloat(ushort address) => ArrayMapper.GetFloat(ramRead, GetIndex(address));
        #endregion

        #region RamWrite
        private byte[] ramWrite;

        [Browsable(false)]
        public byte[] RamWrite => ramWrite.Clone() as byte[];

        public void CopyReadToWrite()
        {
            Array.Copy(ramRead, ramWrite, SIZE);
            OnPropertyChanged(nameof(RamWrite));
        }

        public void ImportImage(byte[] image)
        {
            if (image.Length != SIZE)
            {
                throw new ArgumentException(string.Format("Invalid DataMemory image. Size: {0}, expected: {1}", image.Length, SIZE));
            }
            Array.Copy(image, ramWrite, SIZE);
            OnPropertyChanged(nameof(RamWrite));
        }

        internal void SetBit(ushort address, byte bit, bool value)
        {
            ArrayMapper.SetBit(ramWrite, GetIndex(address), bit, value);
            OnPropertyChanged(nameof(RamWrite));
        }

        internal void SetBits(ushort address, byte startBit, byte width, byte value)
        {
            ArrayMapper.SetBits(ramWrite, GetIndex(address), startBit, width, value);
            OnPropertyChanged(nameof(RamWrite));
        }

        internal void SetByte(ushort address, byte data)
        {
            ramWrite[GetIndex(address)] = data;
            OnPropertyChanged(nameof(RamWrite));
        }

        internal void SetSbyte(ushort address, sbyte value)
        {
            ArrayMapper.SetSbyte(ramWrite, GetIndex(address), value);
            OnPropertyChanged(nameof(RamWrite));
        }

        internal void SetUshort(ushort address, ushort value)
        {
            ArrayMapper.SetUshort(ramWrite, GetIndex(address), value);
            OnPropertyChanged(nameof(RamWrite));
        }

        internal void SetShort(ushort address, short value)
        {
            ArrayMapper.SetShort(ramWrite, GetIndex(address), value);
            OnPropertyChanged(nameof(RamWrite));
        }

        internal void SetFloat(ushort address, float value)
        {
            ArrayMapper.SetFloat(ramWrite, GetIndex(address), value);
            OnPropertyChanged(nameof(RamWrite));
        }

        //public void ImportImage(byte[] image, bool[] writeMask)
        //{
        //    if (image.Length != SIZE)
        //    {
        //        throw new ArgumentException(string.Format("Invalid DataMemory image. Size: {0}, expected: {1}", image.Length, SIZE));
        //    }
        //    Array.Copy(image, ramRead, SIZE);
        //    OnPropertyChanged(nameof(RamRead));
        //}

        //internal void SetBit(ushort address, byte bit, bool value)
        //{
        //    ArrayMapper.SetBit(ramRead, GetIndex(address), bit, value);
        //    OnPropertyChanged(nameof(RamRead));
        //}

        //internal void SetByte(ushort address, byte data)
        //{
        //    ramRead[GetIndex(address)] = data;
        //    OnPropertyChanged(nameof(RamRead));
        //}

        //internal void SetSbyte(ushort address, sbyte value)
        //{
        //    ArrayMapper.SetSbyte(ramRead, GetIndex(address), value);
        //    OnPropertyChanged(nameof(RamRead));
        //}

        //internal void SetUshort(ushort address, ushort value)
        //{
        //    ArrayMapper.SetUshort(ramRead, GetIndex(address), value);
        //    OnPropertyChanged(nameof(RamRead));
        //}

        //internal void SetShort(ushort address, short value)
        //{
        //    ArrayMapper.SetShort(ramRead, GetIndex(address), value);
        //    OnPropertyChanged(nameof(RamRead));
        //}

        //internal void SetFloat(ushort address, float value)
        //{
        //    ArrayMapper.SetFloat(ramRead, GetIndex(address), value);
        //    OnPropertyChanged(nameof(RamRead));
        //}
        #endregion

        internal void ResetRams()
        {
            // TODO clear or new?
            ramRead = new byte[SIZE];
            ramWrite = new byte[SIZE];
            OnPropertyChanged(nameof(RamRead));
            OnPropertyChanged(nameof(RamWrite));
        }

        [Browsable(false)]
        public bool[] RegistersToWrite
        {
            get
            {
                bool[] rtw = new bool[SIZE];
                for (int i = 0; i < rtw.Length; i++)
                {
                    rtw[i] = ramRead[i] != ramWrite[i];
                }
                return rtw;
            }
        }

        #region Properties
        [Category("Calibration"), DisplayName("Voltage/Cell 1 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_1_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 2 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_2_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 3 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_3_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 4 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_4_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 5 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_5_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 6 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_6_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 7 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_7_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 8 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_8_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 9 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_9_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 10 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_10_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 11 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_11_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 12 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_12_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 13 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_13_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 14 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_14_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 15 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_15_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Cell 16 Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__Cell_16_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/Pack Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Calibration__Voltage__Pack_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/TOS Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Calibration__Voltage__TOS_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/LD Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Calibration__Voltage__LD_Gain { get; }

        [Category("Calibration"), DisplayName("Voltage/ADC Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Voltage__ADC_Gain { get; }

        [Category("Calibration"), DisplayName("Current/CC Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Current__CC_Gain Calibration__Current__CC_Gain { get; }

        [Category("Calibration"), DisplayName("Current/Capacity Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Current__Capacity_Gain Calibration__Current__Capacity_Gain { get; }

        [Category("Calibration"), DisplayName("Vcell Offset/Vcell Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Vcell_Offset__Vcell_Offset { get; }

        [Category("Calibration"), DisplayName("V Divider Offset/Vdiv Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__V_Divider_Offset__Vdiv_Offset { get; }

        [Category("Calibration"), DisplayName("Current Offset/Coulomb Counter Offset Samples"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Calibration__Current_Offset__Coulomb_Counter_Offset_Samples { get; }

        [Category("Calibration"), DisplayName("Current Offset/Board Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Current_Offset__Board_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/Internal Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__Internal_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/CFETOFF Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__CFETOFF_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/DFETOFF Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__DFETOFF_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/ALERT Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__ALERT_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/TS1 Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__TS1_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/TS2 Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__TS2_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/TS3 Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__TS3_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/HDQ Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__HDQ_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/DCHG Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__DCHG_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("Temperature/DDSG Temp Offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Calibration__Temperature__DDSG_Temp_Offset { get; }

        [Category("Calibration"), DisplayName("CUV/CUV Threshold Override"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Calibration__CUV__CUV_Threshold_Override { get; }

        [Category("Calibration"), DisplayName("COV/COV Threshold Override"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Calibration__COV__COV_Threshold_Override { get; }

        [Category("System Data"), DisplayName("Integrity/Config RAM Signature"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory System_Data__Integrity__Config_RAM_Signature { get; }

        [Category("Calibration"), DisplayName("Internal Temp Model/Int Gain"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Internal_Temp_Model__Int_Gain { get; }

        [Category("Calibration"), DisplayName("Internal Temp Model/Int base offset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Internal_Temp_Model__Int_base_offset { get; }

        [Category("Calibration"), DisplayName("Internal Temp Model/Int Maximum AD"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Internal_Temp_Model__Int_Maximum_AD { get; }

        [Category("Calibration"), DisplayName("Internal Temp Model/Int Maximum Temp"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Internal_Temp_Model__Int_Maximum_Temp { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Coeff a1"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Coeff_a1 { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Coeff a2"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Coeff_a2 { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Coeff a3"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Coeff_a3 { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Coeff a4"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Coeff_a4 { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Coeff a5"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Coeff_a5 { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Coeff b1"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Coeff_b1 { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Coeff b2"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Coeff_b2 { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Coeff b3"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Coeff_b3 { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Coeff b4"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Coeff_b4 { get; }

        [Category("Calibration"), DisplayName("18K Temperature Model/Adc0"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__18K_Temperature_Model__Adc0 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Coeff a1"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Coeff_a1 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Coeff a2"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Coeff_a2 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Coeff a3"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Coeff_a3 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Coeff a4"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Coeff_a4 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Coeff a5"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Coeff_a5 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Coeff b1"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Coeff_b1 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Coeff b2"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Coeff_b2 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Coeff b3"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Coeff_b3 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Coeff b4"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Coeff_b4 { get; }

        [Category("Calibration"), DisplayName("180K Temperature Model/Adc0"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__180K_Temperature_Model__Adc0 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Coeff a1"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Coeff_a1 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Coeff a2"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Coeff_a2 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Coeff a3"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Coeff_a3 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Coeff a4"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Coeff_a4 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Coeff a5"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Coeff_a5 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Coeff b1"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Coeff_b1 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Coeff b2"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Coeff_b2 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Coeff b3"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Coeff_b3 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Coeff b4"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Coeff_b4 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Rc0"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Rc0 { get; }

        [Category("Calibration"), DisplayName("Custom Temperature Model/Adc0"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Calibration__Custom_Temperature_Model__Adc0 { get; }

        [Category("Calibration"), DisplayName("Current Deadband/Coulomb Counter Deadband"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Calibration__Current_Deadband__Coulomb_Counter_Deadband { get; }

        [Category("Settings"), DisplayName("Fuse/Min Blow Fuse Voltage"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Fuse__Min_Blow_Fuse_Voltage { get; }

        [Category("Settings"), DisplayName("Fuse/Fuse Blow Timeout"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Fuse__Fuse_Blow_Timeout { get; }

        [Category("Settings"), DisplayName("Configuration/Power Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit16DataMemory Settings__Configuration__Power_Config { get; }

        [Category("Settings"), DisplayName("Configuration/REG12 Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public REG12_Config Settings__Configuration__REG12_Config { get; }

        [Category("Settings"), DisplayName("Configuration/REG0 Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public REG0_Config Settings__Configuration__REG0_Config { get; }

        [Category("Settings"), DisplayName("Configuration/HWD Regulator Options"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__HWD_Regulator_Options { get; }

        [Category("Settings"), DisplayName("Configuration/Comm Type"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Comm_Type Settings__Configuration__Comm_Type { get; }

        [Category("Settings"), DisplayName("Configuration/I2C Address"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Configuration__I2C_Address { get; }

        [Category("Settings"), DisplayName("Configuration/SPI Configuration"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SPI_Configuration Settings__Configuration__SPI_Configuration { get; }

        [Category("Settings"), DisplayName("Configuration/Comm Idle Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Configuration__Comm_Idle_Time { get; }

        [Category("Power"), DisplayName("Shutdown/Shutdown Cell Voltage"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Power__Shutdown__Shutdown_Cell_Voltage { get; }

        [Category("Power"), DisplayName("Shutdown/Shutdown Stack Voltage"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Power__Shutdown__Shutdown_Stack_Voltage { get; }

        [Category("Power"), DisplayName("Shutdown/Low V Shutdown Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Power__Shutdown__Low_V_Shutdown_Delay { get; }

        [Category("Power"), DisplayName("Shutdown/Shutdown Temperature"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Power__Shutdown__Shutdown_Temperature { get; }

        [Category("Power"), DisplayName("Shutdown/Shutdown Temperature Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Power__Shutdown__Shutdown_Temperature_Delay { get; }

        [Category("Power"), DisplayName("Sleep/Sleep Current"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Power__Sleep__Sleep_Current { get; }

        [Category("Power"), DisplayName("Sleep/Voltage Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Power__Sleep__Voltage_Time { get; }

        [Category("Power"), DisplayName("Sleep/Wake Comparator Current"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Power__Sleep__Wake_Comparator_Current { get; }

        [Category("Power"), DisplayName("Sleep/Sleep Hysteresis Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Power__Sleep__Sleep_Hysteresis_Time { get; }

        [Category("Power"), DisplayName("Sleep/Sleep Charger Voltage Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Power__Sleep__Sleep_Charger_Voltage_Threshold { get; }

        [Category("Power"), DisplayName("Sleep/Sleep Charger PACK-TOS Delta"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Power__Sleep__Sleep_Charger_PACK_TOS_Delta { get; }

        [Category("Power"), DisplayName("Shutdown/FET Off Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Power__Shutdown__FET_Off_Delay { get; }

        [Category("Power"), DisplayName("Shutdown/Shutdown Command Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Power__Shutdown__Shutdown_Command_Delay { get; }

        [Category("Power"), DisplayName("Shutdown/Auto Shutdown Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Power__Shutdown__Auto_Shutdown_Time { get; }

        [Category("Power"), DisplayName("Shutdown/RAM Fail Shutdown Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Power__Shutdown__RAM_Fail_Shutdown_Time { get; }

        [Category("Security"), DisplayName("Settings/Security Settings"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Security__Settings__Security_Settings { get; }

        [Category("Security"), DisplayName("Keys/Unseal Key Step 1"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Security__Keys__Unseal_Key_Step_1 { get; }

        [Category("Security"), DisplayName("Keys/Unseal Key Step 2"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Security__Keys__Unseal_Key_Step_2 { get; }

        [Category("Security"), DisplayName("Keys/Full Access Key Step 1"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Security__Keys__Full_Access_Key_Step_1 { get; }

        [Category("Security"), DisplayName("Keys/Full Access Key Step 2"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Security__Keys__Full_Access_Key_Step_2 { get; }

        [Category("Settings"), DisplayName("Protection/Protection Configuration"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Protection_Configuration Settings__Protection__Protection_Configuration { get; }

        [Category("Settings"), DisplayName("Protection/Enabled Protections A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Protection__Enabled_Protections_A { get; }

        [Category("Settings"), DisplayName("Protection/Enabled Protections B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Protection__Enabled_Protections_B { get; }

        [Category("Settings"), DisplayName("Protection/Enabled Protections C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Protection__Enabled_Protections_C { get; }

        [Category("Settings"), DisplayName("Protection/CHG FET Protections A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Protection__CHG_FET_Protections_A { get; }

        [Category("Settings"), DisplayName("Protection/CHG FET Protections B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Protection__CHG_FET_Protections_B { get; }

        [Category("Settings"), DisplayName("Protection/CHG FET Protections C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Protection__CHG_FET_Protections_C { get; }

        [Category("Settings"), DisplayName("Protection/DSG FET Protections A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Protection__DSG_FET_Protections_A { get; }

        [Category("Settings"), DisplayName("Protection/DSG FET Protections B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Protection__DSG_FET_Protections_B { get; }

        [Category("Settings"), DisplayName("Protection/DSG FET Protections C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Protection__DSG_FET_Protections_C { get; }

        [Category("Settings"), DisplayName("Alarm/Default Alarm Mask"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit16DataMemory Settings__Alarm__Default_Alarm_Mask { get; }

        [Category("Settings"), DisplayName("Alarm/SF Alert Mask A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Alarm__SF_Alert_Mask_A { get; }

        [Category("Settings"), DisplayName("Alarm/SF Alert Mask B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Alarm__SF_Alert_Mask_B { get; }

        [Category("Settings"), DisplayName("Alarm/SF Alert Mask C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Alarm__SF_Alert_Mask_C { get; }

        [Category("Settings"), DisplayName("Protection/Body Diode Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Protection__Body_Diode_Threshold { get; }

        [Category("Protections"), DisplayName("CUV/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public CellThresholdDataMemory Protections__CUV__Threshold { get; }

        [Category("Protections"), DisplayName("CUV/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Protections__CUV__Delay { get; }

        [Category("Protections"), DisplayName("COV/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public CellThresholdDataMemory Protections__COV__Threshold { get; }

        [Category("Protections"), DisplayName("COV/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Protections__COV__Delay { get; }

        [Category("Protections"), DisplayName("CUV/Recovery Hysteresis"), TypeConverter(typeof(ExpandableObjectConverter))]
        public CellThresholdDataMemory Protections__CUV__Recovery_Hysteresis { get; }

        [Category("Protections"), DisplayName("COV/Recovery Hysteresis"), TypeConverter(typeof(ExpandableObjectConverter))]
        public CellThresholdDataMemory Protections__COV__Recovery_Hysteresis { get; }

        [Category("Protections"), DisplayName("COVL/Latch Limit"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__COVL__Latch_Limit { get; }

        [Category("Protections"), DisplayName("COVL/Counter Dec Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__COVL__Counter_Dec_Delay { get; }

        [Category("Protections"), DisplayName("COVL/Recovery Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__COVL__Recovery_Time { get; }

        [Category("Protections"), DisplayName("OCC/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCC__Threshold { get; }

        [Category("Protections"), DisplayName("OCC/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCC__Delay { get; }

        [Category("Protections"), DisplayName("OCD1/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCD1__Threshold { get; }

        [Category("Protections"), DisplayName("OCD1/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCD1__Delay { get; }

        [Category("Protections"), DisplayName("OCD2/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCD2__Threshold { get; }

        [Category("Protections"), DisplayName("OCD2/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCD2__Delay { get; }

        [Category("Protections"), DisplayName("SCD/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__SCD__Threshold { get; }

        [Category("Protections"), DisplayName("SCD/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__SCD__Delay { get; }

        [Category("Protections"), DisplayName("OCC/Recovery Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Protections__OCC__Recovery_Threshold { get; }

        [Category("Protections"), DisplayName("OCD3/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Protections__OCD3__Threshold { get; }

        [Category("Protections"), DisplayName("OCD3/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCD3__Delay { get; }

        [Category("Protections"), DisplayName("OCD/Recovery Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Protections__OCD__Recovery_Threshold { get; }

        [Category("Protections"), DisplayName("OCDL/Latch Limit"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCDL__Latch_Limit { get; }

        [Category("Protections"), DisplayName("OCDL/Counter Dec Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCDL__Counter_Dec_Delay { get; }

        [Category("Protections"), DisplayName("OCDL/Recovery Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OCDL__Recovery_Time { get; }

        [Category("Protections"), DisplayName("OCDL/Recovery Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Protections__OCDL__Recovery_Threshold { get; }

        [Category("Protections"), DisplayName("SCD/Recovery Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__SCD__Recovery_Time { get; }

        [Category("Protections"), DisplayName("SCDL/Latch Limit"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__SCDL__Latch_Limit { get; }

        [Category("Protections"), DisplayName("SCDL/Counter Dec Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__SCDL__Counter_Dec_Delay { get; }

        [Category("Protections"), DisplayName("SCDL/Recovery Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__SCDL__Recovery_Time { get; }

        [Category("Protections"), DisplayName("SCDL/Recovery Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Protections__SCDL__Recovery_Threshold { get; }

        [Category("Protections"), DisplayName("OTC/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__OTC__Threshold { get; }

        [Category("Protections"), DisplayName("OTC/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OTC__Delay { get; }

        [Category("Protections"), DisplayName("OTC/Recovery"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__OTC__Recovery { get; }

        [Category("Protections"), DisplayName("OTD/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__OTD__Threshold { get; }

        [Category("Protections"), DisplayName("OTD/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OTD__Delay { get; }

        [Category("Protections"), DisplayName("OTD/Recovery"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__OTD__Recovery { get; }

        [Category("Protections"), DisplayName("OTF/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OTF__Threshold { get; }

        [Category("Protections"), DisplayName("OTF/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OTF__Delay { get; }

        [Category("Protections"), DisplayName("OTF/Recovery"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OTF__Recovery { get; }

        [Category("Protections"), DisplayName("OTINT/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__OTINT__Threshold { get; }

        [Category("Protections"), DisplayName("OTINT/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__OTINT__Delay { get; }

        [Category("Protections"), DisplayName("OTINT/Recovery"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__OTINT__Recovery { get; }

        [Category("Protections"), DisplayName("UTC/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__UTC__Threshold { get; }

        [Category("Protections"), DisplayName("UTC/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__UTC__Delay { get; }

        [Category("Protections"), DisplayName("UTC/Recovery"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__UTC__Recovery { get; }

        [Category("Protections"), DisplayName("UTD/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__UTD__Threshold { get; }

        [Category("Protections"), DisplayName("UTD/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__UTD__Delay { get; }

        [Category("Protections"), DisplayName("UTD/Recovery"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__UTD__Recovery { get; }

        [Category("Protections"), DisplayName("UTINT/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__UTINT__Threshold { get; }

        [Category("Protections"), DisplayName("UTINT/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__UTINT__Delay { get; }

        [Category("Protections"), DisplayName("UTINT/Recovery"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Protections__UTINT__Recovery { get; }

        [Category("Protections"), DisplayName("Recovery/Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__Recovery__Time { get; }

        [Category("Protections"), DisplayName("OCC/PACK-TOS Delta"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Protections__OCC__PACK_TOS_Delta { get; }

        [Category("Protections"), DisplayName("HWD/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Protections__HWD__Delay { get; }

        [Category("Protections"), DisplayName("Load Detect/Active Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__Load_Detect__Active_Time { get; }

        [Category("Protections"), DisplayName("Load Detect/Retry Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Protections__Load_Detect__Retry_Delay { get; }

        [Category("Protections"), DisplayName("Load Detect/Timeout"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Protections__Load_Detect__Timeout { get; }

        [Category("Protections"), DisplayName("PTO/Charge Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Protections__PTO__Charge_Threshold { get; }

        [Category("Protections"), DisplayName("PTO/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Protections__PTO__Delay { get; }

        [Category("Protections"), DisplayName("PTO/Reset"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Protections__PTO__Reset { get; }

        [Category("Settings"), DisplayName("Permanent Failure/Enabled PF A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Permanent_Failure__Enabled_PF_A { get; }

        [Category("Settings"), DisplayName("Permanent Failure/Enabled PF B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Permanent_Failure__Enabled_PF_B { get; }

        [Category("Settings"), DisplayName("Permanent Failure/Enabled PF C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Permanent_Failure__Enabled_PF_C { get; }

        [Category("Settings"), DisplayName("Permanent Failure/Enabled PF D"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Permanent_Failure__Enabled_PF_D { get; }

        [Category("Settings"), DisplayName("Alarm/PF Alert Mask A"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Alarm__PF_Alert_Mask_A { get; }

        [Category("Settings"), DisplayName("Alarm/PF Alert Mask B"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Alarm__PF_Alert_Mask_B { get; }

        [Category("Settings"), DisplayName("Alarm/PF Alert Mask C"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Alarm__PF_Alert_Mask_C { get; }

        [Category("Settings"), DisplayName("Alarm/PF Alert Mask D"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Alarm__PF_Alert_Mask_D { get; }

        [Category("Permanent Fail"), DisplayName("CUDEP/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__CUDEP__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("CUDEP/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__CUDEP__Delay { get; }

        [Category("Permanent Fail"), DisplayName("SUV/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__SUV__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("SUV/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__SUV__Delay { get; }

        [Category("Permanent Fail"), DisplayName("SOV/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__SOV__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("SOV/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__SOV__Delay { get; }

        [Category("Permanent Fail"), DisplayName("TOS/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__TOS__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("TOS/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__TOS__Delay { get; }

        [Category("Permanent Fail"), DisplayName("SOCC/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__SOCC__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("SOCC/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__SOCC__Delay { get; }

        [Category("Permanent Fail"), DisplayName("SOCD/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__SOCD__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("SOCD/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__SOCD__Delay { get; }

        [Category("Permanent Fail"), DisplayName("SOT/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Permanent_Fail__SOT__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("SOT/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__SOT__Delay { get; }

        [Category("Permanent Fail"), DisplayName("SOTF/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__SOTF__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("SOTF/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__SOTF__Delay { get; }

        [Category("Permanent Fail"), DisplayName("VIMR/Check Voltage"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__VIMR__Check_Voltage { get; }

        [Category("Permanent Fail"), DisplayName("VIMR/Max Relax Current"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__VIMR__Max_Relax_Current { get; }

        [Category("Permanent Fail"), DisplayName("VIMR/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__VIMR__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("VIMR/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__VIMR__Delay { get; }

        [Category("Permanent Fail"), DisplayName("VIMR/Relax Min Duration"), TypeConverter(typeof(ExpandableObjectConverter))]
        public UshortDataMemory Permanent_Fail__VIMR__Relax_Min_Duration { get; }

        [Category("Permanent Fail"), DisplayName("VIMA/Check Voltage"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__VIMA__Check_Voltage { get; }

        [Category("Permanent Fail"), DisplayName("VIMA/Min Active Current"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__VIMA__Min_Active_Current { get; }

        [Category("Permanent Fail"), DisplayName("VIMA/Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__VIMA__Threshold { get; }

        [Category("Permanent Fail"), DisplayName("VIMA/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__VIMA__Delay { get; }

        [Category("Permanent Fail"), DisplayName("CFETF/OFF Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__CFETF__OFF_Threshold { get; }

        [Category("Permanent Fail"), DisplayName("CFETF/OFF Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__CFETF__OFF_Delay { get; }

        [Category("Permanent Fail"), DisplayName("DFETF/OFF Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__DFETF__OFF_Threshold { get; }

        [Category("Permanent Fail"), DisplayName("DFETF/OFF Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__DFETF__OFF_Delay { get; }

        [Category("Permanent Fail"), DisplayName("VSSF/Fail Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Permanent_Fail__VSSF__Fail_Threshold { get; }

        [Category("Permanent Fail"), DisplayName("VSSF/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__VSSF__Delay { get; }

        [Category("Permanent Fail"), DisplayName("2LVL/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__2LVL__Delay { get; }

        [Category("Permanent Fail"), DisplayName("LFOF/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__LFOF__Delay { get; }

        [Category("Permanent Fail"), DisplayName("HWMX/Delay"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Permanent_Fail__HWMX__Delay { get; }

        [Category("Settings"), DisplayName("Configuration/CFETOFF Pin Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__CFETOFF_Pin_Config { get; }

        [Category("Settings"), DisplayName("Configuration/DFETOFF Pin Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__DFETOFF_Pin_Config { get; }

        [Category("Settings"), DisplayName("Configuration/ALERT Pin Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__ALERT_Pin_Config { get; }

        [Category("Settings"), DisplayName("Configuration/TS1 Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__TS1_Config { get; }

        [Category("Settings"), DisplayName("Configuration/TS2 Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__TS2_Config { get; }

        [Category("Settings"), DisplayName("Configuration/TS3 Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__TS3_Config { get; }

        [Category("Settings"), DisplayName("Configuration/HDQ Pin Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__HDQ_Pin_Config { get; }

        [Category("Settings"), DisplayName("Configuration/DCHG Pin Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__DCHG_Pin_Config { get; }

        [Category("Settings"), DisplayName("Configuration/DDSG Pin Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__DDSG_Pin_Config { get; }

        [Category("Settings"), DisplayName("Configuration/DA Configuration"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__Configuration__DA_Configuration { get; }

        [Category("Settings"), DisplayName("Configuration/Vcell Mode"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit16DataMemory Settings__Configuration__Vcell_Mode { get; }

        [Category("Settings"), DisplayName("Configuration/CC3 Samples"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Configuration__CC3_Samples { get; }

        [Category("Settings"), DisplayName("FET/FET Options"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__FET__FET_Options { get; }

        [Category("Settings"), DisplayName("FET/Chg Pump Control"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Bit8DataMemory Settings__FET__Chg_Pump_Control { get; }

        [Category("Settings"), DisplayName("FET/Precharge Start Voltage"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__FET__Precharge_Start_Voltage { get; }

        [Category("Settings"), DisplayName("FET/Precharge Stop Voltage"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__FET__Precharge_Stop_Voltage { get; }

        [Category("Settings"), DisplayName("FET/Predischarge Timeout"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__FET__Predischarge_Timeout { get; }

        [Category("Settings"), DisplayName("FET/Predischarge Stop Delta"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__FET__Predischarge_Stop_Delta { get; }

        [Category("Settings"), DisplayName("Current Thresholds/Dsg Current Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Current_Thresholds__Dsg_Current_Threshold { get; }

        [Category("Settings"), DisplayName("Current Thresholds/Chg Current Threshold"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Current_Thresholds__Chg_Current_Threshold { get; }

        [Category("Settings"), DisplayName("Cell Open-Wire/Check Time"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Cell_Open_Wire__Check_Time { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 1 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_1_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 2 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_2_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 3 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_3_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 4 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_4_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 5 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_5_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 6 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_6_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 7 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_7_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 8 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_8_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 9 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_9_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 10 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_10_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 11 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_11_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 12 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_12_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 13 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_13_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 14 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_14_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 15 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_15_Interconnect { get; }

        [Category("Settings"), DisplayName("Interconnect Resistances/Cell 16 Interconnect"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Interconnect_Resistances__Cell_16_Interconnect { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Balancing Configuration"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Balancing_Configuration Settings__Cell_Balancing_Config__Balancing_Configuration { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Min Cell Temp"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Settings__Cell_Balancing_Config__Min_Cell_Temp { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Max Cell Temp"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Settings__Cell_Balancing_Config__Max_Cell_Temp { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Max Internal Temp"), TypeConverter(typeof(ExpandableObjectConverter))]
        public SbyteDataMemory Settings__Cell_Balancing_Config__Max_Internal_Temp { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Cell Balance Interval"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Cell_Balancing_Config__Cell_Balance_Interval { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Cell Balance Max Cells"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Cell_Balancing_Config__Cell_Balance_Max_Cells { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Cell Balance Min Cell V (Charge)"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Cell_Balancing_Config__Cell_Balance_Min_Cell_V_Charge { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Cell Balance Min Delta (Charge)"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Cell_Balancing_Config__Cell_Balance_Min_Delta_Charge { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Cell Balance Stop Delta (Charge)"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Cell_Balancing_Config__Cell_Balance_Stop_Delta_Charge { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Cell Balance Min Cell V (Relax)"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ShortDataMemory Settings__Cell_Balancing_Config__Cell_Balance_Min_Cell_V_Relax { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Cell Balance Min Delta (Relax)"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Cell_Balancing_Config__Cell_Balance_Min_Delta_Relax { get; }

        [Category("Settings"), DisplayName("Cell Balancing Config/Cell Balance Stop Delta (Relax)"), TypeConverter(typeof(ExpandableObjectConverter))]
        public ByteDataMemory Settings__Cell_Balancing_Config__Cell_Balance_Stop_Delta_Relax { get; }

        [Category("Settings"), DisplayName("Manufacturing/Mfg Status Init"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Mfg_Status_Init Settings__Manufacturing__Mfg_Status_Init { get; }
        #endregion

        public DataMemory(BQ76942_769142_76952 bms)
        {
            BMS = bms;
            ResetRams();
            #region Properties
            Calibration__Voltage__Cell_1_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_1_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_2_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_2_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_3_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_3_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_4_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_4_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_5_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_5_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_6_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_6_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_7_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_7_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_8_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_8_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_9_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_9_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_10_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_10_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_11_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_11_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_12_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_12_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_13_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_13_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_14_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_14_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_15_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_15_Gain, -32767, 32767, 0);
            Calibration__Voltage__Cell_16_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Cell_16_Gain, -32767, 32767, 0);
            Calibration__Voltage__Pack_Gain = new UshortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__Pack_Gain, 0, 65535, 0);
            Calibration__Voltage__TOS_Gain = new UshortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__TOS_Gain, 0, 65535, 0);
            Calibration__Voltage__LD_Gain = new UshortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__LD_Gain, 0, 65535, 0);
            Calibration__Voltage__ADC_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Voltage__ADC_Gain, -32767, 32767, 0);
            Calibration__Current__CC_Gain = new Current__CC_Gain(this, (ushort)DataMemoryRegister.Calibration__Current__CC_Gain, 1.00E-02f, 10.00E+02f, 7.4768f);
            Calibration__Current__Capacity_Gain = new Current__Capacity_Gain(this, (ushort)DataMemoryRegister.Calibration__Current__Capacity_Gain, 2.98262E+03f, 4.193046E+08f, 2230042.463f);
            Calibration__Vcell_Offset__Vcell_Offset = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Vcell_Offset__Vcell_Offset, -32767, 32767, 0);
            Calibration__V_Divider_Offset__Vdiv_Offset = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__V_Divider_Offset__Vdiv_Offset, -32767, 32767, 0);
            Calibration__Current_Offset__Coulomb_Counter_Offset_Samples = new UshortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Current_Offset__Coulomb_Counter_Offset_Samples, 0, 65535, 64);
            Calibration__Current_Offset__Board_Offset = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Current_Offset__Board_Offset, -32768, 32767, 0);
            Calibration__Temperature__Internal_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__Internal_Temp_Offset, -128, 127, 0);
            Calibration__Temperature__CFETOFF_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__CFETOFF_Temp_Offset, -128, 127, 0);
            Calibration__Temperature__DFETOFF_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__DFETOFF_Temp_Offset, -128, 127, 0);
            Calibration__Temperature__ALERT_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__ALERT_Temp_Offset, -128, 127, 0);
            Calibration__Temperature__TS1_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__TS1_Temp_Offset, -128, 127, 0);
            Calibration__Temperature__TS2_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__TS2_Temp_Offset, -128, 127, 0);
            Calibration__Temperature__TS3_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__TS3_Temp_Offset, -128, 127, 0);
            Calibration__Temperature__HDQ_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__HDQ_Temp_Offset, -128, 127, 0);
            Calibration__Temperature__DCHG_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__DCHG_Temp_Offset, -128, 127, 0);
            Calibration__Temperature__DDSG_Temp_Offset = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Temperature__DDSG_Temp_Offset, -128, 127, 0);
            Calibration__CUV__CUV_Threshold_Override = new UshortDataMemory(this, (ushort)DataMemoryRegister.Calibration__CUV__CUV_Threshold_Override, 0x0000, 0xFFFF, 0xFFFF);
            Calibration__COV__COV_Threshold_Override = new UshortDataMemory(this, (ushort)DataMemoryRegister.Calibration__COV__COV_Threshold_Override, 0x0000, 0xFFFF, 0xFFFF);
            System_Data__Integrity__Config_RAM_Signature = new UshortDataMemory(this, (ushort)DataMemoryRegister.System_Data__Integrity__Config_RAM_Signature, 0x0000, 0x7FFF, 0);
            Calibration__Internal_Temp_Model__Int_Gain = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Internal_Temp_Model__Int_Gain, -32768, 32767, 25390);
            Calibration__Internal_Temp_Model__Int_base_offset = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Internal_Temp_Model__Int_base_offset, -32768, 32767, 3032);
            Calibration__Internal_Temp_Model__Int_Maximum_AD = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Internal_Temp_Model__Int_Maximum_AD, -32768, 32767, 16383);
            Calibration__Internal_Temp_Model__Int_Maximum_Temp = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Internal_Temp_Model__Int_Maximum_Temp, 0, 32767, 6379);
            Calibration__18K_Temperature_Model__Coeff_a1 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Coeff_a1, -32768, 32767, -15524);
            Calibration__18K_Temperature_Model__Coeff_a2 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Coeff_a2, -32768, 32767, 26423);
            Calibration__18K_Temperature_Model__Coeff_a3 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Coeff_a3, -32768, 32767, -22664);
            Calibration__18K_Temperature_Model__Coeff_a4 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Coeff_a4, -32768, 32767, 28834);
            Calibration__18K_Temperature_Model__Coeff_a5 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Coeff_a5, -32768, 32767, 672);
            Calibration__18K_Temperature_Model__Coeff_b1 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Coeff_b1, -32768, 32767, -371);
            Calibration__18K_Temperature_Model__Coeff_b2 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Coeff_b2, -32768, 32767, 708);
            Calibration__18K_Temperature_Model__Coeff_b3 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Coeff_b3, -32768, 32767, -3498);
            Calibration__18K_Temperature_Model__Coeff_b4 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Coeff_b4, -32768, 32767, 5051);
            Calibration__18K_Temperature_Model__Adc0 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__18K_Temperature_Model__Adc0, -32768, 32767, 11703);
            Calibration__180K_Temperature_Model__Coeff_a1 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Coeff_a1, -32768, 32767, -17513);
            Calibration__180K_Temperature_Model__Coeff_a2 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Coeff_a2, -32768, 32767, 25759);
            Calibration__180K_Temperature_Model__Coeff_a3 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Coeff_a3, -32768, 32767, -23593);
            Calibration__180K_Temperature_Model__Coeff_a4 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Coeff_a4, -32768, 32767, 32175);
            Calibration__180K_Temperature_Model__Coeff_a5 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Coeff_a5, -32768, 32767, 2090);
            Calibration__180K_Temperature_Model__Coeff_b1 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Coeff_b1, -32768, 32767, -2055);
            Calibration__180K_Temperature_Model__Coeff_b2 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Coeff_b2, -32768, 32767, 2955);
            Calibration__180K_Temperature_Model__Coeff_b3 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Coeff_b3, -32768, 32767, -3427);
            Calibration__180K_Temperature_Model__Coeff_b4 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Coeff_b4, -32768, 32767, 4385);
            Calibration__180K_Temperature_Model__Adc0 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__180K_Temperature_Model__Adc0, -32768, 32767, 17246);
            Calibration__Custom_Temperature_Model__Coeff_a1 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Coeff_a1, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Coeff_a2 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Coeff_a2, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Coeff_a3 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Coeff_a3, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Coeff_a4 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Coeff_a4, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Coeff_a5 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Coeff_a5, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Coeff_b1 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Coeff_b1, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Coeff_b2 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Coeff_b2, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Coeff_b3 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Coeff_b3, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Coeff_b4 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Coeff_b4, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Rc0 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Rc0, -32768, 32767, 0);
            Calibration__Custom_Temperature_Model__Adc0 = new ShortDataMemory(this, (ushort)DataMemoryRegister.Calibration__Custom_Temperature_Model__Adc0, -32768, 32767, 0);
            Calibration__Current_Deadband__Coulomb_Counter_Deadband = new ByteDataMemory(this, (ushort)DataMemoryRegister.Calibration__Current_Deadband__Coulomb_Counter_Deadband, 0, 255, 9);
            Settings__Fuse__Min_Blow_Fuse_Voltage = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Fuse__Min_Blow_Fuse_Voltage, 0, 32767, 500);
            Settings__Fuse__Fuse_Blow_Timeout = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Fuse__Fuse_Blow_Timeout, 0, 255, 30);
            Settings__Configuration__Power_Config = new Bit16DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__Power_Config, 0x0000, 0xFFFF, 0x2982);
            Settings__Configuration__REG12_Config = new REG12_Config(this, (ushort)DataMemoryRegister.Settings__Configuration__REG12_Config, 0x00, 0xFF, 0x00);
            Settings__Configuration__REG0_Config = new REG0_Config(this, (ushort)DataMemoryRegister.Settings__Configuration__REG0_Config, 0x00, 0x03, 0x00);
            Settings__Configuration__HWD_Regulator_Options = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__HWD_Regulator_Options, 0x00, 0xFF, 0x00);
            Settings__Configuration__Comm_Type = new Comm_Type(this, (ushort)DataMemoryRegister.Settings__Configuration__Comm_Type, 0x00, 0x1F, 0);
            Settings__Configuration__I2C_Address = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__I2C_Address, 0x00, 0xFF, 0);
            Settings__Configuration__SPI_Configuration = new SPI_Configuration(this, (ushort)DataMemoryRegister.Settings__Configuration__SPI_Configuration, 0x00, 0x7F, 0x20);
            Settings__Configuration__Comm_Idle_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__Comm_Idle_Time, 0, 255, 0);
            Power__Shutdown__Shutdown_Cell_Voltage = new ShortDataMemory(this, (ushort)DataMemoryRegister.Power__Shutdown__Shutdown_Cell_Voltage, 0, 32767, 0);
            Power__Shutdown__Shutdown_Stack_Voltage = new ShortDataMemory(this, (ushort)DataMemoryRegister.Power__Shutdown__Shutdown_Stack_Voltage, 0, 32767, 600);
            Power__Shutdown__Low_V_Shutdown_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Power__Shutdown__Low_V_Shutdown_Delay, 0, 63, 1);
            Power__Shutdown__Shutdown_Temperature = new ByteDataMemory(this, (ushort)DataMemoryRegister.Power__Shutdown__Shutdown_Temperature, 0, 150, 85);
            Power__Shutdown__Shutdown_Temperature_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Power__Shutdown__Shutdown_Temperature_Delay, 0, 254, 5);
            Power__Sleep__Sleep_Current = new ShortDataMemory(this, (ushort)DataMemoryRegister.Power__Sleep__Sleep_Current, 0, 32767, 20);
            Power__Sleep__Voltage_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Power__Sleep__Voltage_Time, 1, 255, 5);
            Power__Sleep__Wake_Comparator_Current = new ShortDataMemory(this, (ushort)DataMemoryRegister.Power__Sleep__Wake_Comparator_Current, 500, 32767, 500);
            Power__Sleep__Sleep_Hysteresis_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Power__Sleep__Sleep_Hysteresis_Time, 0, 255, 10);
            Power__Sleep__Sleep_Charger_Voltage_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Power__Sleep__Sleep_Charger_Voltage_Threshold, 0, 32767, 2000);
            Power__Sleep__Sleep_Charger_PACK_TOS_Delta = new ShortDataMemory(this, (ushort)DataMemoryRegister.Power__Sleep__Sleep_Charger_PACK_TOS_Delta, 10, 8500, 200);
            Power__Shutdown__FET_Off_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Power__Shutdown__FET_Off_Delay, 0, 127, 0);
            Power__Shutdown__Shutdown_Command_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Power__Shutdown__Shutdown_Command_Delay, 0, 254, 0);
            Power__Shutdown__Auto_Shutdown_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Power__Shutdown__Auto_Shutdown_Time, 0, 250, 0);
            Power__Shutdown__RAM_Fail_Shutdown_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Power__Shutdown__RAM_Fail_Shutdown_Time, 0, 255, 5);
            Security__Settings__Security_Settings = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Security__Settings__Security_Settings, 0x00, 0x07, 0x00);
            Security__Keys__Unseal_Key_Step_1 = new UshortDataMemory(this, (ushort)DataMemoryRegister.Security__Keys__Unseal_Key_Step_1, 0x0100, 0xFFFF, 0x0414);
            Security__Keys__Unseal_Key_Step_2 = new UshortDataMemory(this, (ushort)DataMemoryRegister.Security__Keys__Unseal_Key_Step_2, 0x0100, 0xFFFF, 0x3672);
            Security__Keys__Full_Access_Key_Step_1 = new UshortDataMemory(this, (ushort)DataMemoryRegister.Security__Keys__Full_Access_Key_Step_1, 0x0100, 0xFFFF, 0xFFFF);
            Security__Keys__Full_Access_Key_Step_2 = new UshortDataMemory(this, (ushort)DataMemoryRegister.Security__Keys__Full_Access_Key_Step_2, 0x0100, 0xFFFF, 0xFFFF);
            Settings__Protection__Protection_Configuration = new Protection_Configuration(this, (ushort)DataMemoryRegister.Settings__Protection__Protection_Configuration, 0x0000, 0x07FF, 0x0002);
            Settings__Protection__Enabled_Protections_A = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__Enabled_Protections_A, 0x00, 0xFF, 0x88);
            Settings__Protection__Enabled_Protections_B = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__Enabled_Protections_B, 0x00, 0xFF, 0x00);
            Settings__Protection__Enabled_Protections_C = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__Enabled_Protections_C, 0x00, 0xFF, 0x00);
            Settings__Protection__CHG_FET_Protections_A = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__CHG_FET_Protections_A, 0x00, 0xFF, 0x98);
            Settings__Protection__CHG_FET_Protections_B = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__CHG_FET_Protections_B, 0x00, 0xFF, 0xD5);
            Settings__Protection__CHG_FET_Protections_C = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__CHG_FET_Protections_C, 0x00, 0xFF, 0x56);
            Settings__Protection__DSG_FET_Protections_A = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__DSG_FET_Protections_A, 0x00, 0xFF, 0xE4);
            Settings__Protection__DSG_FET_Protections_B = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__DSG_FET_Protections_B, 0x00, 0xFF, 0xE6);
            Settings__Protection__DSG_FET_Protections_C = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__DSG_FET_Protections_C, 0x00, 0xFF, 0xE2);
            Settings__Alarm__Default_Alarm_Mask = new Bit16DataMemory(this, (ushort)DataMemoryRegister.Settings__Alarm__Default_Alarm_Mask, 0x0000, 0xFFFF, 0xF800);
            Settings__Alarm__SF_Alert_Mask_A = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Alarm__SF_Alert_Mask_A, 0x00, 0xFF, 0xFC);
            Settings__Alarm__SF_Alert_Mask_B = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Alarm__SF_Alert_Mask_B, 0x00, 0xFF, 0xF7);
            Settings__Alarm__SF_Alert_Mask_C = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Alarm__SF_Alert_Mask_C, 0x00, 0xFF, 0xF4);
            Settings__Protection__Body_Diode_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Protection__Body_Diode_Threshold, 0, 32767, 50);
            Protections__CUV__Threshold = new CellThresholdDataMemory(this, (ushort)DataMemoryRegister.Protections__CUV__Threshold, 20, 90, 50);
            Protections__CUV__Delay = new UshortDataMemory(this, (ushort)DataMemoryRegister.Protections__CUV__Delay, 1, 2047, 74);
            Protections__COV__Threshold = new CellThresholdDataMemory(this, (ushort)DataMemoryRegister.Protections__COV__Threshold, 20, 110, 86);
            Protections__COV__Delay = new UshortDataMemory(this, (ushort)DataMemoryRegister.Protections__COV__Delay, 1, 2047, 74);
            Protections__CUV__Recovery_Hysteresis = new CellThresholdDataMemory(this, (ushort)DataMemoryRegister.Protections__CUV__Recovery_Hysteresis, 2, 20, 2);
            Protections__COV__Recovery_Hysteresis = new CellThresholdDataMemory(this, (ushort)DataMemoryRegister.Protections__COV__Recovery_Hysteresis, 2, 20, 2);
            Protections__COVL__Latch_Limit = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__COVL__Latch_Limit, 0, 255, 0);
            Protections__COVL__Counter_Dec_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__COVL__Counter_Dec_Delay, 0, 255, 10);
            Protections__COVL__Recovery_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__COVL__Recovery_Time, 0, 255, 15);
            Protections__OCC__Threshold = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCC__Threshold, 2, 62, 2);
            Protections__OCC__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCC__Delay, 1, 127, 4);
            Protections__OCD1__Threshold = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCD1__Threshold, 2, 100, 4);
            Protections__OCD1__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCD1__Delay, 1, 127, 1);
            Protections__OCD2__Threshold = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCD2__Threshold, 2, 100, 3);
            Protections__OCD2__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCD2__Delay, 1, 127, 7);
            Protections__SCD__Threshold = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__SCD__Threshold, 0, 15, 0);
            Protections__SCD__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__SCD__Delay, 1, 31, 2);
            Protections__OCC__Recovery_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Protections__OCC__Recovery_Threshold, -32768, 32767, -200);
            Protections__OCD3__Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Protections__OCD3__Threshold, -32768, 0, -4000);
            Protections__OCD3__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCD3__Delay, 0, 255, 2);
            Protections__OCD__Recovery_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Protections__OCD__Recovery_Threshold, -32768, 32767, 200);
            Protections__OCDL__Latch_Limit = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCDL__Latch_Limit, 0, 255, 0);
            Protections__OCDL__Counter_Dec_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCDL__Counter_Dec_Delay, 0, 255, 10);
            Protections__OCDL__Recovery_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OCDL__Recovery_Time, 0, 255, 15);
            Protections__OCDL__Recovery_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Protections__OCDL__Recovery_Threshold, -32768, 32767, 200);
            Protections__SCD__Recovery_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__SCD__Recovery_Time, 0, 255, 5);
            Protections__SCDL__Latch_Limit = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__SCDL__Latch_Limit, 0, 255, 0);
            Protections__SCDL__Counter_Dec_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__SCDL__Counter_Dec_Delay, 0, 255, 10);
            Protections__SCDL__Recovery_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__SCDL__Recovery_Time, 0, 255, 15);
            Protections__SCDL__Recovery_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Protections__SCDL__Recovery_Threshold, -32768, 32767, 200);
            Protections__OTC__Threshold = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTC__Threshold, -40, 120, 55);
            Protections__OTC__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTC__Delay, 0, 255, 2);
            Protections__OTC__Recovery = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTC__Recovery, -40, 120, 50);
            Protections__OTD__Threshold = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTD__Threshold, -40, 120, 60);
            Protections__OTD__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTD__Delay, 0, 255, 2);
            Protections__OTD__Recovery = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTD__Recovery, -40, 120, 55);
            Protections__OTF__Threshold = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTF__Threshold, 0, 150, 80);
            Protections__OTF__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTF__Delay, 0, 255, 2);
            Protections__OTF__Recovery = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTF__Recovery, 0, 150, 65);
            Protections__OTINT__Threshold = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTINT__Threshold, -40, 120, 85);
            Protections__OTINT__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTINT__Delay, 0, 255, 2);
            Protections__OTINT__Recovery = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__OTINT__Recovery, -40, 120, 80);
            Protections__UTC__Threshold = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__UTC__Threshold, -40, 120, 0);
            Protections__UTC__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__UTC__Delay, 0, 255, 2);
            Protections__UTC__Recovery = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__UTC__Recovery, -40, 120, 5);
            Protections__UTD__Threshold = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__UTD__Threshold, -40, 120, 0);
            Protections__UTD__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__UTD__Delay, 0, 255, 2);
            Protections__UTD__Recovery = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__UTD__Recovery, -40, 120, 5);
            Protections__UTINT__Threshold = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__UTINT__Threshold, -40, 120, -20);
            Protections__UTINT__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__UTINT__Delay, 0, 255, 2);
            Protections__UTINT__Recovery = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Protections__UTINT__Recovery, -40, 120, -15);
            Protections__Recovery__Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__Recovery__Time, 0, 255, 3);
            Protections__OCC__PACK_TOS_Delta = new ShortDataMemory(this, (ushort)DataMemoryRegister.Protections__OCC__PACK_TOS_Delta, 10, 8500, 200);
            Protections__HWD__Delay = new UshortDataMemory(this, (ushort)DataMemoryRegister.Protections__HWD__Delay, 0, 65535, 60);
            Protections__Load_Detect__Active_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__Load_Detect__Active_Time, 0, 255, 0);
            Protections__Load_Detect__Retry_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Protections__Load_Detect__Retry_Delay, 0, 255, 50);
            Protections__Load_Detect__Timeout = new UshortDataMemory(this, (ushort)DataMemoryRegister.Protections__Load_Detect__Timeout, 0, 65535, 1);
            Protections__PTO__Charge_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Protections__PTO__Charge_Threshold, -32768, 32767, 250);
            Protections__PTO__Delay = new UshortDataMemory(this, (ushort)DataMemoryRegister.Protections__PTO__Delay, 0, 65535, 1800);
            Protections__PTO__Reset = new ShortDataMemory(this, (ushort)DataMemoryRegister.Protections__PTO__Reset, 0, 10000, 2);
            Settings__Permanent_Failure__Enabled_PF_A = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Permanent_Failure__Enabled_PF_A, 0x00, 0xFF, 0x00);
            Settings__Permanent_Failure__Enabled_PF_B = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Permanent_Failure__Enabled_PF_B, 0x00, 0xFF, 0x00);
            Settings__Permanent_Failure__Enabled_PF_C = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Permanent_Failure__Enabled_PF_C, 0x00, 0xFF, 0x07);
            Settings__Permanent_Failure__Enabled_PF_D = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Permanent_Failure__Enabled_PF_D, 0x00, 0xFF, 0x00);
            Settings__Alarm__PF_Alert_Mask_A = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Alarm__PF_Alert_Mask_A, 0x00, 0xFF, 0x5F);
            Settings__Alarm__PF_Alert_Mask_B = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Alarm__PF_Alert_Mask_B, 0x00, 0xFF, 0x9F);
            Settings__Alarm__PF_Alert_Mask_C = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Alarm__PF_Alert_Mask_C, 0x00, 0xFF, 0x00);
            Settings__Alarm__PF_Alert_Mask_D = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Alarm__PF_Alert_Mask_D, 0x00, 0xFF, 0x00);
            Permanent_Fail__CUDEP__Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__CUDEP__Threshold, 0, 32767, 1500);
            Permanent_Fail__CUDEP__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__CUDEP__Delay, 0, 255, 2);
            Permanent_Fail__SUV__Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SUV__Threshold, 0, 32767, 2200);
            Permanent_Fail__SUV__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SUV__Delay, 0, 255, 5);
            Permanent_Fail__SOV__Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOV__Threshold, 0, 32767, 4500);
            Permanent_Fail__SOV__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOV__Delay, 0, 255, 5);
            Permanent_Fail__TOS__Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__TOS__Threshold, 0, 32767, 500);
            Permanent_Fail__TOS__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__TOS__Delay, 0, 255, 5);
            Permanent_Fail__SOCC__Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOCC__Threshold, -32768, 32767, 10000);
            Permanent_Fail__SOCC__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOCC__Delay, 0, 255, 5);
            Permanent_Fail__SOCD__Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOCD__Threshold, -32768, 32767, -32000);
            Permanent_Fail__SOCD__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOCD__Delay, 0, 255, 5);
            Permanent_Fail__SOT__Threshold = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOT__Threshold, -40, 120, 65);
            Permanent_Fail__SOT__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOT__Delay, 0, 255, 5);
            Permanent_Fail__SOTF__Threshold = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOTF__Threshold, 0, 150, 85);
            Permanent_Fail__SOTF__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__SOTF__Delay, 0, 255, 5);
            Permanent_Fail__VIMR__Check_Voltage = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VIMR__Check_Voltage, 0, 5500, 3500);
            Permanent_Fail__VIMR__Max_Relax_Current = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VIMR__Max_Relax_Current, 10, 32767, 10);
            Permanent_Fail__VIMR__Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VIMR__Threshold, 0, 5500, 500);
            Permanent_Fail__VIMR__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VIMR__Delay, 0, 255, 5);
            Permanent_Fail__VIMR__Relax_Min_Duration = new UshortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VIMR__Relax_Min_Duration, 0, 65535, 100);
            Permanent_Fail__VIMA__Check_Voltage = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VIMA__Check_Voltage, 0, 5500, 3700);
            Permanent_Fail__VIMA__Min_Active_Current = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VIMA__Min_Active_Current, 10, 32767, 50);
            Permanent_Fail__VIMA__Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VIMA__Threshold, 0, 5500, 200);
            Permanent_Fail__VIMA__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VIMA__Delay, 0, 255, 5);
            Permanent_Fail__CFETF__OFF_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__CFETF__OFF_Threshold, 10, 5000, 20);
            Permanent_Fail__CFETF__OFF_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__CFETF__OFF_Delay, 0, 255, 5);
            Permanent_Fail__DFETF__OFF_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__DFETF__OFF_Threshold, -5000, -10, -20);
            Permanent_Fail__DFETF__OFF_Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__DFETF__OFF_Delay, 0, 255, 5);
            Permanent_Fail__VSSF__Fail_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VSSF__Fail_Threshold, 1, 32767, 100);
            Permanent_Fail__VSSF__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__VSSF__Delay, 0, 255, 5);
            Permanent_Fail__2LVL__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__2LVL__Delay, 0, 255, 5);
            Permanent_Fail__LFOF__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__LFOF__Delay, 0, 255, 5);
            Permanent_Fail__HWMX__Delay = new ByteDataMemory(this, (ushort)DataMemoryRegister.Permanent_Fail__HWMX__Delay, 0, 255, 5);
            Settings__Configuration__CFETOFF_Pin_Config = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__CFETOFF_Pin_Config, 0x00, 0xFF, 0x00);
            Settings__Configuration__DFETOFF_Pin_Config = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__DFETOFF_Pin_Config, 0x00, 0xFF, 0x00);
            Settings__Configuration__ALERT_Pin_Config = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__ALERT_Pin_Config, 0x00, 0xFF, 0x00);
            Settings__Configuration__TS1_Config = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__TS1_Config, 0x00, 0xFF, 0x07);
            Settings__Configuration__TS2_Config = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__TS2_Config, 0x00, 0xFF, 0x00);
            Settings__Configuration__TS3_Config = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__TS3_Config, 0x00, 0xFF, 0x00);
            Settings__Configuration__HDQ_Pin_Config = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__HDQ_Pin_Config, 0x00, 0xFF, 0x00);
            Settings__Configuration__DCHG_Pin_Config = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__DCHG_Pin_Config, 0x00, 0xFF, 0x00);
            Settings__Configuration__DDSG_Pin_Config = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__DDSG_Pin_Config, 0x00, 0xFF, 0x00);
            Settings__Configuration__DA_Configuration = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__DA_Configuration, 0x00, 0xFF, 0x05);
            Settings__Configuration__Vcell_Mode = new Bit16DataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__Vcell_Mode, 0x0000, 0xFFFF, 0x0000);
            Settings__Configuration__CC3_Samples = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Configuration__CC3_Samples, 2, 255, 80);
            Settings__FET__FET_Options = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__FET__FET_Options, 0x00, 0xFF, 0x0D);
            Settings__FET__Chg_Pump_Control = new Bit8DataMemory(this, (ushort)DataMemoryRegister.Settings__FET__Chg_Pump_Control, 0x00, 0xFF, 0x01);
            Settings__FET__Precharge_Start_Voltage = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__FET__Precharge_Start_Voltage, 0, 32767, 0);
            Settings__FET__Precharge_Stop_Voltage = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__FET__Precharge_Stop_Voltage, 0, 32767, 0);
            Settings__FET__Predischarge_Timeout = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__FET__Predischarge_Timeout, 0, 255, 5);
            Settings__FET__Predischarge_Stop_Delta = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__FET__Predischarge_Stop_Delta, 0, 255, 50);
            Settings__Current_Thresholds__Dsg_Current_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Current_Thresholds__Dsg_Current_Threshold, 0, 32767, 100);
            Settings__Current_Thresholds__Chg_Current_Threshold = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Current_Thresholds__Chg_Current_Threshold, 0, 32767, 50);
            Settings__Cell_Open_Wire__Check_Time = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Open_Wire__Check_Time, 0, 255, 5);
            Settings__Interconnect_Resistances__Cell_1_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_1_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_2_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_2_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_3_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_3_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_4_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_4_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_5_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_5_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_6_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_6_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_7_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_7_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_8_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_8_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_9_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_9_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_10_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_10_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_11_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_11_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_12_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_12_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_13_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_13_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_14_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_14_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_15_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_15_Interconnect, 0, 32767, 0);
            Settings__Interconnect_Resistances__Cell_16_Interconnect = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Interconnect_Resistances__Cell_16_Interconnect, 0, 32767, 0);
            Settings__Cell_Balancing_Config__Balancing_Configuration = new Balancing_Configuration(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Balancing_Configuration, 0x00, 0xFF, 0x00);
            Settings__Cell_Balancing_Config__Min_Cell_Temp = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Min_Cell_Temp, -128, 127, -20);
            Settings__Cell_Balancing_Config__Max_Cell_Temp = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Max_Cell_Temp, -128, 127, 60);
            Settings__Cell_Balancing_Config__Max_Internal_Temp = new SbyteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Max_Internal_Temp, -128, 127, 70);
            Settings__Cell_Balancing_Config__Cell_Balance_Interval = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Cell_Balance_Interval, 1, 255, 20);
            Settings__Cell_Balancing_Config__Cell_Balance_Max_Cells = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Cell_Balance_Max_Cells, 0, 16, 1);
            Settings__Cell_Balancing_Config__Cell_Balance_Min_Cell_V_Charge = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Cell_Balance_Min_Cell_V_Charge, 0, 5000, 3900);
            Settings__Cell_Balancing_Config__Cell_Balance_Min_Delta_Charge = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Cell_Balance_Min_Delta_Charge, 0, 255, 40);
            Settings__Cell_Balancing_Config__Cell_Balance_Stop_Delta_Charge = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Cell_Balance_Stop_Delta_Charge, 0, 255, 20);
            Settings__Cell_Balancing_Config__Cell_Balance_Min_Cell_V_Relax = new ShortDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Cell_Balance_Min_Cell_V_Relax, 0, 5000, 3900);
            Settings__Cell_Balancing_Config__Cell_Balance_Min_Delta_Relax = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Cell_Balance_Min_Delta_Relax, 0, 255, 40);
            Settings__Cell_Balancing_Config__Cell_Balance_Stop_Delta_Relax = new ByteDataMemory(this, (ushort)DataMemoryRegister.Settings__Cell_Balancing_Config__Cell_Balance_Stop_Delta_Relax, 0, 255, 20);
            Settings__Manufacturing__Mfg_Status_Init = new Mfg_Status_Init(this, (ushort)DataMemoryRegister.Settings__Manufacturing__Mfg_Status_Init, 0x0000, 0xFFFF, 0x0040);
            #endregion
        }

        #region AccessReadWrite
        public void Read(DataMemoryRegister register)
        {
            Read((ushort)register);
        }

        public void Read(ushort address)
        {
            Read(new List<ushort>() { address });
        }

        public void Read(IEnumerable<ushort> addresses)
        {
            foreach (ushort address in addresses)
            {
                byte blockSize = BLOCK_SIZE;
                byte[] commandBytes = BMS.DirectRam.WriteSubcommand(address);
                byte[] buffer = BMS.DirectRam.ReadBuffer(commandBytes, blockSize, true);
                int index = GetIndex(address);
                if (index + blockSize > SIZE)
                {
                    blockSize = (byte)(SIZE - index);
                }
                Array.Copy(buffer, 0, ramRead, index, blockSize);
            }
            OnPropertyChanged(nameof(RamRead));
        }

        public void ReadAll()
        {
            List<ushort> addresses = new List<ushort>();
            for (ushort address = START; address < END; address += BLOCK_SIZE)
            {
                addresses.Add(address);
            }
            Read(addresses);
        }

        public void Write(Block block)
        {
            byte[] buffer = new byte[block.Size];
            Array.Copy(ramWrite, GetIndex(block.Address), buffer, 0, block.Size);
            byte[] commandBytes = BMS.DirectRam.WriteSubcommand(block.Address);
            BMS.DirectRam.WriteBuffer(commandBytes, buffer);
        }

        public void WriteChanges()
        {
            bool[] registersToWrite = RegistersToWrite;
            List<Block> blocks = new List<Block>();
            byte blockSize = GetWriteBlockSize(registersToWrite);
            for (ushort offset = 0; offset < SIZE; offset += blockSize)
            {
                byte maxBlockSize = (byte)Math.Min(blockSize, SIZE - offset);
                if (new ArraySegment<bool>(registersToWrite, offset, maxBlockSize).Contains(true))
                {
                    blocks.Add(new Block((ushort)(START + offset), maxBlockSize));
                }
            }

            foreach (Block block in blocks)
            {
                Console.WriteLine(block.Address.ToString("X4") + " " + block.Size);
            }

            if (blockSize > 1)
            {
                BMS.Subcommands.SET_CFGUPDATE.Send();
            }
            foreach (Block block in blocks)
            {
                Write(block);
            }
            if (blockSize > 1)
            {
                BMS.Subcommands.EXIT_CFGUPDATE.Send();
            }

            Read(blocks.Select(block => block.Address));
        }

        private byte GetWriteBlockSize(bool[] registersToWrite)
        {
            Dictionary<byte, ushort> writeBlockSizes = new Dictionary<byte, ushort>();
            if (registersToWrite.Count(w => w) == 1)
            {
                writeBlockSizes.Add(1, 1);
            }
            else
            {
                for (byte blockSize = 2; blockSize <= 32; blockSize *= 2)
                {
                    ushort blockCount = 0;
                    for (ushort offset = 0; offset < SIZE; offset += blockSize)
                    {
                        byte maxBlockSize = (byte)Math.Min(blockSize, SIZE - offset);
                        if (new ArraySegment<bool>(registersToWrite, offset, maxBlockSize).Contains(true))
                        {
                            blockCount++;
                        }
                    }
                    writeBlockSizes.Add(blockSize, blockCount);
                }
            }
            var optimalBlock = writeBlockSizes.OrderBy(wbs => wbs.Value).First();
            Console.WriteLine(string.Format("The best is write {0} bytes {1} times", optimalBlock.Key, optimalBlock.Value));
            return optimalBlock.Key;
        }
        #endregion

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
