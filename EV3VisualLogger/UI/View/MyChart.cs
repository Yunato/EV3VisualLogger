using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using EV3VisualLogger.Model.Data;

namespace EV3VisualLogger.UI.View {
    public partial class MyChart : Chart {

        public MyChart(Control parentControl, Column column) {
            InitializeComponent();

            this.Location = new Point(1, 16);
            this.Series.Add(column.Series);
        }

        private void chart_MouseWheel(object sender, MouseEventArgs e) {
            Series series = ((Chart)sender).Series[0];
            double viewdMin = ((Chart)sender).ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double viewdMax = ((Chart)sender).ChartAreas[0].AxisX.ScaleView.ViewMaximum;
            double viewdDiff = Math.Abs(viewdMax - viewdMin);

            double changeRate = e.Delta > 0 ? Math.Truncate(viewdDiff / 3) : Math.Ceiling((viewdDiff + 10) * 0.75);
            double viewdStartTime = ((Chart)sender).ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - changeRate;
            double viewdEndTime = ((Chart)sender).ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + changeRate;

            if (viewdStartTime < series.Points[0].XValue) {
                viewdEndTime += Math.Abs(series.Points[0].XValue - viewdStartTime);
                viewdStartTime = series.Points[0].XValue;
            }

            if (viewdEndTime > series.Points[series.Points.Count - 1].XValue) {
                viewdStartTime -= Math.Abs(viewdEndTime - series.Points[series.Points.Count - 1].XValue);
                viewdEndTime = series.Points[series.Points.Count - 1].XValue;
                viewdStartTime = viewdStartTime < series.Points[0].XValue ? series.Points[0].XValue : viewdStartTime;
            }

            viewdStartTime = Math.Truncate(viewdStartTime);
            viewdEndTime = Math.Ceiling(viewdEndTime);
            ((Chart)sender).ChartAreas[0].AxisX.ScaleView.Zoom(viewdStartTime, viewdEndTime);
        }       

        private void chart_MouseLeave(object sender, EventArgs e) {
            if (((Chart)sender).Focused) {
                ((Chart)sender).Parent.Focus();
            }
        }

        private void chart_MouseEnter(object sender, EventArgs e) {
            if (!((Chart)sender).Focused) {
                ((Chart)sender).Focus();
            }
        }
    }
}
