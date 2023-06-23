using AlberEOL.Base;
using System.Collections.Generic;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        private void TestFinishedTask()
        {
            #region statecontrol
            // Állapotot váltunk
            State = StationState.Finished;

            DetectingTesterExceptions = new List<TesterError>
            {
                TesterError.ERR_APPCLOSE,
                TesterError.ERR_EMERGENCY
            };

            TesterSuperVise();
            #endregion

            Operation = "Teszt befejezése";

            IsDoneButtonEnabled = false;
            TaskMessage = null;
            TaskMessage = new GeneralMessage("A teszt befejeződött, kérem csatlakoztassa le a terméket!");

            // Done/Start to continue
            mreDone.Reset();
            IsDoneButtonEnabled = true;
            mreDone.WaitOne();
            IsDoneButtonEnabled = false;

            ResetStationVariables();
        }
    }
}
