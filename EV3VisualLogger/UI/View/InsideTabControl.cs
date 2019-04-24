using System.Drawing;
using System.Windows.Forms;
using EV3VisualLogger.Model.Data;

namespace EV3VisualLogger.UI.View {
    public partial class InsideTabControl : CustomTabControl {

        private Section _section;

        private InsideTabControl() {
            InitializeComponent();
        }

        public static InsideTabControl CreateInsideTabControl(Section section) {
            InsideTabControl tabControl = new InsideTabControl();
            tabControl._section = section;
            return tabControl;
        }

        public void Active(Size parentSize) {
            this.Size = new Size(parentSize.Width, parentSize.Height - 87);

            if (this.Width >= 140 * (this.TabCount + 1)){
                this.ItemSize = new Size(140, 20);
            }else {
                this.ItemSize = new Size((this.Width - 10) / (this.TabCount + 1), 20);
            }

            for(int tabPageIndex = 0; tabPageIndex < this.TabPages.Count; ++tabPageIndex) {
                TabPage tabPage = this.TabPages[tabPageIndex];
                MyChart chart = (MyChart)tabPage.Controls[0];
                chart.Size = new Size(this.Width - 2, this.Height - 60);
                chart.Series.Clear();
                chart.Series.Add(_section.Columns[tabPageIndex + 1].Series);
            }
            this.Show();
        }

        public void Deactive() {
            this.Hide();
        }

        /// <summary>
        /// MyChartオブジェクトのみを保持したタブページを作成する
        /// </summary>
        public void AddTabPage() {
            for (int index = 1; index < _section.Columns.Length; ++index) {
                TabPage tabPage = new TabPage();
                tabPage.Text = _section.Columns[index].Name;
                tabPage.BackColor = ColorTranslator.FromHtml("#2D2D30");
                tabPage.Size = new Size(this.Width, this.Height);
                tabPage.Controls.Add(new MyChart(tabPage, _section.Columns[index]));
                this.TabPages.Add(tabPage);
            }
        }
    }
}
