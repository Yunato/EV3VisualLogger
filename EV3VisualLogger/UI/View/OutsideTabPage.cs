using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EV3VisualLogger.Model.Data;
using EV3VisualLogger.UI.MyForm;

namespace EV3VisualLogger.UI.View {
    public partial class OutsideTabPage : TabPage {

        private Section _section;
        public Section Section {
            get { return _section; }
        }

        private InsideTabControl _tabControl;
        private GridCharts _gridCharts;
        private MyTextBox _textBox;

        private int _mode = Properties.Settings.Default.mode;
        public int Mode {
            set { _mode = value; }
        }

        public OutsideTabPage(Section section, int mode) {
            InitializeComponent();

            _section = section;
            _mode = mode;
            this.Text = section.FileName;
            this.BackColor = ColorTranslator.FromHtml("#2D2D30");

            CreateInsideTabControl();
            CreateGridCharts();
            CreateTextBox();
        }

        private void CreateInsideTabControl() {
            _tabControl = InsideTabControl.CreateInsideTabControl(_section);
            this.Controls.Add(_tabControl);
        }

        private void CreateGridCharts() {
            _gridCharts = GridCharts.CreateGridCharts(_section);
            this.Controls.Add(_gridCharts);
        }

        private void CreateTextBox() {
            _textBox = new MyTextBox();
            _textBox.TextChanged += (object sender, EventArgs e) => {
                _section.ChangeMemo(_textBox.Text);
            };
            this.Controls.Add(_textBox);
        }

        public void CommitMode() {
            this.Size = new Size(this.Parent.Width, this.Parent.Height);
            _textBox.Size = new Size(this.Parent.Width - 16, 70);
            _textBox.Location = new Point(4, this.Parent.Height - 110);
            if(_mode == MainForm.MODE_TAB) {
                _tabControl.Active(this.Size);
                _gridCharts.Deactive();
            }else if(_mode == MainForm.MODE_GRID){
                _tabControl.Deactive();
                _gridCharts.Active(this.Size);
            }
        }

        public void RecordData(string data) {
            Column[] columns = _section.Columns;
            _section.IsUpdate = true;

            data = data.Replace("\n", "");
            if (columns == null) {
                _section.CreateColumns(CountTabChar(data), data.Split('\t'));
                _tabControl.AddTabPage();
                _gridCharts.AddChart();
                CommitMode();
                return;
            }

            if (CountTabChar(data) % columns.Length != 0) {
                return;
            }
            
            string[] values = data.Split('\t').ToArray();
            for(int valueIndex = 0; valueIndex < values.Length; ++valueIndex) {
                if (string.IsNullOrWhiteSpace(values[valueIndex])) {
                    continue;
                }

                int columnIndex = valueIndex % columns.Length;
                double value = double.Parse(values[valueIndex]);
                columns[columnIndex].AddValue(value);

                if (columnIndex == 0) {
                    continue;
                }
                columns[columnIndex].AddValueToSeries(
                    _section.Columns[0].GetLastValue(),
                    _section.Columns[columnIndex].GetLastValue());
            }
        }

        public void RecordData(string[] data) {
            foreach(string datum in data) {
                RecordData(datum);
            }
        }

        public void RecordRowData(string[] rowData) {
            List<string> columnNames = new List<string>();
            foreach(string rowDatum in rowData) {
                string data = rowDatum.Split(',')[0];
                if (!string.IsNullOrWhiteSpace(data)) {
                    columnNames.Add(data);
                }
            }

            _section.CreateColumns(columnNames.Count, columnNames.ToArray());
            _tabControl.AddTabPage();
            _gridCharts.AddChart();
            CommitMode();

            {
                string[] data = rowData[0].Split(',');
                for(int dataIndex = 1; dataIndex < data.Length; ++dataIndex) {
                    if (string.IsNullOrWhiteSpace(data[dataIndex])) {
                        continue;
                    }
                    double value = double.Parse(data[dataIndex]);
                    _section.Columns[0].AddValue(value);
                }
            }

            for(int columnIndex = 1; columnIndex < _section.Columns.Length; ++columnIndex) {
                string[] data = rowData[columnIndex].Split(',');
                for(int dataIndex = 1; dataIndex < data.Length; ++dataIndex) {
                    if (string.IsNullOrWhiteSpace(data[dataIndex])) {
                        continue;
                    }
                    double value = double.Parse(data[dataIndex]);
                    _section.Columns[columnIndex].AddValue(value);
                    _section.Columns[columnIndex].AddValueToSeries(
                        _section.Columns[0].GetValue(dataIndex - 1),
                        _section.Columns[columnIndex].GetLastValue());
                }
            }
        }
        
        /// <summary>
        /// 文字列に含まれるタブ文字を数える
        /// </summary>
        /// <param name="s">対象文字列</param>
        /// <returns>タブ文字の個数</returns>
        private int CountTabChar(string s) {
            return s.Length - s.Replace("\t", "").Length;
        }

        public void RecordMemo(string data) {
            _textBox.Text = data;
        }
    }
}
