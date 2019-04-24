using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EV3VisualLogger.UI.Dialog {
    public partial class MessageDialog : Form {

        private string _message;

        public MessageDialog(string title, Icon icon, string message) {
            InitializeComponent();

            SetUpUI();
            _message = message;
            
            Bitmap bmp = new Bitmap(288, 32);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawIcon(SystemIcons.Question, 0, 0);
            g.Dispose();
            messageIcon.Image = bmp;

            this.Text = title;

            if (icon == null) {
                YesButton.Visible = false;
                NoButton.Visible = false;
                messageIcon.Visible = false;
                messageLabel.Location = messageIcon.Location;
            }

            if (icon == SystemIcons.Exclamation) {
                YesButton.Visible = false;
                NoButton.Text = "OK";
            }
        }

        private void SetUpUI() {
            #region フォーム
            this.BackColor = ColorTranslator.FromHtml("#FF252526");
            #endregion

            #region ボタン
            List<Button> buttons = new List<Button>();
            buttons.Add(this.YesButton);
            buttons.Add(this.NoButton);

            foreach (Button button in buttons) {
                button.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
                button.BackColor = ColorTranslator.FromHtml("#3F3F46");
                button.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#555555");
                button.FlatStyle = FlatStyle.Flat;
            }
            #endregion
        }

        private void MessageDialog_Load(object sender, EventArgs e) {
            this.SuspendLayout();

            this.messageIcon.TabIndex = 0;
            this.messageIcon.TabStop = false;

            this.messageLabel.TabIndex = 0;
            this.messageLabel.TabStop = false;
            this.messageLabel.Text = _message;
            this.messageLabel.ForeColor = ColorTranslator.FromHtml("#FFF1F1F1");

            this.Size = new Size(
                messageLabel.Location.X + messageLabel.PreferredWidth + 51,
                messageLabel.Location.Y + messageLabel.PreferredHeight + 114);

            this.NoButton.Location = new Point(this.Width - 111, this.Height - 75);
            this.YesButton.Location = new Point(this.NoButton.Location.X - 96, this.NoButton.Location.Y);
            this.ResumeLayout(false);
        }

        private void YesButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Yes;
            Close();
        }

        private void NoButton_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.No;
            Close();
        }
    }
}
