using Alber.Eol.Hardware;
using AlberEOL.Base;
using AlberEOL.CustomClasses;
using AlberEOL.Devices;
using AlberEOL.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using TraceabilityHandler;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;
using static AlberEOL.CustomClasses.VtepMANU_DATA;
using static VTEP.TI.BatteryManagement.BQ76942_769142_76952.DataMemory;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        private List<TestDetail> listOfErrorDetails;

        public bool PreliminaryTest(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.PreliminaryTest;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            Parameter param_PRE_CellVoltage = Product.GetParameter("EOL_PRE_CellVoltage");
            DirectRam.ReadAll();
            // Cell Voltages
            decimal measCellVoltage = -1.0M;
            partialResult = true;
            decimal cellVoltageMin = (decimal)param_PRE_CellVoltage.Min;
            decimal cellVoltageMax = (decimal)param_PRE_CellVoltage.Max;
            int i;
            for (i = 1; i <= 10; i++)
            {
                measCellVoltage = DirectRam.GetCellVoltage(i);
                partialResult =
                    measCellVoltage >= cellVoltageMin &&
                    measCellVoltage <= cellVoltageMax;
                if (!partialResult)
                {
                    ErrorCode = new ErrorCode("PRE", $"Nem állnak fenn a kezdeti feltételek a teszthez!");
                    AddErrorInfo(testStep, "PRE", $"{i}. CellVoltage");
                    break;
                }
            }
            result &= partialResult;
            ManageTestDetail("PRE CellVoltage", 1, param_PRE_CellVoltage, (double)measCellVoltage, $"{Math.Min(i, 10)}. cell: {measCellVoltage.ToString()}", partialResult);
            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");

            return result;
        }

        public bool ImpedanceTest(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.ImpedanceTest_1kHz;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            // Paramters
            Parameter param_BIM_Impedance = Product.GetParameter("EOL_BIM_Impedance_1kHz");
            Parameter param_BIM_Voltage = Product.GetParameter("EOL_BIM_Voltage");

            // Local variables
            double paramImpedanceMin = (double)param_BIM_Impedance.Min;
            double paramImpedanceMax = (double)param_BIM_Impedance.Max;
            double paramVoltageMin = (double)param_BIM_Voltage.Min;
            double paramVoltageMax = (double)param_BIM_Voltage.Max;

            // Measurement
            BIM.GetResult(BimCommandType.Init);
            BIM.GetResult(BimCommandType.Mode_1kHz);
            BIM.GetResult(BimCommandType.Single);

            BimResult result_1kHz = BIM.GetResult(BimCommandType.Trigger1);

            // Eredmény - ImpEoledancia
            partialResult =
                result_1kHz.Impedance < paramImpedanceMax &&
                result_1kHz.Impedance > paramImpedanceMin;
            result &= partialResult;
            ManageTestDetail("BIM Impedance", 1, param_BIM_Impedance, result_1kHz.Impedance, result_1kHz.Impedance.ToString("0.000"), partialResult);

            // Eredmény - Voltage
            partialResult =
                result_1kHz.Voltage < paramVoltageMax &&
                result_1kHz.Voltage > paramVoltageMin;
            result &= partialResult;
            ManageTestDetail("BIM Voltage", 1, param_BIM_Voltage, result_1kHz.Voltage, result_1kHz.Voltage.ToString("0.000"), partialResult);

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool VerifyMANU_DATA(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.VerifyManuData;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            // Felhasznált paraméterek
            Parameter param_EOL_VMD_Customer = Product.GetParameter("EOL_VMD_Customer");
            Parameter param_EOL_VMD_Type = Product.GetParameter("EOL_VMD_Type");
            Parameter param_EOL_VMD_State = Product.GetParameter("EOL_VMD_State");
            Parameter param_EOL_VMD_Layout = Product.GetParameter("EOL_VMD_Layout");
            Parameter param_EOL_VMD_ItemCode = Product.GetParameter("EOL_VMD_ItemCode");
            Parameter param_EOL_VMD_Serial = Product.GetParameter("EOL_VMD_Serial");
            Parameter param_EOL_VMD_AccCh = Product.GetParameter("EOL_VMD_AccCh");
            Parameter param_EOL_VMD_AccTime = Product.GetParameter("EOL_VMD_AccTime");

            Parameter param_EOL_VMD_FetEn = Product.GetParameter("EOL_VMD_FetEn");
            Parameter param_EOL_VMD_PfEn = Product.GetParameter("EOL_VMD_PfEn");

            Product_Types productTypes = API.GetProductTypes();
            ProductType actualProductType = productTypes.ProductTypes.FirstOrDefault(pt => pt.ProductTypeID == Tester.ProductTypeID);

            long minSerialNumber = Settings.Default.PCBDateMin;

            // Reset DataMemory
            Subcommands.RESET.Send();

            // MANU_DATA olvasás
            Subcommands.MANU_DATA.Read();
            string serialNumber = ((VtepMANU_DATA)Subcommands.MANU_DATA).SerialNumber.ToString();
            Customers customer = ((VtepMANU_DATA)Subcommands.MANU_DATA).Customer;
            Types type = ((VtepMANU_DATA)Subcommands.MANU_DATA).Type;
            States state = ((VtepMANU_DATA)Subcommands.MANU_DATA).State;
            byte layoutVersion = ((VtepMANU_DATA)Subcommands.MANU_DATA).LayoutVersion;
            string itemCode = ((VtepMANU_DATA)Subcommands.MANU_DATA).ItemCode.ToString();

            // Ellenőrzések
            // MANU_DATA.Customer
            partialResult = customer == Customers.Alber_GmbH;
            result &= partialResult;
            ManageTestDetail("VMD Customer", 1, param_EOL_VMD_Customer, null, customer.ToString(), partialResult);

            // MANU_DATA.Type
            partialResult = (type == Types.Sample || type == Types.CalDummy);
            result &= partialResult;
            ManageTestDetail("VMD Type", 1, param_EOL_VMD_Type, null, type.ToString(), partialResult);

            // MANU_DATA.State
            partialResult = (state == States.FunctionTest || state == States.EolTest);
            result &= partialResult;
            ManageTestDetail("VMD State", 1, param_EOL_VMD_State, null, state.ToString(), partialResult);

            // MANU_DATA.LayoutVersion
            partialResult = layoutVersion == param_EOL_VMD_Layout.Nominal;
            result &= partialResult;
            ManageTestDetail("VMD Layout", 1, param_EOL_VMD_Layout, null, layoutVersion.ToString(), partialResult);

            // MANU_DATA.ItemCode
            partialResult = itemCode == actualProductType.ItemCode;
            result &= partialResult;
            ManageTestDetail("VMD ItemCode", 1, param_EOL_VMD_ItemCode, null, itemCode, partialResult);

            // MANU_DATA.SerialNumber IsNullOrEmpty
            partialResult = !string.IsNullOrEmpty(serialNumber);
            result &= partialResult;
            ManageTestDetail("VMD SNIsNullOrEmp", null, null, "NotNullOrEmpty", "text", serialNumber, partialResult);

            // MANU_DATA.SerialNumber Regex Test
            bool regexTest = Regex.IsMatch(serialNumber, Settings.Default.SNPattern);
            ManageTestDetail("VMD SN Regex", null, null, "YYMMDDNNNN", "text", serialNumber, regexTest);

            // MANU_DATA.SerialNumber check date before 2208220001
            bool dateCheck = ((VtepMANU_DATA)Subcommands.MANU_DATA).SerialNumber >= minSerialNumber;
            ManageTestDetail("VMD SN MinDate", null, null, $">={minSerialNumber}", "text", serialNumber, dateCheck);

            // MANU_DATA.SerialNumber Trace SN
            partialResult = (serialNumber == Product.TraceProduct.SerialNumber) && regexTest && dateCheck;
            result &= partialResult;
            ManageTestDetail("VMD SN ProductState", 1, param_EOL_VMD_Serial, null, serialNumber, partialResult);


            // fetEN és pfEN Check - both must be true after reset
            Subcommands.MANUFACTURING_STATUS.Read();

            // FetEn Check
            partialResult = Subcommands.MANUFACTURING_STATUS.FET_EN;
            result &= partialResult;
            ManageTestDetail(null, 1, param_EOL_VMD_FetEn, partialResult ? 1 : 0, null, partialResult);

            // PfEn Check
            partialResult = Subcommands.MANUFACTURING_STATUS.PF_EN;
            result &= partialResult;
            ManageTestDetail(null, 1, param_EOL_VMD_PfEn, partialResult ? 1 : 0, null, partialResult);

            //Subcommands.DASTATUS6.Read();
            Subcommands.RESET_PASSQ.Send();
            Thread.Sleep(50);
            Subcommands.DASTATUS6.Read();

            // MANU_DATA.DASTATUS6 AccumCharge && AccumTime ?= 0
            partialResult =
                Subcommands.DASTATUS6.Accum_Time <= param_EOL_VMD_AccTime.Max &&
                Subcommands.DASTATUS6.Accum_Time >= param_EOL_VMD_AccTime.Min;
            result &= partialResult;
            ManageTestDetail("VMD SubC AccTime", 1, param_EOL_VMD_AccTime, Subcommands.DASTATUS6.Accum_Time, null, partialResult);

            partialResult =
                Subcommands.DASTATUS6.Accum_Charge <= param_EOL_VMD_AccCh.Max &&
                Subcommands.DASTATUS6.Accum_Charge >= param_EOL_VMD_AccCh.Min;
            result &= partialResult;
            ManageTestDetail("VMD SubC AccCh", 1, param_EOL_VMD_AccCh, Subcommands.DASTATUS6.Accum_Charge, null, partialResult);

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool VerifyDataMemory(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.VerifyDataMemory;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            // Parameters
            Parameter param_VDM_ByteDiffs = Product.GetParameter("EOL_VDM_ByteDiffs");
            Parameter param_VDM_DMLength = Product.GetParameter("EOL_VDM_DMLength");
            Parameter param_VDM_R = Product.GetParameter("EOL_VDM_R");
            Property prop_EOL_DM_DetailParameters = GetPropertyFromTrace("EOL_VDM_CAL_DETAILS_PARAM");

            Property prop_CAL_CDM_DefaultDataMemory = GetPropertyFromTrace("CAL_CDM_DefaultDataMemory");
            Property prop_CAL_CDM_DefaultDataMemoryMask = GetPropertyFromTrace("CAL_CDM_DefaultDataMemoryMask");

            string[] getParams = prop_EOL_DM_DetailParameters.Value.Split(';');
            int opID, parItemID, judgement;

            opID = StringToInt(getParams[0]);
            parItemID = StringToInt(getParams[1]);
            judgement = StringToInt(getParams[2]);

            ProductOperationDetail calDataMemory = API.GetProductOperationDetail(this.Product.TraceProduct.ProductID, opID, parItemID, judgement);

            // Read DataMemory
            DataMemory.ReadAll();

            string dataMemoryString = "";
            byte[] dataMemoryByteArray;
            if (calDataMemory != null && calDataMemory.OK)
            {
                dataMemoryString = calDataMemory.OpDetail[0].TextValue;
            }
            else
            {
                ErrorCode = new ErrorCode("VDM", "Nem kaptam vissza megfelelő DM-et a trace-ből!");
                result = false;
            }

            dataMemoryByteArray = JsonConvert.DeserializeObject<byte[]>(dataMemoryString);

            // Read Stored DataMemory
            PrevDataMemory = dataMemoryByteArray;

            // Test DataMemory Length
            partialResult = DataMemory.RamRead.Length == PrevDataMemory.Length;
            result &= partialResult;
            ManageTestDetail("VDM DMLength", 1, param_VDM_DMLength, DataMemory.RamRead.Length, null, partialResult);

            // Test differences byte by byte
            MemDiffs = new bool[DataMemory.RamRead.Length];

            byte[] dmMask = ReadDataMemoryFromFile($"{prop_CAL_CDM_DefaultDataMemoryMask.Value}.bin");
            byte[] dmDefault = ReadDataMemoryFromFile($"{prop_CAL_CDM_DefaultDataMemory.Value}.bin");

            int diffCounter = 0;
            int defDiffCounter = 0;

            for (int i = 0; i < DataMemory.RamRead.Length; i++)
            {
                MemDiffs[i] = PrevDataMemory[i] != DataMemory.RamRead[i];
                if (MemDiffs[i]) diffCounter++;

                if (dmMask[i] != 0 && dmDefault[i] != DataMemory.RamRead[i])
                {
                    defDiffCounter++;
                }
            }

            DataMemory.CopyReadToWrite();

            float rMin = (float)param_VDM_R.Min;
            float rMax = (float)param_VDM_R.Max;
            float rCC = DataMemory.Calibration__Current__CC_Gain.Resistance;
            float rCA = DataMemory.Calibration__Current__Capacity_Gain.Resistance;
            partialResult = rMin <= rCC && rCC <= rMax;
            result &= partialResult;
            ManageTestDetail("VDM R", 1, param_VDM_R, rCC, $"rCC: {rCC}", partialResult);

            partialResult = rMin <= rCA && rCA <= rMax;
            result &= partialResult;
            ManageTestDetail("VDM R", 2, param_VDM_R, rCA, $"rCA: {rCA}", partialResult);

            partialResult =
                diffCounter <= param_VDM_ByteDiffs.Max &&
                diffCounter >= param_VDM_ByteDiffs.Min;
            result &= partialResult;
            ManageTestDetail("VDM CurrDM Diffs", 1, param_VDM_ByteDiffs, diffCounter, $"CurrentDM: {diffCounter}", partialResult);

            partialResult =
                defDiffCounter <= param_VDM_ByteDiffs.Max &&
                defDiffCounter >= param_VDM_ByteDiffs.Min;
            result &= partialResult;
            ManageTestDetail("VDM DefDM Diffs", 2, param_VDM_ByteDiffs, defDiffCounter, $"DefaultDM: {defDiffCounter}", partialResult);

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool EnableFuseDrive(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.EnableFuseDrive;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            // Parameters
            Parameter param_EOL_EFD_FDStatus = Product.GetParameter("EOL_EFD_FDStatus");

            DataMemory.ReadAll();

            // Read the current state of the bit
            bool fuseDriveBitStatus = DataMemory.Settings__Protection__Protection_Configuration.Bit4;

            // If it not set
            if (!fuseDriveBitStatus)
            {
                DataMemory.Read(DataMemoryRegister.Settings__Protection__Protection_Configuration);
                DataMemory.Settings__Protection__Protection_Configuration.Bit4 = true;
                DataMemory.Write(new Block(DataMemoryRegister.Settings__Protection__Protection_Configuration, 1));
                DataMemory.Read(DataMemoryRegister.Settings__Protection__Protection_Configuration);
            }

            partialResult = DataMemory.Settings__Protection__Protection_Configuration.Bit4 && (param_EOL_EFD_FDStatus.Nominal == 1 ? true : false);
            result &= partialResult;
            ManageTestDetail("EFD - Status", 1, param_EOL_EFD_FDStatus, fuseDriveBitStatus ? 1 : 0, "FD Enabled", partialResult);

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool VerifyDirectRam(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.VerifyDirectRam;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            Parameter param_VDR_TempDeviation = Product.GetParameter("EOL_VDR_TempDeviation");
            Parameter param_VDR_TempDevWoInt = Product.GetParameter("EOL_VDR_TempDevWoInt");
            Parameter param_VDR_IntTemp = Product.GetParameter("EOL_VDR_IntTemp");
            Parameter param_VDR_CC2Current = Product.GetParameter("EOL_VDR_CC2Current");
            Parameter param_VDR_StackVoltage = Product.GetParameter("EOL_VDR_StackVoltage");
            Parameter param_VDR_CellVoltageDev = Product.GetParameter("EOL_VDR_CellVoltageDev");
            Parameter param_VDR_PackPinVoltageDev = Product.GetParameter("EOL_VDR_PackPinVoltageDev");
            Parameter param_VDR_LDPinVoltageDev = Product.GetParameter("EOL_VDR_LDPinVoltageDev");
            Parameter param_VDR_DSG_FET_Status = Product.GetParameter("EOL_VDR_DSG_FET_Status");
            Parameter param_VDR_CHG_FET_Status = Product.GetParameter("EOL_VDR_CHG_FET_Status");
            Parameter param_VDR_SSA = Product.GetParameter("EOL_VDR_SSA");
            Parameter param_VDR_SSB = Product.GetParameter("EOL_VDR_SSB");
            Parameter param_VDR_SSC = Product.GetParameter("EOL_VDR_SSC");
            Parameter param_VDR_PFA = Product.GetParameter("EOL_VDR_PFA");
            Parameter param_VDR_PFB = Product.GetParameter("EOL_VDR_PFB");
            Parameter param_VDR_PFC = Product.GetParameter("EOL_VDR_PFC");
            Parameter param_VDR_PFD = Product.GetParameter("EOL_VDR_PFD");

            // Stack Voltage Parameter
            DirectRam.ReadAll();
            decimal param_MeasuredStackVoltage = DirectRam.Stack_Voltage;
            decimal param_CellVoltage = param_MeasuredStackVoltage / 10;

            decimal packPinVoltageDev = DirectRam.PACK_Pin_Voltage - param_MeasuredStackVoltage;
            decimal ldPinVoltageDev = DirectRam.LD_Pin_Voltage - param_MeasuredStackVoltage;


            // TempDeviation without Internal Temperature
            decimal tempDeviation =
                CalculateDecimalDeviation(false,
                DirectRam.TS1_Temperature,
                DirectRam.TS2_Temperature,
                DirectRam.DCHG_Temperature,
                DirectRam.DDSG_Temperature);

            partialResult =
                tempDeviation <= (decimal)param_VDR_TempDevWoInt.Max &&
                tempDeviation >= (decimal)param_VDR_TempDevWoInt.Min;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_TempDevWoInt, (double)tempDeviation, null, partialResult);

            // Avg of TS1,TS2,DCHG,DSG temp vs. Internal Temperature
            decimal avgTemp =
                (DirectRam.TS1_Temperature +
                DirectRam.TS2_Temperature +
                DirectRam.DCHG_Temperature +
                DirectRam.DDSG_Temperature) / 4;
            decimal absDev = Math.Abs(avgTemp - DirectRam.Int_Temperature);

            partialResult =
                absDev <= (decimal)param_VDR_TempDeviation.Max &&
                absDev >= (decimal)param_VDR_TempDeviation.Min;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_TempDeviation, (double)absDev, null, partialResult);

            // Internal Temperature
            partialResult =
                DirectRam.Int_Temperature >= (decimal)param_VDR_IntTemp.Min &&
                DirectRam.Int_Temperature <= (decimal)param_VDR_IntTemp.Max;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_IntTemp, (double)DirectRam.Int_Temperature, null, partialResult);

            // CC2 Current
            partialResult =
                DirectRam.CC2_Current <= (decimal)param_VDR_CC2Current.Max &&
                DirectRam.CC2_Current >= (decimal)param_VDR_CC2Current.Min;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_CC2Current, (double)DirectRam.CC2_Current, null, partialResult);

            // DSG_FET
            partialResult = param_VDR_DSG_FET_Status.Nominal == (DirectRam.FET_Status.DSG_FET ? 1 : 0);
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_DSG_FET_Status, DirectRam.FET_Status.DSG_FET ? 1 : 0, null, partialResult);

            // CHG_FET
            partialResult = param_VDR_CHG_FET_Status.Nominal == (DirectRam.FET_Status.CHG_FET ? 1 : 0);
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_CHG_FET_Status, DirectRam.FET_Status.CHG_FET ? 1 : 0, null, partialResult);

            // Stack Voltage
            partialResult =
                DirectRam.Stack_Voltage >= (decimal)param_VDR_StackVoltage.Min &&
                DirectRam.Stack_Voltage <= (decimal)param_VDR_StackVoltage.Max;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_StackVoltage, (double)DirectRam.Stack_Voltage, null, partialResult);

            // PACK Pin Voltage
            partialResult =
                packPinVoltageDev >= (decimal)param_VDR_PackPinVoltageDev.Min &&
                packPinVoltageDev <= (decimal)param_VDR_PackPinVoltageDev.Max;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_PackPinVoltageDev, (double)packPinVoltageDev, null, partialResult);

            // LD Pin Voltage
            partialResult =
                ldPinVoltageDev >= (decimal)param_VDR_LDPinVoltageDev.Min &&
                ldPinVoltageDev <= (decimal)param_VDR_LDPinVoltageDev.Max;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_LDPinVoltageDev, (double)ldPinVoltageDev, null, partialResult);

            // Cell Voltages
            decimal meas_CellVoltage;
            for (int i = 1; i <= 10; i++)
            {
                meas_CellVoltage = DirectRam.GetCellVoltage(i);
                partialResult =
                    (meas_CellVoltage - param_CellVoltage) >= (decimal)param_VDR_CellVoltageDev.Min &&
                    (meas_CellVoltage - param_CellVoltage) <= (decimal)param_VDR_CellVoltageDev.Max;
                result &= partialResult;
                ManageTestDetail($"{param_VDR_CellVoltageDev.Name}({i})", i, param_VDR_CellVoltageDev, (double)(meas_CellVoltage - param_CellVoltage), null, partialResult);
            }

            // Safety Status A
            partialResult = DirectRam.Safety_Status_A.Data == param_VDR_SSA.Nominal;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_SSA, DirectRam.Safety_Status_A.Data, null, partialResult);
            if (!partialResult)
            {
                string[] ssaCode = { "RSVD0_0", "RSVD1_0", "CUV", "COV", "OCC", "OCD1", "OCD2", "SCD" };
                StoreRegisterBitErrors(testStep, DirectRam.Safety_Status_A, ssaCode, "SSA");
            }

            // Safety Status B
            partialResult = DirectRam.Safety_Status_B.Data == param_VDR_SSB.Nominal;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_SSB, DirectRam.Safety_Status_B.Data, null, partialResult);
            if (!partialResult)
            {
                string[] ssbCode = { "UTC", "UTD", "UTINT", "RSVD3_0", "OTC", "OTD", "OTINT", "OTF" };
                StoreRegisterBitErrors(testStep, DirectRam.Safety_Status_B, ssbCode, "SSB");
            }

            // Safety Status C
            partialResult = DirectRam.Safety_Status_C.Data == param_VDR_SSC.Nominal;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_SSC, DirectRam.Safety_Status_C.Data, null, partialResult);
            if (!partialResult)
            {
                string[] sscCode = { "RSVD0_0", "HWDF", "PTO", "RSVD3_0", "COVL", "OCDL", "SCDL", "OCD3" };
                StoreRegisterBitErrors(testStep, DirectRam.Safety_Status_C, sscCode, "SSC");
            }

            // PF Status A
            partialResult = DirectRam[(int)DirectRamRegister.PF_Status_A] == param_VDR_PFA.Nominal;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_PFA, DirectRam[(int)DirectRamRegister.PF_Status_A], null, partialResult);
            if (!partialResult)
            {
                string[] pfaCode = { "SUV", "SOV", "SOCC", "SOCD", "SOT", "RSVD5_0", "SOTF", "CUDEP" };
                StoreRegisterBitErrors(testStep, DirectRam.PF_Status_A, pfaCode, "PFA");
            }

            // PF Status B
            partialResult = DirectRam[(int)DirectRamRegister.PF_Status_B] == param_VDR_PFB.Nominal;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_PFB, DirectRam[(int)DirectRamRegister.PF_Status_B], null, partialResult);
            if (!partialResult)
            {
                string[] pfbCode = { "CFETF", "DFETF", "_2LVL", "VIMR", "VIMA", "RSVD5_0", "RSVD6_0", "SCDL" };
                StoreRegisterBitErrors(testStep, DirectRam.PF_Status_B, pfbCode, "PFB");
            }

            // PF Status C
            partialResult = DirectRam[(int)DirectRamRegister.PF_Status_C] == param_VDR_PFC.Nominal;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_PFC, DirectRam[(int)DirectRamRegister.PF_Status_C], null, partialResult);
            if (!partialResult)
            {
                string[] pfcCode = { "OTPF", "DRMF", "IRMF", "LFOF", "VREF", "VSSF", "HWMX", "CMDF" };
                StoreRegisterBitErrors(testStep, DirectRam.PF_Status_C, pfcCode, "PFC");
            }

            // PF Status D
            partialResult = DirectRam[(int)DirectRamRegister.PF_Status_D] == param_VDR_PFD.Nominal;
            result &= partialResult;
            ManageTestDetail(null, 1, param_VDR_PFD, DirectRam[(int)DirectRamRegister.PF_Status_D], null, partialResult);
            if (!partialResult)
            {
                string[] pfdCode = { "TOSF", "RSVD1_0", "RSVD2_0", "RSVD3_0", "RSVD4_0", "RSVD5_0", "RSVD6_0", "RSVD7_0" };
                StoreRegisterBitErrors(testStep, DirectRam.PF_Status_D, pfdCode, "PFD");
            }

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool TestOCD(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.TestOCD;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            Property prop_OCDProtectionTimer = GetPropertyFromTrace("EOL_OCD_PROTECTION_TIMER");
            Property prop_OCDRecoveryTimer = GetPropertyFromTrace("EOL_OCD_RECOVERY_TIMER");

            Parameter param_OCDInitState = Product.GetParameter("EOL_OCD_InitState");
            Parameter param_OCDOnState = Product.GetParameter("EOL_OCD_OnState");
            Parameter param_OCDEndState = Product.GetParameter("EOL_OCD_EndState");
            Parameter param_OCDTimeOn = Product.GetParameter("EOL_OCD_TimeOn");
            Parameter param_OCDTimeOff = Product.GetParameter("EOL_OCD_TimeOff");

            double timeoutOnValue = PropertyStringToDouble(prop_OCDProtectionTimer) / 1000;
            double timeoutOffValue = PropertyStringToDouble(prop_OCDRecoveryTimer) / 1000;

            // Enable or Disable BMS EventHandler
            bool sendPropertyChangedOriginal = BMS.SendPropertyChanged;
            bool sendPropertyChangedCurrent = SendPropertyChangedEventHandler;
            bool partialResultOn, timeoutOn, partialResultOff, timeoutOff;
            string partialResultStringValue, partialResultOnStringValue, partialResultOffStringValue;
            decimal maxCC2Abs = 0;

            Stopwatch stopwatchOn = new Stopwatch();
            Stopwatch stopwatchOff = new Stopwatch();

            FET_Status fetStatus = DirectRam.FET_Status;
            Safety_Status_C sscStatus = DirectRam.Safety_Status_C;
            BMS.SendPropertyChanged = sendPropertyChangedCurrent;

            // Check initial condition
            ReadDirectRamCustomBlocks();
            partialResult = fetStatus.CHG_FET && fetStatus.DSG_FET && !sscStatus.OCDL;
            partialResultStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET};OCDL={sscStatus.OCDL}";
            result &= partialResult;
            ManageTestDetail("OCD Initial", 1, param_OCDInitState, partialResult ? 1 : 0, partialResultStringValue, partialResult);

            if (!result)
            {
                ErrorCode = new ErrorCode("OCD", $"Nem állnak fenn a kezdeti feltételek az OCD teszthez: {partialResultStringValue}");
                AddErrorInfo(testStep, "OCDInitError", partialResultStringValue);
                return result;
            }

            // TEST
            // ON phase
            // Turn on 1 Ohm relay
            CommInterface.RelayStatus = RelayStatus.Load1Ohm;

            // Timer for timeoutOn
            CustomTimer.Start(timeoutOnValue);
            stopwatchOn.Start();
            while (true)
            {
                ReadDirectRamCustomBlocks();
                timeoutOn = CustomTimer.TimeOut ? true : false;
                partialResultOn = fetStatus.CHG_FET && !fetStatus.DSG_FET && sscStatus.OCDL;
                partialResultOnStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET};OCDL={sscStatus.OCDL}";
                maxCC2Abs = Math.Abs(DirectRam.CC2_Current) > maxCC2Abs ? Math.Abs(DirectRam.CC2_Current) : maxCC2Abs;
                if (timeoutOn || partialResultOn)
                {
                    stopwatchOn.Stop();
                    break;
                }
            }
            // OFF phase
            // Turn off 1 Ohm relay
            CommInterface.RelayStatus = RelayStatus.AllOFF;

            // Timer for timeoutOff
            CustomTimer.Start(timeoutOffValue);
            stopwatchOff.Start();
            while (true)
            {
                ReadDirectRamCustomBlocks();
                timeoutOff = CustomTimer.TimeOut ? true : false;
                partialResultOff = fetStatus.CHG_FET && fetStatus.DSG_FET && !sscStatus.OCDL;
                partialResultOffStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET};OCDL={sscStatus.OCDL}";
                if (timeoutOff || partialResultOff)
                {
                    stopwatchOff.Stop();
                    break;
                }
            }

            // Only for Info
            ManageTestDetail("OCD MaxCC2Current", "", "", "", "A", maxCC2Abs.ToString(), true);

            // Timeout On
            partialResult = !timeoutOn;
            result &= partialResult;
            ManageTestDetail(null, 1, param_OCDTimeOn, stopwatchOn.ElapsedMilliseconds, null, partialResult);

            // State On
            partialResult = !timeoutOn && partialResultOn;
            result &= partialResult;
            ManageTestDetail(null, 1, param_OCDOnState, partialResult ? 1 : 0, partialResultOnStringValue, partialResult);

            // Timeout Off
            partialResult = !timeoutOff;
            result &= partialResult;
            ManageTestDetail(null, 1, param_OCDTimeOff, stopwatchOff.ElapsedMilliseconds, null, partialResult);

            // State Off
            partialResult = !timeoutOff && partialResultOff;
            result &= partialResult;
            ManageTestDetail(null, 1, param_OCDEndState, partialResult ? 1 : 0, partialResultOffStringValue, partialResult);

            BMS.SendPropertyChanged = sendPropertyChangedOriginal;
            DirectRam.ReadAll();
            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool TestSCD(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.TestSCD;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            Property prop_SCDProtectionTimer = GetPropertyFromTrace("EOL_SCD_PROTECTION_TIMER");
            Property prop_SCDRecoveryTimer = GetPropertyFromTrace("EOL_SCD_RECOVERY_TIMER");

            Parameter param_SCDInitState = Product.GetParameter("EOL_SCD_InitState");
            Parameter param_SCDOnState = Product.GetParameter("EOL_SCD_OnState");
            Parameter param_SCDEndState = Product.GetParameter("EOL_SCD_EndState");
            Parameter param_SCDTimeOn = Product.GetParameter("EOL_SCD_TimeOn");
            Parameter param_SCDTimeOff = Product.GetParameter("EOL_SCD_TimeOff");

            // ms -> sec
            double timeoutOnValue = PropertyStringToDouble(prop_SCDProtectionTimer) / 1000;
            double timeoutOffValue = PropertyStringToDouble(prop_SCDRecoveryTimer) / 1000;

            // Enable or Disable BMS EventHandler
            bool sendPropertyChangedOriginal = BMS.SendPropertyChanged;
            bool sendPropertyChangedCurrent = SendPropertyChangedEventHandler;
            bool partialResultOn, timeoutOn, partialResultOff, timeoutOff;
            string partialResultStringValue, partialResultOnStringValue, partialResultOffStringValue;
            decimal maxCC2Abs = 0;

            Stopwatch stopwatchOn = new Stopwatch();
            Stopwatch stopwatchOff = new Stopwatch();

            FET_Status fetStatus = DirectRam.FET_Status;
            Safety_Status_C sscStatus = DirectRam.Safety_Status_C;
            BMS.SendPropertyChanged = sendPropertyChangedCurrent;

            // Check initial condition
            ReadDirectRamCustomBlocks();
            partialResult = fetStatus.CHG_FET && fetStatus.DSG_FET && !sscStatus.SCDL;
            partialResultStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET};SCDL={sscStatus.SCDL}";
            result &= partialResult;
            ManageTestDetail("SCD Initial", 1, param_SCDInitState, partialResult ? 1 : 0, partialResultStringValue, partialResult);

            if (!result)
            {
                ErrorCode = new ErrorCode("SCD_INIT", "Nem állnak fenn a kezdeti feltételek az SCD teszthez!");
                AddErrorInfo(testStep, "SCDInitError", partialResultStringValue);
                return result;
            }

            // TEST
            // ON phase
            // Turn on 0,5 Ohm relay
            CommInterface.RelayStatus = RelayStatus.Load0_5Ohm;

            // Timer for timeoutOn
            CustomTimer.Start(timeoutOnValue);
            stopwatchOn.Start();
            while (true)
            {
                ReadDirectRamCustomBlocks();
                timeoutOn = CustomTimer.TimeOut ? true : false;
                partialResultOn = fetStatus.CHG_FET && !fetStatus.DSG_FET && sscStatus.SCDL;
                partialResultOnStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET};SCDL={sscStatus.SCDL}";
                maxCC2Abs = Math.Abs(DirectRam.CC2_Current) > maxCC2Abs ? Math.Abs(DirectRam.CC2_Current) : maxCC2Abs;

                if (timeoutOn || partialResultOn)
                {
                    stopwatchOn.Stop();
                    break;
                }
            }

            // OFF phase
            // Turn off 0,5 Ohm relay
            CommInterface.RelayStatus = RelayStatus.AllOFF;
            // Timer for timeoutOff
            CustomTimer.Start(timeoutOffValue);
            stopwatchOff.Start();

            while (true)
            {
                ReadDirectRamCustomBlocks();
                timeoutOff = CustomTimer.TimeOut ? true : false;
                partialResultOff = fetStatus.CHG_FET && fetStatus.DSG_FET && !sscStatus.SCDL;
                partialResultOffStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET};SCDL={sscStatus.SCDL}";

                if (timeoutOff || partialResultOff)
                {
                    stopwatchOff.Stop();
                    break;
                }
            }

            // Only for Info
            ManageTestDetail("SCD MaxCC2Current", "", "", "", "A", maxCC2Abs.ToString(), true);

            // Timeout On
            partialResult = !timeoutOn;
            result &= partialResult;
            ManageTestDetail(null, 1, param_SCDTimeOn, stopwatchOn.ElapsedMilliseconds, null, partialResult);

            // State On
            partialResult = !timeoutOn && partialResultOn;
            result &= partialResult;
            ManageTestDetail(null, 1, param_SCDOnState, partialResult ? 1 : 0, partialResultOnStringValue, partialResult);

            // Timeout Off
            partialResult = !timeoutOff;
            result &= partialResult;
            ManageTestDetail(null, 1, param_SCDTimeOff, stopwatchOff.ElapsedMilliseconds, null, partialResult);

            // State Off
            partialResult = !timeoutOff && partialResultOff;
            result &= partialResult;
            ManageTestDetail(null, 1, param_SCDEndState, partialResult ? 1 : 0, partialResultOffStringValue, partialResult);

            BMS.SendPropertyChanged = sendPropertyChangedOriginal;
            DirectRam.ReadAll();

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool TestDSG2Ohm(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.TestDSG2Ohm;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            #region TestParameters
            Property prop_DSGDischargeTimer = GetPropertyFromTrace("EOL_DSG_DISCHARGE_TIMER");

            Parameter param_DSGTimeOn = Product.GetParameter("EOL_DSG_TimeOn");
            Parameter param_DSGInitState = Product.GetParameter("EOL_DSG_InitState");
            Parameter param_DSGOnState = Product.GetParameter("EOL_DSG_OnState");
            Parameter param_DSGCC2Current = Product.GetParameter("EOL_DSG_CC2Current");
            Parameter param_DSG_DDSGTemp = Product.GetParameter("EOL_DSG_DDSGTemp");
            Parameter param_DSG_DCHGTemp = Product.GetParameter("EOL_DSG_DCHGTemp");

            double timeoutOnValue = PropertyStringToDouble(prop_DSGDischargeTimer) / 1000;

            bool sendPropertyChangedOriginal = BMS.SendPropertyChanged;
            bool sendPropertyChangedCurrent = SendPropertyChangedEventHandler;

            string partialResultStringValue, partialResultOnStringValue;
            bool timeoutOn, partialResultOn;

            decimal tempDDSGbefore, tempDDSGafter, tempDCHGbefore, tempDCHGafter;
            decimal CC2CurrentMin = 0;
            #endregion

            Stopwatch stopwatch = new Stopwatch();
            FET_Status fetStatus = DirectRam.FET_Status;
            BMS.SendPropertyChanged = sendPropertyChangedCurrent;

            // Check initial condition
            DirectRam.ReadAll();
            partialResult = fetStatus.CHG_FET && fetStatus.DSG_FET;
            partialResultStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET}";
            result &= partialResult;
            ManageTestDetail("DSG Initial", 1, param_DSGInitState, partialResult ? 1 : 0, partialResultStringValue, partialResult);

            if (!result)
            {
                ErrorCode = new ErrorCode("DSG_INIT", "Nem állnak fenn a kezdeti feltételek a DSG teszthez!");
                AddErrorInfo(testStep, "DSGInitError", partialResultStringValue);
                return result;
            }

            // Store temp data before test
            tempDCHGbefore = DirectRam.DCHG_Temperature;
            tempDDSGbefore = DirectRam.DDSG_Temperature;

            // ON phase
            // Turn on 2 Ohm relay
            CommInterface.RelayStatus = RelayStatus.Load2Ohm;

            CustomTimer.Start(timeoutOnValue);
            stopwatch.Start();
            TimeSpan elapsed;
            // DSG ciklus
            while (true)
            {
                ReadDirectRamCustomBlocks();
                Subcommands.DASTATUS6.Read();
                timeoutOn = CustomTimer.TimeOut ? true : false;
                partialResultOn = fetStatus.CHG_FET || fetStatus.DSG_FET;
                partialResultOnStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET}";
                CC2CurrentMin = DirectRam.CC2_Current < CC2CurrentMin ? DirectRam.CC2_Current : CC2CurrentMin;
                elapsed = DateTime.Now - CustomTimer.StartTime;

                Message = new GeneralMessage($"DSG time: {elapsed.ToString("mm\\:ss")}, Acc.ch: {Subcommands.DASTATUS6.Accum_Charge} mAh");

                if (timeoutOn || !partialResultOn)
                {
                    stopwatch.Stop();
                    break;
                }
            }

            // DSG Relay OFF
            CommInterface.RelayStatus = RelayStatus.AllOFF;

            // Eredmények
            DirectRam.ReadAll();
            tempDCHGafter = DirectRam.DCHG_Temperature;
            tempDDSGafter = DirectRam.DDSG_Temperature;

            // CC2 Current Min
            partialResult =
                (double)CC2CurrentMin <= param_DSGCC2Current.Max &&
                (double)CC2CurrentMin >= param_DSGCC2Current.Min;
            result &= partialResult;
            ManageTestDetail(null, 1, param_DSGCC2Current, (double)CC2CurrentMin, null, partialResult);

            // Timeout On
            partialResult = timeoutOn && stopwatch.ElapsedMilliseconds >= param_DSGTimeOn.Min;
            result &= partialResult;
            ManageTestDetail(null, 1, param_DSGTimeOn, stopwatch.ElapsedMilliseconds, null, partialResult);

            // State On
            partialResult = timeoutOn && partialResultOn;
            result &= partialResult;
            ManageTestDetail(null, 1, param_DSGOnState, partialResult ? 1 : 0, partialResultOnStringValue, partialResult);

            // FET Temperature
            double DDSGTempDev = (double)(tempDDSGafter - tempDDSGbefore);
            partialResult =
                DDSGTempDev >= param_DSG_DDSGTemp.Min &&
                DDSGTempDev <= param_DSG_DDSGTemp.Max;
            result &= partialResult;
            ManageTestDetail(null, 1, param_DSG_DDSGTemp, DDSGTempDev, null, partialResult);

            // SHUNT Temperature
            double DCHGTempDev = (double)(tempDCHGafter - tempDCHGbefore);
            partialResult =
                DCHGTempDev >= param_DSG_DCHGTemp.Min &&
                DCHGTempDev <= param_DSG_DCHGTemp.Max;
            result &= partialResult;
            ManageTestDetail(null, 1, param_DSG_DCHGTemp, DCHGTempDev, null, partialResult);

            BMS.SendPropertyChanged = sendPropertyChangedOriginal;
            DirectRam.ReadAll();

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool TestCHG3A(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.TestCHG3A;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            #region TestParameters
            // Parameters & Properties
            Property prop_EOL_CHG_Voltage = GetPropertyFromTrace("EOL_CHG_Voltage");
            Property prop_EOL_CHG_CurrentLimit = GetPropertyFromTrace("EOL_CHG_CurrentLimit");
            Property prop_EOL_CHG_ChargeTimer = GetPropertyFromTrace("EOL_CHG_CHARGE_TIMER");

            Parameter param_AbsCC2CurrentDev = Product.GetParameter("EOL_CHG_AbsCC2CurrentDev");
            Parameter param_CHGInitState = Product.GetParameter("EOL_CHG_InitState");
            Parameter param_CHGOnState = Product.GetParameter("EOL_CHG_OnState");
            Parameter param_CHGTimeOn = Product.GetParameter("EOL_CHG_TimeOn");
            Parameter param_CHGAccChargeBefore = Product.GetParameter("EOL_CHG_AccChargeBefore");
            Parameter param_CHGAccChargeAfter = Product.GetParameter("EOL_CHG_AccChargeAfter");

            // Local variables
            int timeoutOnValue = PropertyStringToInt(prop_EOL_CHG_ChargeTimer);
            double ichg = PropertyStringToDouble(prop_EOL_CHG_CurrentLimit);
            decimal AbsCC2CurrentDevMax = 0;
            string partialResultStringValue, partialResultOnStringValue;
            bool timeoutOn, partialResultOn;

            bool CC2OutOfRange = false;
            bool accumCharge = false;

            Stopwatch stopwatch = new Stopwatch();
            FET_Status fetStatus = DirectRam.FET_Status;
            #endregion

            // Check Initial conditions
            ReadDirectRamCustomBlocks();
            partialResult = fetStatus.CHG_FET && fetStatus.DSG_FET;
            partialResultStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET}";
            result &= partialResult;
            ManageTestDetail("CHG Initial", 1, param_CHGInitState, partialResult ? 1 : 0, partialResultStringValue, partialResult);

            if (!result)
            {
                ErrorCode = new ErrorCode("CHG_INIT", "Nem állnak fenn a kezdeti feltételek a CHG teszthez!");
                AddErrorInfo(testStep, "CHGInitError", partialResultStringValue);
                return result;
            }

            // MANU_DATA.DASTATUS6 AccumCharge Before Charge
            Subcommands.DASTATUS6.Read();
            int accChCurrentValue = Subcommands.DASTATUS6.Accum_Charge;
            partialResult =
                accChCurrentValue <= param_CHGAccChargeBefore.Max &&
                accChCurrentValue >= param_CHGAccChargeBefore.Min;
            ManageTestDetail(null, 1, param_CHGAccChargeBefore, accChCurrentValue, null, partialResult);

            //Setup PowerSupply
            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.SetVoltage), prop_EOL_CHG_Voltage.Value);
                CPX.Command(CPX.GetCommand(Cpx400Function.SetCurrentLimit), prop_EOL_CHG_CurrentLimit.Value);
                // Switch on Power Supply
                CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "1");
                Thread.Sleep(100);
            }
            // Switch on Charge Relay
            CommInterface.RelayStatus = RelayStatus.Charge;

            CustomTimer.Start(timeoutOnValue);
            stopwatch.Start();
            TimeSpan elapsed;
            // TEST
            while (true)
            {
                ReadDirectRamCustomBlocks();
                Subcommands.DASTATUS6.Read();
                timeoutOn = CustomTimer.TimeOut ? true : false;
                partialResultOn = fetStatus.CHG_FET || fetStatus.DSG_FET;
                partialResultOnStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET}";
                accumCharge = Subcommands.DASTATUS6.Accum_Charge >= 0;
                elapsed = DateTime.Now - CustomTimer.StartTime;

                Message = new GeneralMessage($"CHG time: {elapsed.ToString("mm\\:ss")}, Acc.ch: {Subcommands.DASTATUS6.Accum_Charge} mAh");

                if (elapsed.TotalSeconds > 2)
                {
                    AbsCC2CurrentDevMax = Math.Abs(DirectRam.CC2_Current - (decimal)ichg);
                    CC2OutOfRange = AbsCC2CurrentDevMax > (decimal)param_AbsCC2CurrentDev.Max;
                }

                if (timeoutOn || !partialResultOn || accumCharge || CC2OutOfRange)
                {
                    stopwatch.Stop();
                    break;
                }
            }

            // Turn off CHG Relay and Power Supply
            CommInterface.RelayStatus = RelayStatus.AllOFF;

            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "0");
            }

            // Timeout On
            partialResult = !timeoutOn && stopwatch.Elapsed.Seconds <= param_CHGTimeOn.Max;
            result &= partialResult;
            ManageTestDetail(null, 1, param_CHGTimeOn, stopwatch.Elapsed.Seconds, null, partialResult);

            // State On
            partialResult = partialResultOn;
            result &= partialResult;
            ManageTestDetail(null, 1, param_CHGOnState, partialResult ? 1 : 0, partialResultOnStringValue, partialResult);

            // Max CC2 Dev
            partialResult = !CC2OutOfRange;
            result &= partialResult;
            ManageTestDetail(null, 1, param_AbsCC2CurrentDev, (double)AbsCC2CurrentDevMax, null, partialResult);

            // AccCh After
            accChCurrentValue = Subcommands.DASTATUS6.Accum_Charge;
            partialResult =
                accChCurrentValue <= param_CHGAccChargeAfter.Max &&
                accChCurrentValue >= param_CHGAccChargeAfter.Min;
            ManageTestDetail(null, 1, param_CHGAccChargeAfter, accChCurrentValue, null, partialResult);

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool TestOCC(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.TestOCC;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult;
            #endregion

            Property prop_OCCProtectionTimer = GetPropertyFromTrace("EOL_OCC_PROTECTION_TIMER");
            Property prop_OCCRecoveryTimer = GetPropertyFromTrace("EOL_OCC_RECOVERY_TIMER");
            Property prop_OCCOverCurrentLimit = GetPropertyFromTrace("EOL_OCC_OVERCURRENT_LIMIT");
            Property prop_CHGVoltage = GetPropertyFromTrace("EOL_CHG_Voltage");

            Parameter param_OCCInitState = Product.GetParameter("EOL_OCC_InitState");
            Parameter param_OCCOnState = Product.GetParameter("EOL_OCC_OnState");
            Parameter param_OCCEndState = Product.GetParameter("EOL_OCC_EndState");
            Parameter param_OCCTimeOn = Product.GetParameter("EOL_OCC_TimeOn");
            Parameter param_OCCTimeOff = Product.GetParameter("EOL_OCC_TimeOff");

            double timeoutOnValue = PropertyStringToDouble(prop_OCCProtectionTimer) / 1000;
            double timeoutOffValue = PropertyStringToDouble(prop_OCCRecoveryTimer) / 1000;

            bool partialResultOn, timeoutOn, partialResultOff, timeoutOff;
            string partialResultStringValue, partialResultOnStringValue, partialResultOffStringValue;

            bool sendPropertyChangedOriginal = BMS.SendPropertyChanged;
            bool sendPropertyChangedCurrent = SendPropertyChangedEventHandler;

            Stopwatch stopwatchOn = new Stopwatch();
            Stopwatch stopwatchOff = new Stopwatch();

            FET_Status fetStatus = DirectRam.FET_Status;
            Safety_Status_A ssaStatus = DirectRam.Safety_Status_A;
            BMS.SendPropertyChanged = sendPropertyChangedCurrent;

            // Check initial condition
            DirectRam.ReadAll();
            partialResult = fetStatus.CHG_FET && fetStatus.DSG_FET && !ssaStatus.OCC;
            partialResultStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET};OCC={ssaStatus.OCC}";
            result &= partialResult;
            ManageTestDetail(null, 1, param_OCCInitState, partialResult ? 1 : 0, partialResultStringValue, partialResult);

            if (!result)
            {
                ErrorCode = new ErrorCode("OCC_INIT", "Nem állnak fenn a kezdeti feltételek az OCC teszthez!");
                AddErrorInfo(testStep, "OCCInitError", partialResultStringValue);
                return result;
            }

            //Setup PowerSupply
            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.SetVoltage), prop_CHGVoltage.Value);
                Thread.Sleep(50);
                CPX.Command(CPX.GetCommand(Cpx400Function.SetCurrentLimit), prop_OCCOverCurrentLimit.Value);
                Thread.Sleep(50);
                CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "1");
                Thread.Sleep(50);
            }

            // ON phase
            // Turn on Charge relay
            CommInterface.RelayStatus = RelayStatus.Charge;

            // Timer for timeoutOn
            CustomTimer.Start(timeoutOnValue);
            stopwatchOn.Start();
            while (true)
            {
                ReadDirectRamCustomBlocks();
                timeoutOn = CustomTimer.TimeOut ? true : false;
                partialResultOn = fetStatus.DSG_FET && !fetStatus.CHG_FET && ssaStatus.OCC;
                partialResultOnStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET};OCC={ssaStatus.OCC}";

                if (timeoutOn || partialResultOn)
                {
                    stopwatchOn.Stop();
                    break;
                }
            }

            // OFF phase
            // Turn off Charge relay
            CommInterface.RelayStatus = RelayStatus.AllOFF;

            // Timers
            CustomTimer.Start(timeoutOffValue);
            stopwatchOff.Start();

            // DSG 2Ohm Relay for 1 sec
            CommInterface.RelayStatus = RelayStatus.Load2Ohm;
            Thread.Sleep(2000);
            CommInterface.RelayStatus = RelayStatus.AllOFF;

            while (true)
            {
                DirectRam.ReadAll();
                timeoutOff = CustomTimer.TimeOut ? true : false;
                partialResultOff = fetStatus.DSG_FET && fetStatus.CHG_FET && !ssaStatus.OCC;

                partialResultOff = fetStatus.CHG_FET && fetStatus.DSG_FET && !ssaStatus.OCC;
                partialResultOffStringValue = $"CHG={fetStatus.CHG_FET};DSG={fetStatus.DSG_FET};OCC={ssaStatus.OCC}";

                if (timeoutOff || partialResultOff)
                {
                    stopwatchOff.Stop();
                    break;
                }
            }

            //Tápegység kikapcsol
            lock (CPX)
            {
                CPX.Command(CPX.GetCommand(Cpx400Function.SetOutput), "0");
            }

            // Timeout On
            partialResult = !timeoutOn;
            result &= partialResult;
            ManageTestDetail(null, 1, param_OCCTimeOn, stopwatchOn.ElapsedMilliseconds, null, partialResult);

            // State On
            partialResult = !timeoutOn && partialResultOn;
            result &= partialResult;
            ManageTestDetail(null, 1, param_OCCOnState, partialResult ? 1 : 0, partialResultOnStringValue, partialResult);

            // Timeout Off
            partialResult = !timeoutOff;
            result &= partialResult;
            ManageTestDetail(null, 1, param_OCCTimeOff, stopwatchOff.ElapsedMilliseconds, null, partialResult);

            // State Off
            partialResult = !timeoutOff && partialResultOff;
            result &= partialResult;
            ManageTestDetail(null, 1, param_OCCEndState, partialResult ? 1 : 0, partialResultOffStringValue, partialResult);

            BMS.SendPropertyChanged = sendPropertyChangedOriginal;
            DirectRam.ReadAll();

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool UpdateMANU_DATA(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.UpdateManuData;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult = true;
            #endregion

            Parameter param_EOL_UMD_State = Product.GetParameter("EOL_UMD_State");
            Subcommands.MANU_DATA.Read();
            States state = ((VtepMANU_DATA)Subcommands.MANU_DATA).State;

            if (state == States.EolTest)
            {
                ManageTestDetail("UMD State Before", 1, param_EOL_UMD_State, null, state.ToString(), true);
                return true;
            }

            DataMemory.ReadAll();
            DataMemory.CopyReadToWrite();
            Thread.Sleep(1000);
            DataMemory.Settings__Manufacturing__Mfg_Status_Init.OTPW_EN = true;
            DataMemory.WriteChanges();
            Subcommands.SET_CFGUPDATE.Send();
            Subcommands.MANU_DATA.Read();
            ((VtepMANU_DATA)Subcommands.MANU_DATA).State = States.EolTest;
            Thread.Sleep(250);
            Subcommands.MANU_DATA.Write();
            Thread.Sleep(250);
            Subcommands.EXIT_CFGUPDATE.Send();
            Thread.Sleep(1500);
            Subcommands.RESET.Send();
            Thread.Sleep(1500);
            Subcommands.MANU_DATA.Read();
            state = ((VtepMANU_DATA)Subcommands.MANU_DATA).State;
            DirectRam.ReadAll();
            partialResult = (state == States.EolTest);
            result &= partialResult;
            ManageTestDetail("UMD State After", 1, param_EOL_UMD_State, null, state.ToString(), partialResult);

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public bool Shutdown(TestStep testNumber)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.Shutdown;
            if ((testNumber & testStep) == TestStep.None)
            {
                return true;
            }
            // Test Init
            Operation = $"Teszt - {testStep}";
            bool result = true;
            bool partialResult = false;
            #endregion

            Parameter param_EOL_SHT_State = Product.GetParameter("EOL_SHT_State");
            Property prop_SHTTimeOut = GetPropertyFromTrace("EOL_SHT_TIMEOUT");
            double shutDownTimeOut = PropertyStringToDouble(prop_SHTTimeOut);
            CustomTimer.Start(shutDownTimeOut);

            Subcommands.SHUTDOWN.Send();
            Thread.Sleep(3000);
            while (!CustomTimer.TimeOut)
            {
                try
                {
                    DirectRam.ReadAll();
                }
                catch (Exception)
                {
                    partialResult = true;
                    break;
                }
            }

            result &= partialResult;
            ManageTestDetail(null, 1, param_EOL_SHT_State, partialResult ? 1 : 0, null, partialResult);

            Message = new GeneralMessage($"{testStep} result: {(result ? "OK" : "NOK")}");
            return result;
        }

        public void PrintLabel(TestStep testNumber, bool cont, DateTime endDate)
        {
            #region TestHeader
            // Header
            TestStep testStep = TestStep.PrintLabel;
            if ((testNumber & testStep) == TestStep.None)
            {
                return;
            }
            // Test Init
            Operation = $"Címke nyomtatás";
            //bool result = true;
            //bool partialResult = false;
            #endregion
            string okLabelPrn = Settings.Default.LabelOKPrn;
            string nokLabelPrn = Settings.Default.LabelNOKPrn;

            if (!cont)
            {
                bool needErrorLabel = MessageBox.Show("Sikertelen teszt, akar hibacímkét nyomtatni?", "Megerősítés!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes ? true : false;
                if (!needErrorLabel)
                {
                    return;
                }
                string textToPrint = ReadPrnFromFile(nokLabelPrn);
                if (string.IsNullOrEmpty(textToPrint))
                {
                    CZP.Message = "Invalid .prn file";
                    return;
                }
                textToPrint = textToPrint.Replace("%serial%", Product.TraceProduct.SerialNumber);
                textToPrint = textToPrint.Replace("%eoldate%", endDate.ToString("yyyy-MM-dd HH:mm:ss"));
                textToPrint = textToPrint.Replace("%trid%", Product.TraceProduct.ProductID);
                string sample1 = @"^FT";
                int start = 185;
                string sample2 = @",376^A0B,17,18^FH\^CI28^FD";
                string sample3 = @"^FS^CI27";
                string testDetailsString = "";

                int counter = 0;
                foreach (var det in listOfErrorDetails)
                {
                    if (!det.Passed)
                    {
                        testDetailsString += sample1 + (start + counter++ * 18).ToString() + sample2;
                        testDetailsString += $"{det.ParamName}={det.ResultValue} {det.UnitOfMeasure}";
                        testDetailsString += sample3 + Environment.NewLine;
                        testDetailsString += sample1 + (start + counter++ * 18).ToString() + sample2;
                        testDetailsString += $"\t(min:{det.Min}, max:{det.Max}, nom:{det.Nominal})";
                        testDetailsString += sample3 + Environment.NewLine;
                    }
                }

                textToPrint = textToPrint.Replace("%testdetails%", testDetailsString);
                CZP.PrintUSBTask(textToPrint);
            }
            else
            {
                Parameter param_EOL_LBL_HasPrinted = Product.GetParameter("EOL_LBL_HasPrinted");
                int printCount = 0;

                ProductOperationDetail lastPrintResult = API.GetProductOperationDetail(this.Product.TraceProduct.ProductID, 3, 88, 0);
                bool needNewPrint = true;
                if (lastPrintResult != null && lastPrintResult.OK)
                {
                    printCount = lastPrintResult.OpDetail[0].Value;
                    needNewPrint = printCount > 0 ? false : true;
                }

                if (!needNewPrint)
                {
                    needNewPrint = MessageBox.Show("A termékhez már nyomtatásra került címke, akar újat nyomtatni?", "Megerősítés!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes ? true : false;
                }

                if (!needNewPrint)
                {
                    return;
                }
                string oldText1 = "^FD*******^FS";
                string year = Product.TraceProduct.SerialNumber.Substring(0, 2);
                string month = Product.TraceProduct.SerialNumber.Substring(2, 2);
                string newText1 = $"^FD{"20" + year + "-" + month}^FS";
                string oldText2 = "**********^FS";
                string newText2 = $"{Product.TraceProduct.SerialNumber}^FS";
                string textToPrint;
                textToPrint = ReadPrnFromFile(okLabelPrn);
                if (string.IsNullOrEmpty(textToPrint))
                {
                    CZP.Message = "Invalid .prn file";
                    return;
                }
                textToPrint = textToPrint.Replace(oldText1, newText1);
                textToPrint = textToPrint.Replace(oldText2, newText2);

                CZP.PrintUSBTask(textToPrint);
                ManageTestDetail(null, 1, param_EOL_LBL_HasPrinted, ++printCount, "printed", true);
                //AddOperationDetail(param_EOL_LBL_HasPrinted.ItemID, 1, 1, ++printCount, "printed");
            }
        }

        public string ReadPrnFromFile(string fileName)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\" + fileName);
            FileInfo fi = new FileInfo(path);
            string prnFile = null;
            if (fi.Exists && fi.Extension == ".prn")
            {
                prnFile = File.ReadAllText(path);
            }
            return prnFile;
        }

    }
}
