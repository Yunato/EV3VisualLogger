using System.Drawing;
using System.Windows.Forms;
using EV3VisualLogger.Model.Data;
using EV3VisualLogger.UI;

namespace EV3VisualLogger.UI.View {
    public partial class GridCharts : Panel {

        private Section _section;

        private GridCharts() {
            InitializeComponent();
        }

        public static GridCharts CreateGridCharts(Section section) {
            GridCharts gridCharts = new GridCharts();
            gridCharts._section = section;
            return gridCharts;
        }

        public void Active(Size parentSize) {
            this.Size = new Size(parentSize.Width, parentSize.Height - 87);

            int rowSize = 0;
            int dataSize = _section.Columns.Length - 1;
            if(dataSize % 2 == 0) {
                rowSize = dataSize / 2;
            }else {
                rowSize = (dataSize / 2) + 1;
            }

            int index = 1;
            for(int grpBoxIndex = 0; grpBoxIndex < this.Controls.Count; ++grpBoxIndex) {
                GroupBox grpBox = (GroupBox)this.Controls[grpBoxIndex];
                grpBox.Size = new Size(this.Width / 2 - 16, this.Height / rowSize);
                grpBox.Location = new Point(
                    (this.Width / 2) * ((index - 1) / rowSize) + 6, 
                    (this.Height / rowSize) * ((index - 1) % rowSize));

                MyChart chart = (MyChart)grpBox.Controls[0];
                chart.Size = new Size(grpBox.Width - 2, grpBox.Height - 18);
                chart.Series.Clear();
                chart.Series.Add(_section.Columns[grpBoxIndex + 1].Series);
                ++index;
            }
            this.Show();
        }
        
        public void Deactive() {
            this.Hide();
        }

        public void AddChart() {
            for (int index = 1; index < _section.Columns.Length; ++index) {
                GroupBox grpBox = new GroupBox();
                grpBox.Text = _section.Columns[index].Name;
                grpBox.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
                grpBox.Controls.Add(new MyChart(grpBox, _section.Columns[index]));
                this.Controls.Add(grpBox);
            }
        }
    }
}
