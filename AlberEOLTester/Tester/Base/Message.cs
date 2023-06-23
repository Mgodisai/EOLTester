using AlberEOL.Properties;
using System;
using System.Drawing;

namespace AlberEOL.Base
{
    public class GeneralMessage
    {
        public string Text;
        internal Color ForeColor;
        internal Color BackGroundColor;

        public GeneralMessage(string message)
        {
            this.Text = message;
            this.ForeColor = Color.Black;
            this.BackGroundColor = Settings.Default.NormalColor;
        }

        public void Clear()
        {
            this.Text = string.Empty;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class SuccessMessage : GeneralMessage
    {
        public SuccessMessage(string text) : base(text)
        {
            this.ForeColor = Color.Black;
            this.BackGroundColor = Settings.Default.SuccessColor;
        }
    }

    public class AlertMessage : GeneralMessage
    {
        public AlertMessage(string text) : base(text)
        {
            this.ForeColor = Color.Black;
            this.BackGroundColor = Settings.Default.ErrorColor;
        }
    }

    public class WarningMessage : GeneralMessage
    {
        public WarningMessage(string text) : base(text)
        {
            this.ForeColor = Color.Black;
            this.BackGroundColor = Settings.Default.WarningColor;
        }
    }

    public class ErrorCode
    {
        public string Code;
        public GeneralMessage Message;
        public DateTime Date;

        public ErrorCode(string code, string message)
        {
            this.Code = code;
            this.Message = new AlertMessage(message);
            this.Date = DateTime.Now;
        }

        public ErrorCode(string code, GeneralMessage message)
        {
            this.Code = code;
            this.Message = message;
            this.Date = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            var item = obj as ErrorCode;

            if (item == null)
            {
                return false;
            }

            return this.Code.Equals(item.Code);
        }

        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Code}: {Date.ToString("yyyy. MM. dd. HH:mm:ss")} - {Message.Text}";
        }
    }
}
