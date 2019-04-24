using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace EV3VisualLogger.UI.View {
    public partial class MySeries : Series {

        public MySeries(string name): base(name) {
            this.ChartType = SeriesChartType.Line;
            this.Color = ColorTranslator.FromHtml("#007ACC");
            this.MarkerSize = 3;
            this.MarkerStyle = MarkerStyle.Circle;
        }
    }
}
