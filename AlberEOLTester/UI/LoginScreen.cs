using AlberEOL.Properties;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using TraceabilityHandler;
using Logger = AlberEOL.CustomClasses.Logger;

namespace AlberEOL.UI
{
    public partial class LoginScreen : Form
    {
        // Backdrop
        private const int CS_DropShadow = 0x00020000;

        // UserState
        public UserState TempUserState { get; set; }
        public LoginScreen()
        {
            InitializeComponent();
            this.CancelButton = btn_Exit;
            API.BaseUrl = Settings.Default.TraceBaseURL;
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            SoftwareVersion.Text = "ver." + fvi.FileVersion;
            TempUserState = null;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            LoginMSG.Text = null;
            string userName = txt_UN1.Texts;
            string passWord = txt_PW1.Texts;

            // Ellenőrizzük, hogy minden mezőt kitöltöttek-e
            if (userName != "" && passWord != "")
            {
                try
                {
                    this.TempUserState = API.Login(new UserLogin { UserName = userName, Password = passWord });

                    Logger.WriteAuthLog(userName, TempUserState.Success, TempUserState.Message);

                    if (TempUserState.Success == true)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        LoginMSG.Text = this.TempUserState.Message.ToString();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    LoginMSG.Text = ex.Message;
                    return;
                }
            }
            else
            {
                LoginMSG.Text = "Az összes mező kitöltése kötelező!";
                txt_PW1.Texts = "";
                return;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = CS_DropShadow;
                return cp;
            }
        }

        private void txt_PW1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btn_Login_Click(sender, e);
        }

        private void txt_PW1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btn_Login_Click(sender, e);
            }
        }
    }
}
