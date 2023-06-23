namespace VTEP.SerialComm
{
    public interface II2cAdapter
    {
        void Write(byte address, byte[] txd, uint speed);

        byte[] Read(byte address, int length, uint speed);
    }
}
