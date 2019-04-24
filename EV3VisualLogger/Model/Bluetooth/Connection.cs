using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EV3VisualLogger.Model.Data;
using EV3VisualLogger.UI.Dialog;

namespace EV3VisualLogger.Model.Bluetooth {
    class Connection {

        public const int STATE_NONE                    = 0;
        public const int STATE_CONNECTING_FILEDATA     = 1;
        public const int STATE_CONNECTING_FILECONTENT  = 2;
        public const int STATE_CONNECTING_FIN          = 3;
        public const int STATE_CONNECTED               = 4;

        private SerialPort serialPort;
        private Section _section;
        public Section ConnectingSection {
            get { return _section; }
        }

        private bool _isReceive;
        public bool IsReceive {
            set { _isReceive = value; }
            get { return _isReceive; }
        }

        private string appFilePath = string.Empty;

        public Connection(SerialPort serialPort) {
            this.serialPort = serialPort;
        }

        /// <summary>
        /// EV3との接続を開始する
        /// </summary>
        /// <param name="portName">Port名 (COM3等)</param>
        /// <returns>接続結果</returns>
        public bool Start(string portName) {
            if (serialPort.IsOpen == true) {
                Close();
                return false;
            }

            MessageDialog dialog = new MessageDialog("接続中", null, "EV3と接続しています．");
            dialog.Show();
            dialog.Refresh();
            SetUpSerialPort(portName);
            try {
                serialPort.Open();
            }
            catch (Exception ex) {
                dialog.Close();
                MessageBox.Show(ex.Message);
                return false;
            }
            dialog.Close();
            _section = null;
            return true;
        }

        /// <summary>
        /// SerialPortの初期設定を行う
        /// </summary>
        /// <param name="portName">Port名 (COM3等)</param>
        private void SetUpSerialPort(string portName) {
            serialPort.PortName = portName;
            serialPort.BaudRate = 115200;
            serialPort.DataBits = 8;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;
            serialPort.WriteTimeout = 100;
        }

        /// <summary>
        /// 信号を送信する
        /// </summary>
        /// <param name="packet">送信データ</param>
        private bool Write(Packet packet) {
            try {
                serialPort.Write(packet.Get(), 0, packet.GetSize());
            }catch(TimeoutException ex) {
                MessageBox.Show(ex.Message);
                return false;
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// ヘッダーを送信する
        /// </summary>
        /// <param name="section">受信セクション</param>
        /// <returns>送信結果</returns>
        public bool SendHeader(Section section) {
            _section = section;
            appFilePath = Properties.Settings.Default.appFilePath;

            Packet packet = new Packet();
            packet.Add(new byte[] {
                0x72, 0x7A, 0x0D, 0x2A, 0x2A, 0x18, 0x42, 0x30,
                0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
                0x30, 0x30, 0x30, 0x30, 0x30, 0x8D, 0x8A, 0x11 });

            #region Send ZRQINIT
            bool isSuccess = Write(packet);
            return isSuccess;
            #endregion
        }

        /// <summary>
        /// ファイルの付属情報を送信する　(ファイル名，ファイルサイズ等)
        /// </summary>
        public void SendFileData() {
            Packet packet = new Packet();
            FileInfo fi = new FileInfo(appFilePath);

            packet.Add(new byte[] { 0x2A, 0x18, 0x41, 0x04, 0x00, 0x00, 0x00, 0x01, 0x99, 0x27 });

            #region ファイル名をbyte配列へ変換 -> 送信データへ追加
            byte[] FileName = Encoding.GetEncoding("utf-8").GetBytes(fi.Name);
            packet.Add(FileName);
            packet.Add(new byte[] { 0x00 });
            #endregion

            #region ファイルサイズをbyte配列へ変換 -> 送信データへ追加
            byte[] FileSize = Encoding.GetEncoding("utf-8").GetBytes(fi.Length.ToString());
            packet.Add(FileSize);
            packet.Add(new byte[] { 0x20 });
            #endregion

            #region ファイル最終更新時刻をbyte配列へ変換 -> 送信データへ追加
            long time = GetUnixTime(fi.LastWriteTime);
            byte[] FileUpdateTime = System.Text.Encoding.GetEncoding("utf-8").GetBytes(Convert.ToString(time, 8));
            packet.Add(FileUpdateTime);
            packet.Add(new byte[] { 0x20 });
            #endregion

            #region ファイルパーミッションをbyte配列へ変換 -> 送信データへ追加
            byte[] FilePermission = new byte[] { 0x31, 0x30, 0x30, 0x36, 0x34, 0x34 };
            packet.Add(FilePermission);
            packet.Add(new byte[] { 0x00 });
            #endregion

            #region CRC
            packet.InitCRC();
            packet.UpdateCRCForAll(start: 10);
            packet.Add(new byte[] { 0x18, 0x6B });
            packet.UpdateCRCLastData();
            packet.DecideCRC();
            #endregion

            #region Send ZsbHdr & ZSendFileDat
            Write(packet);
            #endregion
        }

        private static DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        private static long GetUnixTime(DateTime targetTime)
        {
            targetTime = targetTime.ToUniversalTime();
            TimeSpan elapsedTime = targetTime - UNIX_EPOCH;
            return (long)elapsedTime.TotalSeconds;
        }

        /// <summary>
        /// ファイルの中身を送信する
        /// </summary>
        public void SendFileContent() {
            FilePacket packet = new FilePacket();
            packet.Add(new byte[] { 0x2A, 0x18, 0x41, 0x18, 0x4A, 0x00, 0x00, 0x00, 0x00, 0x46, 0xAE });

            #region Send ZsbHdr & ZSendFileDat
            Write(packet);
            #endregion

            packet.Init();
            FileStream fs = new FileStream(appFilePath, FileMode.Open, FileAccess.Read);
            byte[] content = new byte[fs.Length];
            fs.Read(content, 0, content.Length);
            long fileSize = fs.Length;
            fs.Close();

            const int MaxDataLen = 1024;
            int head = 0;
            int signalSize = 0;
            long LastPos = 0;

            #region ファイルの中身をbyte配列へ変換 -> 送信データへ追加
            while (head != fileSize) {
                FilePacket contentPacket = new FilePacket();
                
                while (head != fileSize) {
                    if (contentPacket.GetSize() > MaxDataLen - 2) {
                        break;
                    }
                    contentPacket.AddFileContent(content[head]);
                    ++signalSize;
                    ++head;
                }
                contentPacket.Add(new byte[] { 0x18 });

                int WinSize = 32767;
                char b;
                if (signalSize >= fileSize) {
                    b = 'h';
                } else if (signalSize - LastPos > WinSize) {
                    b = 'j';
                } else {
                    b = 'i';
                }

                contentPacket.Add(new byte[] { Convert.ToByte(b) });
                contentPacket.UpdateCRCLastData();
                contentPacket.DecideCRC();
                contentPacket.CopyCRCTo(packet);

                #region Send ZsbHdr & ZSendFileDat
                Write(contentPacket);
                #endregion

                if (b == 'j'){
                    LastPos = signalSize;
                }
            }
            packet.DecideEOF(signalSize);
            #endregion

            #region Send ZsbHdr & ZSendFileDat
            Write(packet);
            #endregion
        }

        /// <summary>
        /// フッターを送信する
        /// </summary>
        public void SendFin(){
            Packet packet = new Packet();
            packet.Add(new byte[] {
                0x2A, 0x2A, 0x18, 0x42, 0x30, 0x38, 0x30,
                0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
                0x30, 0x32, 0x32, 0x64, 0x8D, 0x8A });

            #region Send ZFIN
            Write(packet);
            #endregion
            _isReceive = true;
        }

        public void SendCharData(string data) {
            if (!serialPort.IsOpen) {
                return;
            }

            if (string.IsNullOrWhiteSpace(data)) {
                return;
            }

            try {
                serialPort.Write(data);
            }catch (Exception ex){
                MessageBox.Show(ex.Message);
            }

        }

        public void Close() {
            if (serialPort.IsOpen){
                bool isClosed = false;
                Thread CloseThread = new Thread(() => {
                    serialPort.Close();
                    isClosed = true;
                });
                CloseThread.IsBackground = true;
                CloseThread.Start();

                while (!isClosed) {
                    Thread.Sleep(10);
                }
            }
        }
    }
}
