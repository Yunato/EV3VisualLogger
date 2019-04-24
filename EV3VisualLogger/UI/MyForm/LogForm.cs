using System.Drawing;
using System.Windows.Forms;

namespace EV3VisualLogger.UI.MyForm{
    public partial class LogForm : Form{

        public LogForm(){
            InitializeComponent();
            SetUpUI();
            Screen s = Screen.FromControl(this);
            this.Location = new Point(0, 0);
            this.Size = new Size(s.Bounds.Width / 2, s.Bounds.Height / 3 * 2);
            logTextBox.MouseWheel += new MouseEventHandler(this.logTextBox_MouseWheel);
        }
        
        private void SetUpUI() {
            #region フォーム
            this.BackColor = ColorTranslator.FromHtml("#FF252526");
            #endregion

            #region ボタン
            this.closeButton.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
            this.closeButton.BackColor = ColorTranslator.FromHtml("#3F3F46");
            this.closeButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#555555");
            this.closeButton.FlatStyle = FlatStyle.Flat;
            #endregion

            #region グループ
            this.grpRecv.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
            #endregion

            #region テキストボックス
            this.logTextBox.ForeColor = ColorTranslator.FromHtml("#FEF1F1F1");
            this.logTextBox.ForeColor = ColorTranslator.FromHtml("#FF333337");
            #endregion
        }

        public void LogCallFunctionName() {
            if(logTextBox.IsDisposed || logTextBox == null) {
                return;
            }

            const int callerFrameIndex = 1;
            System.Diagnostics.StackFrame callerFrame = new System.Diagnostics.StackFrame(callerFrameIndex);
            System.Reflection.MethodBase callerMethod = callerFrame.GetMethod();
            logTextBox.AppendText("Function : " + callerMethod.Name + "\n");
        }

        /// <summary>
        /// ログ出力を行う
        /// </summary>
        /// <param name="status">ログとして残しておく文字列</param>
        public void LogStatus(string status) {
            logTextBox.AppendText(status);
        }
    }
}
