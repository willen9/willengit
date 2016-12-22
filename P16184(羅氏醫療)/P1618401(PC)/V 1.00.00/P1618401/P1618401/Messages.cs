using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace P1618401
{
    public class Messages
    {
        public static void Asterisk(string msg)
        {
            MessageBox.Show(msg, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }

        public static void Error(string msg)
        {
            MessageBox.Show(msg, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Question(string msg)
        {
            return MessageBox.Show(msg, "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        public static void Warning(string msg)
        {
            MessageBox.Show(msg, "訊息", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }
    }
}
