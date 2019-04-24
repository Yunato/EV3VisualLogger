using System.IO.Ports;
using System.Management;
using System.Windows.Forms;

namespace EV3VisualLogger.UI.View {
    public partial class PortNameComboBox : ComboBox {

        public PortNameComboBox() {
            InitializeComponent();
            load();
        }

        /// <summary>
        /// 使用しているPCに登録されたポートを調べる
        /// </summary>
        public void load() {
            string[] deviceNames = ExploreDeviceNames();
            if (deviceNames != null) {
                foreach (string deviceName in deviceNames) {
                    char[] del = { '(', ')' };
                    string[] separatedName = deviceName.Split(del);
                    if (separatedName.Length == 3) {
                        this.Items.Add(separatedName[1]);
                    }
                }
            }

            if (this.Items.Count == 0) {
                string[] portNames = SerialPort.GetPortNames();
                foreach (string portName in portNames) {
                    this.Items.Add(portName);
                }
            }
        }

        /// <summary>
        /// 使用できるポートを取得する
        /// </summary>
        /// <returns>ポート配列</returns>
        public string[] ExploreDeviceNames() {
            var deviceNameList = new System.Collections.ArrayList();
            var check = new System.Text.RegularExpressions.Regex("(COM[1-9][0-9]?[0-9]?)");

            ManagementClass mcPnPEntity = new ManagementClass("Win32_PnPEntity");
            ManagementObjectCollection manageObjCol = mcPnPEntity.GetInstances();

            foreach (ManagementObject manageObj in manageObjCol) {
                var namePropertyValue = manageObj.GetPropertyValue("Name");
                if (namePropertyValue == null) {
                    continue;
                }

                string name = namePropertyValue.ToString();
                if (check.IsMatch(name)) {
                    deviceNameList.Add(name);
                }
            }

            if (deviceNameList.Count <= 0) {
                return null;
            }
            string[] deviceNames = new string[deviceNameList.Count];
            int index = 0;
            foreach (var name in deviceNameList) {
                deviceNames[index++] = name.ToString();
            }
            return deviceNames;
        }
    }
}
