using AlberEOL.Base;
using AlberEOL.Exceptions;
using AlberEOL.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TraceabilityHandler;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;
using static VTEP.TI.BatteryManagement.BQ76942_769142_76952.DirectRam;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        public decimal CalculateDecimalDeviation(bool useAbsValue, params decimal[] list)
        {
            if (list.Length == 0) return 0;

            decimal min = decimal.MaxValue;
            decimal max = decimal.MinValue;
            decimal value;
            foreach (decimal number in list)
            {
                value = useAbsValue ? Math.Abs(number) : number;

                min = value < min ? value : min;
                max = value > max ? value : max;
            }
            return max - min;
        }

        public byte[] ReadDataMemoryFromFile(string fileName)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\" + fileName);
            FileInfo fi = new FileInfo(path);
            byte[] fileDataMemory = null;
            if (fi.Exists && fi.Extension == ".bin")
            {
                fileDataMemory = File.ReadAllBytes(path);
            }
            return fileDataMemory;
        }

        public void ReadDirectRamCustomBlocks()
        {
            List<Block> blocks = new List<Block>();
            blocks.Add(new Block(DirectRamRegister.Safety_Status_A, 1));
            blocks.Add(new Block(DirectRamRegister.Safety_Status_C, 1));
            blocks.Add(new Block(DirectRamRegister.Battery_Status_low, 2));
            blocks.Add(new Block(DirectRamRegister.FET_Status, 1));
            blocks.Add(new Block(DirectRamRegister.CC2_Current_low, 2));
            DirectRam.Read(blocks);
        }

        private Property GetPropertyFromTrace(string propertyName)
        {
            Property tempProperty = Product.GetProperty(propertyName);
            if (tempProperty == null)
            {
                ErrorCode = new ErrorCode("TRACE4", $"Hiányzó terméktulajdonság: {propertyName}");
                throw new TesterException();
            }
            return tempProperty;
        }

        private int PropertyStringToInt(Property property)
        {
            int tempIntegerValue;
            try
            {
                tempIntegerValue = StringToInt(property.Value);
            }
            catch (TesterException)
            {
                ErrorCode = new ErrorCode("TRACE4", $"A terméktulajdonság értéke nem konvertálható (string to int) {property.PropertyName}");
                throw new TesterException();
            }
            return tempIntegerValue;
        }

        private int StringToInt(string text)
        {
            int tempIntegerValue;
            if (int.TryParse(text, out tempIntegerValue))
            {
                return tempIntegerValue;
            }
            else
            {
                ErrorCode = new ErrorCode("TRACE4", $"A szöveg nem konvertálható (string to int): {text}");
                throw new TesterException();
            }
        }

        private double PropertyStringToDouble(Property property)
        {
            double tempDoubleValue;
            if (double.TryParse(property.Value, out tempDoubleValue))
            {
                return tempDoubleValue;
            }
            else
            {
                ErrorCode = new ErrorCode("TRACE4", $"A terméktulajdonság értéke nem konvertálható(string to double): {property.PropertyName}");
                throw new TesterException();
            }
        }

        private void ManageTestDetail(string parameterName, int itemIndex, Parameter parameter, double? value, string textValue, bool passed)
        {
            // GUI PropertyGrid
            TestDetail tempDetail = new TraceTestDetail()
            {
                TraceParameter = parameter,
                ResultValue = textValue,
                Passed = passed
            };
            tempDetail.ResultValue = (textValue == null ? value.ToString() : textValue);
            listOfErrorDetails.Add(tempDetail);
            LastTestDetail = tempDetail;

            //Trace Operation Detail
            OperationDetail tempOpDetail = new OperationDetail()
            {
                ParameterItemID = parameter.ItemID,
                ItemIndex = itemIndex,
                TextValue = textValue,
                Passed = passed ? 1 : 0
            };
            if (value != null) tempOpDetail.Value = (double)value;

            Product.AddOperationDetail(tempOpDetail);
        }

        private void ManageTestDetail(string parameterName, string min, string max, string nominal, string uom, string textValue, bool passed)
        {
            // GUI PropertyGrid
            TestDetail tempDetail = new TestDetail()
            {
                ParamName = parameterName,
                Min = min,
                Max = max,
                Nominal = nominal,
                UnitOfMeasure = uom,
                ResultValue = textValue,
                Passed = passed
            };
            LastTestDetail = tempDetail;
        }

        private void AddErrorInfo(TestStep testStep, string errorCode, string errorMessage)
        {
            Parameter errorParameter = Product.GetParameter(Settings.Default.ErrorParameterName);
            ManageTestDetail(null, Product.ErrorCodeItemIndex++, errorParameter, 0, $"{errorCode};{testStep};{errorMessage}", false);
        }

        private void StoreRegisterBitErrors(TestStep testStep, BitRegister register, string[] codes, string errorCode)
        {
            string errorMessage = "";
            for (byte i = 0; i < 8; i++)
            {
                errorMessage += register.GetBit(i) ? codes[i] : "";
            }
            AddErrorInfo(testStep, errorCode, errorMessage);
        }
    }
}
