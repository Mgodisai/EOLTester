using AlberEOL.Base;
using AlberEOL.Properties;
using AlberEOL.Station;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AlberEOL.UI.GraphicalComponents
{
    public partial class StationControl : UserControl
    {

        public StationControl()
        {
            InitializeComponent();
        }

        private StationBase Station;

        public void SetStation(StationBase station)
        {
            Station = station;
            Station.PropertyChanged += Station_PropertyChanged;
            this.groupBox1.Text = Station.StationName.ToString();
        }

        public void StopStation()
        {
            Station.PropertyChanged -= Station_PropertyChanged;
            Station = null;
        }

        private void Station_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Action action = () =>
            {
                switch (e.PropertyName)
                {
                    case nameof(StationBase.State):
                        if (Station.State == StationState.WaitForStart)
                        {
                            DoneOrStartButton.Text = "START";
                            BackButton.Enabled = true;
                        }
                        else
                        {
                            DoneOrStartButton.Text = "NYUGTA";
                            BackButton.Enabled = false;
                        };
                        break;
                    case nameof(StationBase.Message):
                        if (Station.Message == null)
                        {
                            txt_TaskBox.Text = "";
                            txt_Message.BackColor = Settings.Default.NormalColor;
                            //txt_Message.ForeColor = Station.Message.ForeColor;
                        }
                        else
                        {
                            txt_Message.Text = Station.Message.Text;
                            txt_Message.BackColor = Station.Message.BackGroundColor;
                            txt_Message.ForeColor = Station.Message.ForeColor;
                        }

                        break;

                    case nameof(StationBase.TaskMessage):
                        if (Station.TaskMessage == null)
                        {
                            txt_TaskBox.Text = "";
                        }
                        else
                        {
                            if (txt_TaskBox.Text != "") txt_TaskBox.Text = txt_TaskBox.Text + Environment.NewLine + Station.TaskMessage.Text;
                            if (txt_TaskBox.Text == "") txt_TaskBox.Text = Station.TaskMessage.Text;
                        }
                        break;

                    case nameof(StationBase.Product):
                        if (Station.Product == null || Station.Product.TraceProduct==null)
                        {
                            ProductIDLabel.Text = "ProductIDLabel";
                            SerialNumberLabel.Text = "SerialNumber";
                            ProductTypeLabel.Text = "ModelName";
                        }
                        else
                        {
                            ProductIDLabel.Text = Station.Product.TraceProduct.ProductID;
                            SerialNumberLabel.Text = Station.Product.TraceProduct.SerialNumber;
                            ProductTypeLabel.Text = Station.Product.ProductType.ModelName;
                        }

                        break;

                    case nameof(AlberEOLStation.IsDoneButtonEnabled):
                        DoneOrStartButton.Enabled = ((AlberEOLStation)sender).IsDoneButtonEnabled;
                        if (DoneOrStartButton.Enabled)
                        {
                            DoneOrStartButton.BackColor = Color.MediumSlateBlue;
                            DoneOrStartButton.ForeColor = Color.Black;
                        }
                        else
                        {
                            DoneOrStartButton.BackColor = Color.DarkSlateGray;
                            DoneOrStartButton.ForeColor = Color.LightGray;
                        }
                        break;

                    case nameof(AlberEOLStation.IsBackButtonEnabled):
                        BackButton.Enabled = ((AlberEOLStation)sender).IsBackButtonEnabled;
                        if (BackButton.Enabled)
                        {
                            BackButton.BackColor = Color.MediumSlateBlue;
                            BackButton.ForeColor = Color.Black;
                        }
                        else
                        {
                            BackButton.BackColor = Color.DarkSlateGray;
                            BackButton.ForeColor = Color.LightGray;
                        }
                        break;
                    case nameof(StationBase.ErrorCode):
                        if (Station.ErrorCode == null)
                        {
                            return;
                        }
                        if (Station.ErrorCode.Code != "")
                        {

                            if (txt_ErrorLog.Text != "") txt_ErrorLog.Text = Station.ErrorCode.ToString() + Environment.NewLine + txt_ErrorLog.Text;
                            if (txt_ErrorLog.Text == "") txt_ErrorLog.Text = Station.ErrorCode.ToString();

                            if (Station.ErrorCode.Code != "")
                            {
                                txt_Message.Text = "HIBA: " + Station.ErrorCode.Message + Environment.NewLine + " NYUGTÁZZA!";
                                txt_Message.BackColor = Station.ErrorCode.Message.BackGroundColor;
                            }
                        }
                        break;

                    case nameof(StationBase.Operation):
                        if (string.IsNullOrEmpty(Station.Operation))
                        {
                            txt_operation.Clear();
                        }
                        else
                        {
                            txt_operation.Text = Station.Operation + Environment.NewLine + txt_operation.Text;
                        }

                        break;
                }
            };
            this.Invoke(action);
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            ((AlberEOLStation)Station).mreDone.Set();
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            ((AlberEOLStation)Station).mreBack.Set();
        }
    }
}
