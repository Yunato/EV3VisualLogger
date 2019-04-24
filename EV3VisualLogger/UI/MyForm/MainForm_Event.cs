using System;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using EV3VisualLogger.Model.Bluetooth;
using EV3VisualLogger.Model.Data;
using EV3VisualLogger.Model.IO;
using EV3VisualLogger.UI.Dialog;
using EV3VisualLogger.UI.View;

namespace EV3VisualLogger.UI.MyForm {
    public partial class MainForm : Form{

        private void connectButton_Click(object sender, EventArgs e){
            LogForm.LogCallFunctionName();
            string portName = portNameComboBox.SelectedItem.ToString();
            SetStatusToBar("Connecting...");
            bool isSuccess = _connection.Start(portName);

            if (isSuccess) {
                connectButton.Text = "切断";
                sendButton.Enabled = true;
                HideSendControl();
                SetStatusToBar("[Success] Connect");
                LogForm.LogStatus("[Success] Connect");
            } else {
                connectButton.Text = "接続";
                sendButton.Enabled = false;
                SetStatusToBar("[Failed] Connect");
                LogForm.LogStatus("[Failed] Connect");
            }
        }

        private void sendButton_Click(object sender, EventArgs e){
            LogForm.LogCallFunctionName();
            SaveActiveSection(outsideTabControl.SelectedIndex);
            SaveActiveSection(outsideTabControl.ReceivedIndex);
            _connectionState = Connection.STATE_NONE;

            Section section = Section.CreateReceiveSection();
            SetStatusToBar("Sending header...");
            LogForm.LogStatus("Sending header...");
            bool isSuccess = _connection.SendHeader(section);
            if (isSuccess) {
                SetStatusToBar("[Success] Send header");
                LogForm.LogStatus("[Success] Send header");
                HideSendControl();
            } else {
                _connection.Close();
                connectButton.Text = "接続";
                sendButton.Enabled = false;
                SetStatusToBar("[Failed] Send header");
                LogForm.LogStatus("[Failed] Send header");
            }
        }

        private void exitButton_Click(object sender, EventArgs e){
            LogForm.LogCallFunctionName();
            SetStatusToBar("Closing...");
            LogForm.LogStatus("Closing...");
            _connectionState = Connection.STATE_NONE;
            Close();
        }

        private void saveDataButton_Click(object sender, EventArgs e) {
            LogForm.LogCallFunctionName();
            SaveActiveSection(outsideTabControl.SelectedIndex);
            SaveActiveSection(outsideTabControl.ReceivedIndex);
        }

        private void readFileButton_Click(object sender, EventArgs e) {
            LogForm.LogCallFunctionName();
            this.SuspendLayout();
            SaveActiveSection(outsideTabControl.SelectedIndex);
            SaveActiveSection(outsideTabControl.ReceivedIndex);
            _connectionState = Connection.STATE_NONE;
            HideSendControl();

            if (openFileDialog.ShowDialog(this) == DialogResult.OK) {
                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                CreateOutTabPageOnReading(openFileDialog.FileNames);
                sw.Stop();
                LogForm.LogStatus($"ファイル読み取り時間 : {sw.ElapsedMilliseconds}ミリ秒");
            }
            this.ResumeLayout();
        }

        private void resetButton_Click(object sender, EventArgs e) {
            LogForm.LogCallFunctionName();
            SaveActiveSection(outsideTabControl.SelectedIndex);
            SaveActiveSection(outsideTabControl.ReceivedIndex);
            _connectionState = Connection.STATE_NONE;
            outsideTabControl.Reset();
            HideSendControl();
            SetStatusToBar("Reset");
            LogForm.LogStatus("Reset");
        }

        private void modeChangeButton_Click(object sender, EventArgs e) {
            LogForm.LogCallFunctionName();
            this._mode ^= 1;
            Properties.Settings.Default["mode"] = _mode;
            Properties.Settings.Default.Save();
            SetMode();
        }

        private void pathSetButton_Click(object sender, EventArgs e) {
            string appFilePath = SetAppPathDialog.ShowSetAppPathDialog(Properties.Settings.Default.appFilePath);
            if (!string.IsNullOrWhiteSpace(appFilePath)) {
                Properties.Settings.Default["appFilePath"] = appFilePath;
                Properties.Settings.Default.Save();
            }
        }

        private void portNameComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            connectButton.Enabled = true;
        }
        
        private void sendTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == (char)Keys.Enter) {
                e.Handled = true;
                string signal = string.IsNullOrWhiteSpace(sendTextBox.Text) ? "1" : sendTextBox.Text;
                _connection.SendCharData(signal);
                sendTextBox.Clear();
            }
        }

        private void recEndButton_Click(object sender, EventArgs e) {
            _connection.IsReceive = false;
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e) {
            string[] dataArgs = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            string fileName = dataArgs[0];
            bool isDropped = e.Data.GetDataPresent(DataFormats.FileDrop) ? true : false;
            if (isDropped && FileStream.CheckFileTypeIsCsv(fileName)) {
                e.Effect = DragDropEffects.All;
            } else {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e) {
            LogForm.LogCallFunctionName();
            this.SuspendLayout();
            SaveActiveSection(outsideTabControl.SelectedIndex);
            SaveActiveSection(outsideTabControl.ReceivedIndex);
            _connectionState = Connection.STATE_NONE;

            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            CreateOutTabPageOnReading(fileNames);
            this.ResumeLayout();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e){
            _connection.Close();
            for(int index = 0; index < outsideTabControl.TabPages.Count; ++index) {
                SaveActiveSection(index);
            }
            SaveActiveSection(outsideTabControl.ReceivedIndex);
        }

        private delegate void Delegate_AddTabOnOutSideTabControl(Section section, int mode, bool isReceived);
        private delegate void Delegate_RecievedData(string data);
        private delegate void Delegate_ShowSendControl();
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e){
            if (!serialPort.IsOpen){
                return;
            }

            try{
                string receivedData = serialPort.ReadExisting();
                switch (_connectionState){
                    case Connection.STATE_NONE:
                        if (receivedData.Equals("**B0100000023be50\r\n")) {
                            SetStatusToBar("Sending metadata...");
                            _connection.SendFileData();
                            _connectionState = Connection.STATE_CONNECTING_FILEDATA;
                        }
                        break;
                    case Connection.STATE_CONNECTING_FILEDATA:
                        SetStatusToBar("Sending file contents...");
                        _connection.SendFileContent();
                        _connectionState = Connection.STATE_CONNECTING_FILECONTENT;
                        Thread.Sleep(2000);
                        break;
                    case Connection.STATE_CONNECTING_FILECONTENT:
                        SetStatusToBar("Sending footer...");
                        _connection.SendFin();
                        _connectionState = Connection.STATE_CONNECTING_FIN;
                        break;
                    case Connection.STATE_CONNECTING_FIN:
                        SetStatusToBar("Receiving...");
                        BeginInvoke(
                            new Delegate_AddTabOnOutSideTabControl(outsideTabControl.AddTabPage),
                            new Object[] { _connection.ConnectingSection, _mode, true });
                        BeginInvoke(
                            new Delegate_ShowSendControl(this.ShowSendControl),
                            new Object[] { });
                        _connectionState = Connection.STATE_CONNECTED;
                        break;
                    case Connection.STATE_CONNECTED:
                        OutsideTabPage tabpage = (OutsideTabPage)outsideTabControl.TabPages[outsideTabControl.ReceivedIndex];
                        if (_connection.IsReceive) {
                            BeginInvoke(
                                new Delegate_RecievedData(tabpage.RecordData),
                                new Object[] { receivedData });
                        }
                        break;
                }
            }catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}
