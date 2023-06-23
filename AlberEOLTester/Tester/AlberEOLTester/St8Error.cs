using Alber.Eol.Hardware;
using AlberEOL.Base;
using System.Collections.Generic;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        private void ErrorTask()
        {
            #region statecontrol
            // Állapotot váltunk
            State = StationState.Error;

            DetectingTesterExceptions = new List<TesterError>
            {
                TesterError.ERR_APPCLOSE
            };

            string[] NoDBErrors = { "TRACE1", "TRACE2", "TRACE3", "EMERGENCY" };
            #endregion

            Operation = "Hibakezelés";
            mreDone.Reset();
            TaskMessage = null;
            CommInterface.RelayStatus = RelayStatus.AllOFF;
            foreach (var item in StoredTesterExceptions)
            {
                switch (item)
                {
                    case TesterError.ERR_EMERGENCY:
                        ErrorCode = new ErrorCode("EMERGENCY", "VÉSZMEGÁLLÁS! TÁPELLÁTÁS MEGSZAKADT!");
                        break;
                }
            }
            IsDoneButtonEnabled = true;
            mreDone.WaitOne();
            IsDoneButtonEnabled = false;
            StoredTesterExceptions.Clear();

            ResetStationVariables();
            LastTestDetail = null;
        }
    }
}
