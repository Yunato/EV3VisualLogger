using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using EV3VisualLogger.Model.Bluetooth;
using EV3VisualLogger.Model.Data;
using EV3VisualLogger.UI.Dialog;
using EV3VisualLogger.UI.View;

namespace EV3VisualLogger.UI.MyForm {
    public partial class MainForm : Form {

        private LogForm LogForm = new LogForm();
        private Connection _connection;

        public const int MODE_TAB =    0;
        public const int MODE_GRID =   1;
        private int _mode = Properties.Settings.Default.mode;
        
        private int _connectionState = Connection.STATE_NONE;
        private static string _rootPath = Application.StartupPath + "\\OUTPUT\\";
        public static string RootPath {
            get { return _rootPath; }
        }

        public MainForm() {
            InitializeComponent();
            SetUpUI();

            #region //Bluetooth機器名前取得
            /*
            foreach (BluetoothDeviceInfo d in new BluetoothClient().DiscoverDevices())
            {
                Console.WriteLine(d.DeviceName + " " + d.DeviceAddress + " " +
                    d.Authenticated + " " + d.ClassOfDevice + " " +
                    d.Connected);
            }
            */
            #endregion
        }

        private void SetUpUI() {
            #region フォーム
            this.BackColor = ColorTranslator.FromHtml("#FF252526");
            #endregion

            #region ボタン
            List<Button> buttons = new List<Button>();
            buttons.Add(this.connectButton);
            buttons.Add(this.sendButton);
            buttons.Add(this.readFileButton);
            buttons.Add(this.saveDataButton);
            buttons.Add(this.resetButton);
            buttons.Add(this.exitButton);
            buttons.Add(this.modeChangeButton);
            buttons.Add(this.pathSetButton);
            buttons.Add(this.recEndButton);

            foreach (Button button in buttons) {
                button.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
                button.BackColor = ColorTranslator.FromHtml("#3F3F46");
                button.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#555555");
                button.FlatStyle = FlatStyle.Flat;
            }
            #endregion

            #region グループ
            List<GroupBox> grps = new List<GroupBox>();
            grps.Add(this.grpConnection);
            grps.Add(this.grpIO);
            grps.Add(this.grpOther);
            grps.Add(this.grpAppFile);

            foreach (GroupBox grp in grps) {
                grp.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
            }
            #endregion

            #region ラベル
            this.sendLabel.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
            #endregion

            #region テキストボックス
            this.sendTextBox.ForeColor = ColorTranslator.FromHtml("#FFF1F1F1");
            this.sendTextBox.BackColor = ColorTranslator.FromHtml("#FF333337");
            #endregion

            #region ステータス
            this.statusStrip.BackColor = ColorTranslator.FromHtml("#EF007ACC");
            this.statusStrip.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
            #endregion
        }

        private void MainForm_Load(object sender, EventArgs e) {
            SetStatusToBar("Setupping...");
            _connection = new Connection(serialPort);

            openFileDialog.InitialDirectory = _rootPath;
            if (!Directory.Exists(_rootPath)) {
                Directory.CreateDirectory(_rootPath);
            }
            if (IsAdministrator()) {
                LogForm.Show();
            }
            SetMode();
            SetStatusToBar("Ready");
        }

        private void SaveActiveSection(int index) {
            if(index < 0 || outsideTabControl.TabPages.Count - 1 < index) {
                return;
            }
            OutsideTabPage tabpage = (OutsideTabPage)outsideTabControl.TabPages[index];
            if (tabpage != null) {
                SetStatusToBar("Saving log data...");
                Model.IO.FileStream fs = new Model.IO.FileStream(tabpage.Section);
                fs.SaveFiles();
                SetStatusToBar("[Finish] Save");
            }
        }

        private void CreateOutTabPageOnReading(string[] fileNames) {
            SetStatusToBar("Reading log data...");
            ProgressDialog dialog = new ProgressDialog("ファイルを読込中...");
            dialog.Maximum = fileNames.Length;
            dialog.Show(this);

            foreach(string fileName in fileNames) {
                if (outsideTabControl.isOpen(fileName)) {
                    outsideTabControl.SelectedIndex = outsideTabControl.GetIndexForFileName(fileName);
                    continue;
                }

                Section section = Section.CreateReadSection(fileName);
                Model.IO.FileStream readFileStream = new Model.IO.FileStream(section);
                string[] readedData = readFileStream.AccessFile();
                if(readedData == null) {
                    continue;
                }

                outsideTabControl.AddTabPage(section, _mode, isReceived: false);
                OutsideTabPage tabpage = (OutsideTabPage)outsideTabControl.TabPages[outsideTabControl.SelectedIndex];

                List<string> LogData = new List<string>();
                for(int readIndex = 0; readIndex < readedData.Length - 1; ++readIndex) {
                    LogData.Add(readedData[readIndex]);
                }
                tabpage.RecordRowData(LogData.ToArray());
                tabpage.RecordMemo(readedData[readedData.Length - 1]);

                ++dialog.Value;
            }

            SetStatusToBar("[Finish] Read");
            dialog.Close();
        }
        
        private void ShowSendControl() {
            this.sendLabel.Visible = true;
            this.sendTextBox.Visible = true;
            this.recEndButton.Visible = true;
        }

        private void HideSendControl() {
            this.sendLabel.Visible = false;
            this.sendTextBox.Visible = false;
            this.recEndButton.Visible = false;
        }

        private void SetMode() {
            if(this._mode == MODE_TAB) {
                this.Size = new Size(1210, 581);
                this.WindowState = FormWindowState.Normal;
                this.modeChangeButton.Text = "GRIDモードへ";
            }else if(this._mode == MODE_GRID) {
                this.Location = new Point(0, 0);
                this.WindowState = FormWindowState.Maximized;
                this.modeChangeButton.Text = "TABモードへ";
            }
            outsideTabControl.ChangeMode(this._mode);
        }

        private void SetStatusToBar(string status) {
            this.statusStrip.Items[0].Text = status;
        }

        private bool IsAdministrator() {
            System.Security.Principal.WindowsIdentity wi = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal wp = new System.Security.Principal.WindowsPrincipal(wi);
            return wp.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// GRIDモード時の Form の移動をキャンセルする
        /// </summary>
        /// <param name="m">メッセージ</param>
        protected override void WndProc(ref Message m) {
            const int WM_SYSCOMMAND = 0x0112;
            const long SC_MOVE = 0xF010L;
            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_MOVE && _mode == MODE_GRID) {
                m.Result = IntPtr.Zero;
                return;
            }
            base.WndProc(ref m);
        }
    }
}
