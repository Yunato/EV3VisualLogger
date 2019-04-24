namespace EV3VisualLogger.UI.View {
    partial class MyChart {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            this.Location = new System.Drawing.Point(0, 0);
            this.TabIndex = 0;
            this.TabStop = false;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#2D2D30");

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea =
                new System.Windows.Forms.DataVisualization.Charting.ChartArea("Area");
            chartArea.BackColor = System.Drawing.ColorTranslator.FromHtml("#2D2D30"); 
            chartArea.AxisX.LineColor = System.Drawing.ColorTranslator.FromHtml("#F1F1F1");
            chartArea.AxisY.LineColor = System.Drawing.ColorTranslator.FromHtml("#F1F1F1");
            chartArea.AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml("#434346");
            chartArea.AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml("#434346");
            chartArea.AxisX.LabelStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#F1F1F1");
            chartArea.AxisY.LabelStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#F1F1F1");
            chartArea.AxisX.ScrollBar.Enabled = true;
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.IsMarginVisible = false;
            chartArea.Position.Y = 10;
            chartArea.Position.Height = 97;
            chartArea.Position.Width = 97;
            this.ChartAreas.Add(chartArea);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(chart_MouseWheel);
            this.MouseLeave += new System.EventHandler(chart_MouseLeave);
            this.MouseEnter += new System.EventHandler(chart_MouseEnter);

        }

        #endregion
    }
}
