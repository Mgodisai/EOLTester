using AlberEOL.Base;
using System.Collections.Generic;
using TraceabilityHandler;

namespace AlberEOL.Station
{
    public partial class AlberEOLStation : StationBase
    {
        private void AfterActionsTask()
        {
            #region statecontrol
            // Állapotot váltunk
            State = StationState.AfterActions;

            DetectingTesterExceptions = new List<TesterError>
            {
                TesterError.ERR_APPCLOSE,
                TesterError.ERR_EMERGENCY
            };

            TesterSuperVise();
            #endregion

            Operation = "Eredmények mentése";

            IsDoneButtonEnabled = false;
            Message = new GeneralMessage("Eredmények mentése az Adatbázisba");

            var judgement = StoreToTraceability();
            if (judgement == 0)
            {
                Message = new SuccessMessage("SIKERES EOL TESZT");
            }
            else
            {
                Message = new AlertMessage($"SIKERTELEN EOL TESZT!");
                TaskMessage = null;
                TaskMessage = new GeneralMessage("Kérem, nyugtázza!");
                // Done/Start to continue
                mreDone.Reset();
                IsDoneButtonEnabled = true;
                mreDone.WaitOne();
                IsDoneButtonEnabled = false;
            }

        }

        private int StoreToTraceability()
        {
            int judgement;
            if (Product.OperationDetails.Details.Count != 0)
            {
                try
                {
                    judgement = Product.SetOperationDetails(Tester.User.UserID);
                    Product.AddOperationResult();
                }
                catch (TraceabilityException ex)
                {
                    ErrorCode = new ErrorCode("TRACE3", $"Traceability adatfeltöltés sikertelen.A szerver válasza: {ex.Message}");
                    judgement = 1;
                }

                if (Product.ProductState.OK == false)
                {
                    ErrorCode = new ErrorCode("TRACE3", $"Traceability adatfeltöltés sikertelen.A termék állapota: {Product.ProductState.Message}");
                    judgement = 1;
                }

            }
            else
            {
                ErrorCode = new ErrorCode("TRACE3", $"Traceability adatfeltöltés sikertelen.A termék állapota: {Product.ProductState.Message}");
                judgement = 1;
            }
            return judgement;
        }
    }
}
