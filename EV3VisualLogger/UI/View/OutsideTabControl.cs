using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EV3VisualLogger.Model.Data;
using EV3VisualLogger.UI.MyForm;

namespace EV3VisualLogger.UI.View {
    public partial class OutsideTabControl : CustomTabControl {

        private int _receivedIndex = -1;
        public int ReceivedIndex {
            get {
                if(_receivedIndex < 0 || _receivedIndex > this.TabPages.Count - 1) {
                    return -1;
                }
                return _receivedIndex;
            }
        }

        private List<string> fileNames = new List<string>();

        public OutsideTabControl() {
            InitializeComponent();
        }

        public void AddTabPage(Section section, int mode, bool isReceived = false) {
            OutsideTabPage tabPage = new OutsideTabPage(section, mode);
            tabPage.Size = new Size(this.Width, this.Height);
            if (this.Width < this.ItemSize.Width * (this.TabCount + 1)) {
                this.ItemSize = new Size((this.Width - 50) / (this.TabCount + 1), 20);
            }
            this.Controls.Add(tabPage);

            this.SelectedIndex = this.TabCount - 1;
            if (isReceived) {
                _receivedIndex = this.SelectedIndex;
            }
            this.Size = new Size(this.Parent.Width - 40, this.Parent.Height - 111 - this.Location.Y);
            this.Show();

            fileNames.Add(section.FileName);
        }

        public void Reset() {
            this.TabPages.Clear();
            this.fileNames.Clear();
            this.Hide();
            _receivedIndex = -1;
        }

        public bool isOpen(string fileName) {
            fileName = fileName.Remove(0, fileName.LastIndexOf('\\') + 1);
            fileName = fileName.Remove(fileName.LastIndexOf('.'));
            return fileNames.Contains(fileName);
        }

        public int GetIndexForFileName(string fileName) {
            fileName = fileName.Remove(0, fileName.LastIndexOf('\\') + 1);
            fileName = fileName.Remove(fileName.LastIndexOf('.'));
            return fileNames.IndexOf(fileName);
        }

        public void ChangeMode(int mode) {
            foreach(OutsideTabPage tabPage in this.TabPages) {
                tabPage.Mode = mode;
                tabPage.CommitMode();
            }

            if (this.Width >= 140 * (this.TabCount + 1)){
                this.ItemSize = new Size(140, 20);
            }else {
                this.ItemSize = new Size((this.Width - 10) / (this.TabCount + 1), 20);
            }
        }
    }
}
