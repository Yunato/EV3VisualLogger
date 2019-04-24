using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using EV3VisualLogger.UI.View;

namespace EV3VisualLogger.Model.Data {
    public class Column {

        private string _name;

        private List<double> values;
        public int ItemSize {
            get { return values.Count; }
        }

        private MySeries _series;
        public MySeries Series {
            get { return _series; }
        }

        public Column(string name) {
            _name = name;
            values = new List<double>();
            _series = new MySeries(name);
        }

        public string Name {
            get { return _name; }
        }

        public void AddValue(double value) {
            values.Add(value);
        }

        public void AddValueToSeries(double x, double y) {
            _series.Points.AddXY(x, y);
        }

        public double GetValue(int index) {
            return values[index];
        }

        public double GetLastValue() {
            return values[values.Count - 1];
        }
    }
}
