using System;
using System.IO;
using MCP2221;
using VTEP.SerialComm;

namespace Alber.Eol.Hardware
{
    public class MCP2221 : II2cAdapter
    {
        // TODO errorcodes
        private readonly MchpUsbI2c Device;

        public bool IsConnected => Device.Settings.GetConnectionStatus();

        public bool GetGpioPin(byte pin)
        {
            int errorNumber = Device.Functions.ReadGpioPinValue(pin);
            if (errorNumber < 0)
            {
                throw new IOException(nameof(MchpUsbI2c), errorNumber);
                // TODO
            }
            return errorNumber != 0;
        }

        public void SetGpioPin(byte pin, bool value)
        {
            int errorNumber = Device.Functions.WriteGpioPinValue(pin, (byte)(value ? 1 : 0));
            if (errorNumber != 0)
            {
                throw new IOException(nameof(MchpUsbI2c), errorNumber);
                // TODO
            }
        }

        public MCP2221()
        {
            Device = new MchpUsbI2c();
        }

        public void Init()
        {
            byte[] ioPinDesignations = new byte[] {
                (byte)GP0_Designation.GPIO,
                (byte)GP1_Designation.GPIO,
                (byte)GP2_Designation.GPIO,
                (byte)GP3_Designation.GPIO
            };
            byte[] ioPinDirections = new byte[] {
                (byte)GPIO_Direction.Output,
                (byte)GPIO_Direction.Output,
                (byte)GPIO_Direction.Output,
                (byte)GPIO_Direction.Output
            };
            byte[] ioPinValues = new byte[] { 0, 0, 0, 0 };

            int res = Device.Settings.SetGpPinConfiguration(0, ioPinDesignations, ioPinDirections, ioPinValues);
        }

        public void Write(byte address, byte[] txd, uint speed)
        {
            int errorNumber = Device.Functions.WriteI2cData(address, txd, (uint)txd.Length, speed);
            if (errorNumber != 0)
            {
                ThrowCommunicationError(nameof(Write), errorNumber);
            }
        }

        public byte[] Read(byte address, int length, uint speed)
        {
            byte[] data = new byte[length];
            int errorNumber = Device.Functions.ReadI2cData(address, data, (uint)length, speed);
            if (errorNumber != 0)
            {
                ThrowCommunicationError(nameof(Read), errorNumber);
            }
            return data;
        }

        private void ThrowCommunicationError(string method, int errorNumber)
        {
            throw new IOException(String.Format("I2C communication error! Method: {0}; ErrorNumber: {1}", method, errorNumber));
            // TODO Custom Exception
        }
    }
}
