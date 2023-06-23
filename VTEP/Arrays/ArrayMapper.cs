using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTEP.Arrays
{
    public static class ArrayMapper
    {
        #region 1 bit
        public static bool GetBit(byte[] array, int index, byte bit)
        {
            int offset = bit >> 3;
            bit -= (byte)(offset << 3);
            int mask = 1 << bit;
            return (array[index + offset] & mask) != 0;
        }

        public static void SetBit(byte[] array, int index, byte bit, bool value)
        {
            int offset = bit >> 3;
            bit -= (byte)(offset << 3);
            byte mask = (byte)(1 << bit);
            if (value)
            {
                array[index + offset] |= mask;
            }
            else
            {
                array[index + offset] &= (byte)~mask;
            }
        }

        public static bool GetBit(byte data, byte bit)
        {
            int mask = 1 << bit;
            return (data & mask) != 0;
        }
        #endregion

        #region 8 bit
        public static sbyte GetSbyte(byte[] array, int index)
        {
            return (sbyte)array[index];
        }

        public static void SetSbyte(byte[] array, int index, sbyte value)
        {
            array[index] = (byte)value;
        }

        public static byte GetBits(byte[] array, int index, byte startBit, byte width)
        {
            byte data = array[index];
            int mask = (1 << width) - 1;
            return (byte)((data >> startBit) & mask);
        }

        public static void SetBits(byte[] array, int index, byte startBit, byte width, byte value)
        {
            int mask = ((1 << width) - 1) << startBit;
            int src = value << startBit;
            int dst = array[index];
            array[index] = (byte)(((src ^ dst) & mask) ^ dst);
        }
        #endregion

        #region 16 bit
        public static short GetShort(byte[] array, int index)
        {
            return BitConverter.ToInt16(array, index);
        }

        public static void SetShort(byte[] array, int index, short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Copy(bytes, 0, array, index, bytes.Length);
        }

        public static ushort GetUshort(byte[] array, int index)
        {
            return BitConverter.ToUInt16(array, index);
        }

        public static void SetUshort(byte[] array, int index, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Copy(bytes, 0, array, index, bytes.Length);
        }

        public static decimal GetShortDecimal(byte[] array, int index)
        {
            return GetShort(array, index);
        }
        #endregion

        #region 32 bit
        public static int GetInt(byte[] array, int index)
        {
            return BitConverter.ToInt32(array, index);
        }

        public static uint GetUint(byte[] array, int index)
        {
            return BitConverter.ToUInt32(array, index);
        }

        public static float GetFloat(byte[] array, int index)
        {
            return BitConverter.ToSingle(array, index);
        }

        public static void SetFloat(byte[] array, int index, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Copy(bytes, 0, array, index, bytes.Length);
        }
        #endregion

        public static long GetBCD(byte[] array, int index, byte digits)
        {
            long value = 0;
            int mul = 1;
            byte size = (byte)Math.Ceiling(digits / 2m);
            for (int i = 0; i < size; i++)
            {
                byte b = array[index + i];
                int low = b & 0x0F;
                int high = b >> 4;
                value += mul * low;
                mul *= 10;
                value += mul * high;
                mul *= 10;
            }
            return value;
        }

        public static void SetBCD(byte[] array, int index, byte digits, long value)
        {
            byte size = (byte)Math.Ceiling(digits / 2m);
            for (int i = 0; i < size; i++)
            {
                value = Math.DivRem(value, 10, out long low);
                value = Math.DivRem(value, 10, out long high);
                array[index + i] = (byte)((high << 4) + low);
            }
        }
    }
}
