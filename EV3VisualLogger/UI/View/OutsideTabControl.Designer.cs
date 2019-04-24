namespace EV3VisualLogger.UI.View {
    partial class OutsideTabControl {
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
            this.TabIndex = 0;
            this.SelectedIndex = 0;
            this.TabStop = false;
            this.DisplayStyle = System.Windows.Forms.TabStyle.VS2010;
            this.DisplayStyleProvider.ShowTabCloser = false;
            this.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.ItemSize = new System.Drawing.Size(140, 20);
            this.DisplayStyleProvider.ShowTabCloser = true;
            this.SelectedIndexChanged += new System.EventHandler(OutsideTabControl_OnSelectedIndexChanged);
            this.TabClosing += new System.EventHandler<System.Windows.Forms.TabControlCancelEventArgs>(OutsideTabControl_TabClosing);
        }

        #endregion
    }
}
