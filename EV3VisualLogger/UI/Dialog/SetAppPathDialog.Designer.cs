namespace EV3VisualLogger.UI.Dialog {
    partial class SetAppPathDialog {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.referButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // pathTextBox
            // 
            this.pathTextBox.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.pathTextBox.Location = new System.Drawing.Point(16, 8);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(352, 23);
            this.pathTextBox.TabIndex = 0;
            // 
            // referButton
            // 
            this.referButton.Font = new System.Drawing.Font("Meiryo UI", 9F);
            this.referButton.Location = new System.Drawing.Point(382, 8);
            this.referButton.Name = "referButton";
            this.referButton.Size = new System.Drawing.Size(96, 25);
            this.referButton.TabIndex = 1;
            this.referButton.Text = "参照";
            this.referButton.UseVisualStyleBackColor = true;
            this.referButton.Click += new System.EventHandler(this.referButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.applyButton.Location = new System.Drawing.Point(360, 41);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(112, 24);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "適用";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.ShowReadOnly = true;
            this.openFileDialog.Title = "appファイルのPATH設定";
            // 
            // SetAppPathDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 75);
            this.ControlBox = false;
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.referButton);
            this.Controls.Add(this.pathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SetAppPathDialog";
            this.Text = "GetPathDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button referButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}