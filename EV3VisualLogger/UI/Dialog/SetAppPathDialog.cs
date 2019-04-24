using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EV3VisualLogger.UI.Dialog {
    public partial class SetAppPathDialog : Form {

        private string returnPath = string.Empty;

        public SetAppPathDialog() {
            InitializeComponent();
            SetUpUI();
        }

        private void SetUpUI() {
            #region フォーム
            this.BackColor = ColorTranslator.FromHtml("#FF252526");
            #endregion

            #region ボタン
            this.referButton.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
            this.referButton.BackColor = ColorTranslator.FromHtml("#3F3F46");
            this.referButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#555555");
            this.referButton.FlatStyle = FlatStyle.Flat;
            this.applyButton.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
            this.applyButton.BackColor = ColorTranslator.FromHtml("#3F3F46");
            this.applyButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#555555");
            this.applyButton.FlatStyle = FlatStyle.Flat;
            #endregion
        }

        private void referButton_Click(object sender, EventArgs e) {
            if(openFileDialog.ShowDialog(this) == DialogResult.OK) {
                pathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void applyButton_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(pathTextBox.Text) && File.Exists(pathTextBox.Text)
                    && pathTextBox.Text.Substring(pathTextBox.Text.Length - "app".Length).Equals("app")) {
                this.returnPath = pathTextBox.Text;
                this.Close();
            }else {
                if (!ShowQuestionMessageDialog()) {
                    this.returnPath = string.Empty;
                    this.Close();
                }
            }
        }

        private bool ShowQuestionMessageDialog() {
            MessageDialog dialog = new MessageDialog(
                "警告"
                , SystemIcons.Question
                ,"PATHが正しくありません．\n\nこのまま設定を続けますか？");
            if(dialog.ShowDialog(this) == DialogResult.No) {
                return false;
            }
            return true;
        }

        public static string ShowSetAppPathDialog(string appFilePath) {
            SetAppPathDialog dialog = new SetAppPathDialog();
            dialog.pathTextBox.Text = appFilePath;
            dialog.ShowDialog();
            string resultPath = dialog.returnPath;
            dialog.Dispose();
            return resultPath;
        }
    }
}
