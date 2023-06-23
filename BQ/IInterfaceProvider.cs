namespace VTEP.TI.BatteryManagement.BQ76942_769142_76952
{
    public interface IInterfaceProvider
    {
        CommType CommType { get; }

        void WriteI2C(byte address, byte[] txd, uint speed);

        byte[] ReadI2C(byte address, int length, uint speed);

        byte[] TransferSpi(byte[] txd, uint speed, byte mode);
    }
}
