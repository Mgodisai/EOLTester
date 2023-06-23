
using AlberEOL.Base;
using AlberEOL.Properties;
using AlberEOL.Station;
using AlberEOL.UI.GraphicalComponents;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Logger = AlberEOL.CustomClasses.Logger;

namespace AlberEOL.UI
{
    public partial class MainScreen : Form

    {
        private Tester Tester;

        public MainScreen()
        {
            this.Tester = new Tester();

            InitializeComponent();

            if (!Login())
            {
                this.Close();
                return;
            }
            UserIDLabel.Text = Tester.User.UserName + (Tester.AdminUser == null ? "" : "(admin)");

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            SoftwareVersion.Text = "ver." + fvi.FileVersion;

            LauncherOptions.SoftwareType = "ALBER";

            switch (LauncherOptions.SoftwareType)
            {
                case "ALBER":
                    break;
            }
        }

        private bool Login()
        {
            using (var loginScreen = new LoginScreen())
            {
                var result = loginScreen.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Tester.User = loginScreen.TempUserState;

                    if (Settings.Default.AdminUserIdList.Contains(Tester.User.UserID))
                    {
                        Tester.AdminUser = loginScreen.TempUserState;
                    }
                    return true;
                }

                if (result == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return false;
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            // Teszter indítás
            Tester.PropertyChanged += Tester_PropertyChanged;
            Tester.StartTester(LauncherOptions.SoftwareType);
            Tester.StartStations();

            AlberEOLStation station = (AlberEOLStation)Tester.AlberEOLStation;
            // Control-Station összerendelés
            SettingsControl.SetStation(Tester);
            StationControl.SetStation(Tester.AlberEOLStation);

            PowerSupplyDisplay.SetPowerSupply(station.CPX);
            RelayControlContainer.SetCommInterface(station.CommInterface);
            RegisterControl.SetDirectRam(station.DirectRam);
            DataControlContainer.SetCompontents(station.BMS, station);
            bimControl1.SetDevice(station.BIM);
            printerControl1.SetDevice(station.CZP);
            TesterStatusStripLabel.Text = "Teszter elindtva";
        }

        private void Tester_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Action action = () =>
            {
                switch (e.PropertyName)
                {
                    case nameof(Tester.Message):
                        TesterStatusStripLabel.Text = Tester.Message;
                        break;
                }
            };
            this.Invoke(action);
        }

        #region Backdrop
        private const int CS_DropShadow = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = CS_DropShadow;
                return cp;
            }
        }
        #endregion

        public static class LauncherOptions
        {
            public static string SoftwareType { get; set; }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            RelayControlContainer.Close();
            DataControlContainer.CloseControl();
            PowerSupplyDisplay.CloseControl();
            StationControl.StopStation();
            SettingsControl.Close();
            Tester.StopTester();
            bimControl1.Close();
            printerControl1.Close();
            Logger.WriteGeneralLog("A program leállítva", "MainScreen");
            Thread.Sleep(200);
            this.Close();

        }

        private void MainTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            DialogResult answer;
            if (e.TabPage.Name == "ServiceTab")
            {
                answer = MessageBox.Show("Biztos, hogy a Service Tab-ra akar váltani?", "Megerősítés", MessageBoxButtons.YesNo);
                if (answer == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
                if (Tester.AdminUser != null)
                {
                    var Station = Tester.Stations.FirstOrDefault(s => s.State.ToString() == StationState.Working.ToString());

                    // Ellenőrizni, megálltak-e az állomások

                    if (Station != null)
                    {
                        if (MessageBox.Show("Van olyan állomás ami még dolgozik. Folytatja? ", "Figyelem!", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }

                    Tester.OperationMode = OperationMode.Manual;
                    Tester.Errors[Err.ERR_MANUAL] = true;
                    //txt_Messages.Text = "Manuális üzemmód!";

                    Tester.OperationMode = OperationMode.StandAlone;
                    Tester.Errors[Err.ERR_MANUAL] = false;
                    //txt_Messages.Text = "Automata üzemmód!" + Environment.NewLine;
                }
                else
                {
                    MessageBox.Show("A beállításokat csak admin módban módosíthatja!");
                    MainTabControl.SelectedIndex--;
                }
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
