using AlberEOL.Base;
using System.Collections.Generic;
using System.Threading;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        private void ManualTask()
        {
            #region statecontrol
            // Állapotot váltunk
            State = StationState.Manual;
            DetectingTesterExceptions = new List<TesterError>
            {
                TesterError.ERR_APPCLOSE
            };

            #endregion

            mreDone.Reset();
            Operation = "Manual Test Mode";
            TaskMessage = null;
            Message = null;
            Thread.Sleep(2500);
            //DataMemory.Settings__Protection__Protection_Configuration.Bit4 = true;


            IsDoneButtonEnabled = true;
            mreDone.WaitOne();
            IsDoneButtonEnabled = false;
            VerifyDataMemory(TestStep.VerifyDataMemory);
            mreDone.Reset();
            IsDoneButtonEnabled = true;
            mreDone.WaitOne();
            IsDoneButtonEnabled = false;

            ResetStationVariables();
        }

        private void ResetStationVariables()
        {
            TaskMessage = null;
            Message = null;
            ErrorCode = null;
            Product = null;
            Operation = null;
            LastTestDetail = null;
            PrevDataMemory = new byte[DataMemory.RamRead.Length];
            MemDiffs = new bool[DataMemory.RamRead.Length];
        }
    }
}
