using System;
using System.ComponentModel;
using System.Globalization;

namespace VTEP.TypeConverters
{
    public class UshortHexTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string)
                ? true
                : base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value.GetType() == typeof(ushort) && destinationType == typeof(string)
                ? string.Format("0x{0:X4}", value)
                : base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string)
                ? true
                : base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            try
            {
                string input = (string)value;
                if (input.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    input = input.Substring(2);
                }
                return ushort.Parse(input, NumberStyles.HexNumber, culture);
            }
            catch (Exception)
            {
                return base.ConvertFrom(context, culture, value);
            }
        }
    }
}
