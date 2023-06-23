using Alber.Eol.Hardware;
using AlberEOL.Base;
using AlberEOL.Properties;
using System;
using System.Collections.Generic;
using TraceabilityHandler;

namespace AlberEOL.Station
{
    [Flags]
    public enum TestStep : short
    {
        None = 0,
        VerifyManuData = 1,
        VerifyDataMemory = 2,
        EnableFuseDrive = 4,
        VerifyDirectRam = 8,
        TestOCD = 16,
        TestSCD = 32,
        TestDSG2Ohm = 64,
        TestCHG3A = 128,
        TestOCC = 256,
        UpdateManuData = 512,
        Shutdown = 1024,
        ImpedanceTest_1kHz = 2048,
        PrintLabel = 4096,
        PreliminaryTest = 8192
    }
    public partial class AlberEOLStation : StationBase
    {
        static bool SendPropertyChangedEventHandler = false;

        private void Test()
        {
            // TESZTFOLYAMAT
            #region statecontrol
            // Állapotot váltunk
            State = StationState.Working;

            DetectingTesterExceptions = new List<TesterError>
            {
                TesterError.ERR_MANUAL,
                TesterError.ERR_APPCLOSE,
                TesterError.ERR_EMERGENCY
            };
            CommInterface.RelayStatus = RelayStatus.AllOFF;
            TesterSuperVise();
            #endregion

            TaskMessage = null;
            TaskMessage = new GeneralMessage("A teszt elindult, kérem várjon a befejezéséig!");

            StartTime = DateTime.Now;

            listOfErrorDetails = new List<TestDetail>();

            // Tesztkiválasztás (Settings menü alapján)
            TestStep testNumber = (TestStep)Settings.Default.TestNumber;

            // Tesztek
            bool cont = CheckLoadBetweenTests();

            if (cont) cont = PreliminaryTest(testNumber);

            if (cont) cont = VerifyMANU_DATA(testNumber);

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.VerifyManuData, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = VerifyDataMemory(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.VerifyDataMemory, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = EnableFuseDrive(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.EnableFuseDrive, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = VerifyDirectRam(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.VerifyDirectRam, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = ImpedanceTest(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.ImpedanceTest_1kHz, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = TestOCD(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.TestOCD, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = TestSCD(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.TestSCD, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = TestDSG2Ohm(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.TestDSG2Ohm, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = TestCHG3A(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.TestCHG3A, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = TestOCC(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.TestOCC, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = UpdateMANU_DATA(testNumber);
            cont &= CheckLoadBetweenTests();

            if (!CheckLoadBetweenTests())
            {
                cont = false;
                AddErrorInfo(TestStep.UpdateManuData, "ILLEGAL_LOAD", "illegal load state between test steps");
                return;
            }

            if (cont) cont = Shutdown(testNumber);

            PrintLabel(testNumber, cont, DateTime.Now);

            Parameter param_EOL_StartTime = Product.GetParameter("EOL_StartTime");
            Parameter param_EOL_EndTime = Product.GetParameter("EOL_EndTime");
            EndTime = DateTime.Now;
            ManageTestDetail(null, 1, param_EOL_StartTime, 0, StartTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
            ManageTestDetail(null, 1, param_EOL_EndTime, 0, EndTime.ToString("yyyy-MM-dd HH:mm:ss"), true);
            ManageTestDetail("CycleTime", "", "", "", "s", (EndTime - StartTime).TotalSeconds.ToString(), true);
        }

        private bool CheckLoadBetweenTests()
        {
            // Terhelés ellenőrzés
            DirectRam.ReadAll();
            if (Math.Abs(DirectRam.CC2_Current) > 1)
            {
                Message = new AlertMessage("A pakk terhelés alatt van, teszt megszakításra kerül");
                return false;
            }
            return true;
        }
    }
}
