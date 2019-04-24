using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace EV3VisualLogger.UI.Dialog {
    partial class ProgressForm : System.Windows.Forms.Form {
        internal System.Windows.Forms.Label label;
        internal System.Windows.Forms.ProgressBar progressBar;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(16, 8);
            this.label.Name = "Label1";
            this.label.Size = new System.Drawing.Size(288, 40);
            this.label.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(8, 48);
            this.progressBar.Name = "ProgressBar1";
            this.progressBar.Size = new System.Drawing.Size(294, 23);
            this.progressBar.TabIndex = 1;
            // 
            // ProgressForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.ClientSize = new System.Drawing.Size(320, 120);
            this.ControlBox = false;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.ShowInTaskbar = false;
            this.Text = "ProgressForm";
            this.ResumeLayout(false);

        }

        #endregion
    }

    public class ProgressDialog : IDisposable {
        private volatile bool _canceled = false;
        private volatile ProgressForm form;
        private System.Threading.ManualResetEvent startEvent;
        private bool showed = false;
        private volatile bool closing = false;
        private Form ownerForm;
        private System.Threading.Thread thread;
        private volatile string _title = "ファイルの読み込み";
        private volatile int _minimum = 0;
        private volatile int _maximum = 1;
        private volatile int _value = 0;
        private volatile string _message = "";

        public ProgressDialog(string message) {
            this._message = message;
        }

        public string Title {
            set {
                _title = value;
                if (form != null)
                    form.Invoke(new MethodInvoker(SetTitle));
            }
            get {
                return _title;
            }
        }

        public int Minimum {
            set {
                _minimum = value;
                if (form != null)
                    form.Invoke(new MethodInvoker(SetProgressMinimum));
            }
            get {
                return _minimum;
            }
        }

        public int Maximum {
            set {
                _maximum = value;
                if (form != null)
                    form.Invoke(new MethodInvoker(SetProgressMaximum));
            }
            get {
                return _maximum;
            }
        }

        public int Value {
            set {
                _value = value;
                if (form != null)
                    form.Invoke(new MethodInvoker(SetProgressValue));
            }
            get {
                return _value;
            }
        }

        public string Message {
            set {
                _message = value;
                if (form != null) {
                    form.Invoke(new MethodInvoker(SetMessage));
                }
            }
            get {
                return _message;
            }
        }

        public bool Canceled {
            get { return _canceled; }
        }

        public void Show(Form owner) {
            if (showed)
                throw new Exception("ダイアログは一度表示されています。");
            showed = true;

            _canceled = false;
            startEvent = new System.Threading.ManualResetEvent(false);
            ownerForm = owner;

            thread = new System.Threading.Thread(
                new System.Threading.ThreadStart(Run));
            thread.IsBackground = true;
            thread.Start();

            startEvent.WaitOne();
        }

        public void Show() {
            Show(null);
        }

        private void Run() {
            form = new ProgressForm();
            form.Text = _title;
            form.Closing += new CancelEventHandler(form_Closing);
            form.Activated += new EventHandler(form_Activated);
            form.progressBar.Minimum = _minimum;
            form.progressBar.Maximum = _maximum;
            form.progressBar.Value = _value;
            if (ownerForm != null) {
                form.StartPosition = FormStartPosition.Manual;
                form.Left = ownerForm.Left + (ownerForm.Width - form.Width) / 2;
                form.Top = ownerForm.Top + (ownerForm.Height - form.Height) / 2;
            }
            form.ShowDialog();
            form.Dispose();
        }

        public void Close() {
            closing = true;
            form.Invoke(new MethodInvoker(form.Close));
        }

        public void Dispose() {
            form.Invoke(new MethodInvoker(form.Dispose));
        }

        private void SetProgressValue() {
            if (form != null && !form.IsDisposed)
                form.progressBar.Value = _value;
        }

        private void SetMessage() {
            if (form != null && !form.IsDisposed)
                form.label.Text = _message;
        }

        private void SetTitle() {
            if (form != null && !form.IsDisposed)
                form.Text = _title;
        }

        private void SetProgressMaximum() {
            if (form != null && !form.IsDisposed)
                form.progressBar.Maximum = _maximum;
        }

        private void SetProgressMinimum() {
            if (form != null && !form.IsDisposed)
                form.progressBar.Minimum = _minimum;
        }

        private void Button1_Click(object sender, EventArgs e) {
            _canceled = true;
        }

        private void form_Closing(object sender, CancelEventArgs e) {
            if (!closing) {
                e.Cancel = true;
                _canceled = true;
            }
        }

        private void form_Activated(object sender, EventArgs e) {
            form.Activated -= new EventHandler(form_Activated);
            startEvent.Set();
        }
    }
}
