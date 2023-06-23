using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using VTEP.TI.BatteryManagement.BQ76942_769142_76952;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class RegisterControlContainer : UserControl
    {
        DirectRam DirectRam;

        public RegisterControlContainer()
        {
            InitializeComponent();
            SetupControl();
        }

        public void SetDirectRam(DirectRam directRam)
        {
            this.DirectRam = directRam;
            directRam.PropertyChanged += DirectRam_PropertyChanged;
            RefreshAllStatusRegisterData();
        }

        private void DirectRam_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RefreshAllStatusRegisterData();
        }

        private void RefreshAllStatusRegisterData()
        {
            InvokeGuiThread(() =>
            {
                SetStatusRegisterData(FETStatusControl, DirectRam.FET_Status);
                SetStatusRegisterData(SafetyStatusAControl, DirectRam.Safety_Status_A);
                SetStatusRegisterData(SafetyStatusCControl, DirectRam.Safety_Status_C);
                SetStatusRegisterData(BatteryStatusHighControl, DirectRam.Battery_Status_high);
                SetStatusRegisterData(BatteryStatusLowControl, DirectRam.Battery_Status_low);
            });
        }

        private void SetStatusRegisterData(RegisterControl control, BitRegister bitRegister)
        {
            control.Bit0Status = bitRegister.GetBit(0);
            control.Bit1Status = bitRegister.GetBit(1);
            control.Bit2Status = bitRegister.GetBit(2);
            control.Bit3Status = bitRegister.GetBit(3);
            control.Bit4Status = bitRegister.GetBit(4);
            control.Bit5Status = bitRegister.GetBit(5);
            control.Bit6Status = bitRegister.GetBit(6);
            control.Bit7Status = bitRegister.GetBit(7);
        }

        private void ResetAll()
        {
            foreach (var reg in this.Controls.AsParallel())
            {
                if (reg.GetType() == typeof(RegisterControl))
                {
                    ((RegisterControl)reg).Bit0Status = false;
                    ((RegisterControl)reg).Bit1Status = false;
                    ((RegisterControl)reg).Bit2Status = false;
                    ((RegisterControl)reg).Bit3Status = false;
                    ((RegisterControl)reg).Bit4Status = false;
                    ((RegisterControl)reg).Bit5Status = false;
                    ((RegisterControl)reg).Bit6Status = false;
                    ((RegisterControl)reg).Bit7Status = false;
                }
            }
        }

        private void SetupControl()
        {
            // FET Status
            this.FETStatusControl.Title = "FET Status";
            this.FETStatusControl.Bit0Text = "CHG_FET";
            this.FETStatusControl.Bit1Text = "PCHG_FET";
            this.FETStatusControl.Bit2Text = "DSG_FET";
            this.FETStatusControl.Bit3Text = "PDSG_FET";
            this.FETStatusControl.Bit4Text = "DCHG_PIN";
            this.FETStatusControl.Bit5Text = "DDSG_PIN";
            this.FETStatusControl.Bit6Text = "ALRT_PIN";
            this.FETStatusControl.Bit7Text = "RSVD7_0";
            // Safety Status A
            this.SafetyStatusAControl.Title = "Safety Status A";
            this.SafetyStatusAControl.Bit0Text = "RSVD0_0";
            this.SafetyStatusAControl.Bit1Text = "RSVD1_0";
            this.SafetyStatusAControl.Bit2Text = "CUV";
            this.SafetyStatusAControl.Bit3Text = "COV";
            this.SafetyStatusAControl.Bit4Text = "OCC";
            this.SafetyStatusAControl.Bit5Text = "OCD1";
            this.SafetyStatusAControl.Bit6Text = "OCD2";
            this.SafetyStatusAControl.Bit7Text = "SCD";
            // Safety Status C
            this.SafetyStatusCControl.Title = "Safety Status C";
            this.SafetyStatusCControl.Bit0Text = "RSVD0_0";
            this.SafetyStatusCControl.Bit1Text = "HWDF";
            this.SafetyStatusCControl.Bit2Text = "PTO";
            this.SafetyStatusCControl.Bit3Text = "RSVD3_0";
            this.SafetyStatusCControl.Bit4Text = "COVL";
            this.SafetyStatusCControl.Bit5Text = "OCDL";
            this.SafetyStatusCControl.Bit6Text = "SCDL";
            this.SafetyStatusCControl.Bit7Text = "OCD3";
            // Battery Status High
            this.BatteryStatusHighControl.Title = "Battery Status H";
            this.BatteryStatusHighControl.Bit0Text = "SEC0";
            this.BatteryStatusHighControl.Bit1Text = "SEC1";
            this.BatteryStatusHighControl.Bit2Text = "FUSE";
            this.BatteryStatusHighControl.Bit3Text = "SS";
            this.BatteryStatusHighControl.Bit4Text = "PF";
            this.BatteryStatusHighControl.Bit5Text = "SD_CMD";
            this.BatteryStatusHighControl.Bit6Text = "RSVD14_0";
            this.BatteryStatusHighControl.Bit7Text = "SLEEP";
            // Battery Status Low
            this.BatteryStatusLowControl.Title = "Battery Status L";
            this.BatteryStatusLowControl.Bit0Text = "CFGUPDATE";
            this.BatteryStatusLowControl.Bit1Text = "PCHG_MODE";
            this.BatteryStatusLowControl.Bit2Text = "SLEEP_EN";
            this.BatteryStatusLowControl.Bit3Text = "POR";
            this.BatteryStatusLowControl.Bit4Text = "WD";
            this.BatteryStatusLowControl.Bit5Text = "COW_CHK";
            this.BatteryStatusLowControl.Bit6Text = "OTPW";
            this.BatteryStatusLowControl.Bit7Text = "OTPB";
        }

        private void InvokeGuiThread(Action action)
        {
            this.BeginInvoke(action);
        }

    }
}
