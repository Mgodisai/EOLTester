using System;
using System.Text;
using System.IO;

namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public static class CodeGenerator
    {
        public static void GenerateDataMemory(string filename)
        {

            string[] lines = File.ReadAllText(filename).Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sbEnum = new StringBuilder();
            StringBuilder sbProp = new StringBuilder();
            StringBuilder sbCtor = new StringBuilder();

            ushort offset = 0;
            while (offset < DataMemory.SIZE)
            {
                string line = lines[offset];
                string[] fields = line.Split(new char[] { ';' });
                string _class = fields[0];
                string subclass = fields[1];
                string strAddress = fields[2];
                string regName = fields[3];
                string type = fields[4];
                string strMin = fields[5];
                string strMax = fields[6];
                string strDef = fields[7];
                string strUnits = fields[8];

                if (type == "U1" && strUnits.ToLower()=="hex")
                {
                    type = "H1";
                }

                ushort address;
                try
                {
                    address = ushort.Parse(strAddress);
                    if (address != DataMemory.START + offset)
                    {
                        throw new Exception("DataMemory CSV Address error: " + address);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                byte size;
                string propertyClass;
                switch (type)
                {
                    case "H1":
                        size = 1;
                        propertyClass = "Bit8DataMemory";
                        break;
                    case "H2":
                        size = 2;
                        propertyClass = "Bit16DataMemory"; //
                        break;
                    case "U1":
                        size = 1;
                        propertyClass = "ByteDataMemory";
                        break;
                    case "U2":
                        size = 2;
                        propertyClass = "UshortDataMemory";
                        break;
                    case "I1":
                        size = 1;
                        propertyClass = "SbyteDataMemory";
                        break;
                    case "I2":
                        size = 2;
                        propertyClass = "ShortDataMemory";
                        break;
                    case "F4":
                        size = 4;
                        propertyClass = "FloatDataMemory";
                        break;
                    case "":
                        _class = "Unused";
                        subclass = "None";
                        regName = address.ToString("X4");
                        size = 1;
                        propertyClass = "";
                        break;
                    default:
                        throw new Exception(string.Format("Unknown register type: {0}", type));
                }
                string enumName = string.Format("{0}__{1}__{2}", ToSnakeCase(_class), ToSnakeCase(subclass), ToSnakeCase(regName));
                string propertyName = enumName;
                for (int i = 0; i < size; i++)
                {
                    string enumLine = i == 0
                        ? string.Format("{0} = 0x{1},", enumName, address.ToString("X4"))
                        : string.Format("{0}__{2} = 0x{1},", enumName, (address + i).ToString("X4"), i);
                    sbEnum
                        .Append(enumLine)
                        .Replace("\n", "")
                        .AppendLine();
                }
                if (_class == "Unused")
                {
                    offset += size;
                    continue;
                }

                sbProp.AppendFormat("[Category(\"{0}\"), DisplayName(\"{1}/{2}\"), TypeConverter(typeof(ExpandableObjectConverter))]", _class, subclass, regName)
                    .Replace("\n","")
                    .AppendLine();
                sbProp.AppendFormat("public {0} {1} {{ get; }}", propertyClass, propertyName)
                    .Replace("\n", "")
                    .AppendLine();
                sbProp.AppendLine();

                sbCtor.AppendFormat("{0} = new {1}(this, (ushort)DataMemoryRegister.{2}, {3}, {4}, {5});", propertyName, propertyClass, enumName, strMin, strMax, strDef)
                    .Replace("\n", "")
                    .AppendLine();

                offset += size;
            }
            string path = AppDomain.CurrentDomain.BaseDirectory;
            File.WriteAllText(Path.Combine(path, "DataMemoryRegister.txt"), sbEnum.ToString());
            File.WriteAllText(Path.Combine(path, "DataMemoryProperty.txt"), sbProp.ToString());
            File.WriteAllText(Path.Combine(path, "DataMemoryConstructor.txt"), sbCtor.ToString());
        }

        private static string ToSnakeCase(string name)
        {
            return name
                .Replace(' ', '_')
                .Replace('-', '_')
                .Replace("(", "")
                .Replace(")", "");
        }
    }
}
