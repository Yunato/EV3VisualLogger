using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EV3VisualLogger.UI.Dialog {
    public partial class ProgressForm : Form {

        public ProgressForm() {
            InitializeComponent();
            SetUpUI();
            this.TopMost = true;
        }

        private void SetUpUI() {
            #region フォーム
            this.BackColor = ColorTranslator.FromHtml("#FF252526");
            #endregion

            #region ラベル
            this.label.ForeColor = ColorTranslator.FromHtml("#F1F1F1");
            #endregion
        }
    }
}
