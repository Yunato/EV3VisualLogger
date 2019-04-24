using System;
using System.Drawing;
using System.Windows.Forms;

namespace EV3VisualLogger.UI.MyForm {
    public partial class LogForm : Form{

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e) {
            if(e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void logTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            e.Handled = true;
        }

        private void closeButton_Click(object sender, EventArgs e) {
            Hide();
        }
        
        private void logTextBox_MouseWheel(object sender, MouseEventArgs e) {
            if (Math.Abs(e.Delta) < 120) {
                return;
            }

            ScrollableControl control = (ScrollableControl)this;
            var scroll = control.VerticalScroll;

            var maximum = 1 + scroll.Maximum - scroll.LargeChange;
            var delta = -(e.Delta / 120) * scroll.SmallChange;
            var offset = Math.Min(Math.Max(scroll.Value + delta, scroll.Minimum), maximum);

            scroll.Value = offset;
            scroll.Value = offset;
        }
    }
}
