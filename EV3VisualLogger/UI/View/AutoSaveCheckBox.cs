using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EV3VisualLogger.UI.View {
    public partial class AutoSaveCheckBox : CheckBox {

        public static bool IsChecked { get; private set; }

        public AutoSaveCheckBox() {
            InitializeComponent();
            CheckStateChanged += CheckBox_CheckStateChaged;
        }

        private void CheckBox_CheckStateChaged(object sender, EventArgs e) {
            switch (((CheckBox)sender).CheckState) {
                case CheckState.Checked:
                    IsChecked = true;
                    break;
                case CheckState.Unchecked:
                    IsChecked = false;
                    break;
                default:
                    break;
            }
        }
    }
}
