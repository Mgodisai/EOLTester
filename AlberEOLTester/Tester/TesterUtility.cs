using System.Collections.Specialized;

namespace AlberEOL.Base
{
    /// <summary>
    /// Name of the Stations
    /// </summary>
    public enum StationName
    {
        AlberEOLStation = 0
    }

    /// <summary>
    /// Teszter szintű hibák
    /// </summary>
    public enum TesterError
    {
        ERR_MAIN = 0,
        ERR_APPCLOSE,
        ERR_EMERGENCY,
        ERR_MANUAL
    }

    /// <summary>
    /// Működési módok: folyamatos, manuális
    /// </summary>
    public enum OperationMode
    {
        StandAlone = 0,
        Manual
    }

    public static class Err
    {
        public static int ERR_MAIN = BitVector32.CreateMask();
        public static int ERR_APPCLOSE = BitVector32.CreateMask(ERR_MAIN);
        public static int ERR_EMERGENCY = BitVector32.CreateMask(ERR_APPCLOSE);
        public static int ERR_MANUAL = BitVector32.CreateMask(ERR_EMERGENCY);
    }

}
