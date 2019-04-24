namespace EV3VisualLogger.UI.MyForm
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.portNameComboBox = new EV3VisualLogger.UI.View.PortNameComboBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.grpIO = new System.Windows.Forms.GroupBox();
            this.autoSaveCheckBox = new EV3VisualLogger.UI.View.AutoSaveCheckBox();
            this.saveDataButton = new System.Windows.Forms.Button();
            this.readFileButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.grpOther = new System.Windows.Forms.GroupBox();
            this.modeChangeButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pathSetButton = new System.Windows.Forms.Button();
            this.grpAppFile = new System.Windows.Forms.GroupBox();
            this.outsideTabControl = new EV3VisualLogger.UI.View.OutsideTabControl();
            this.sendLabel = new System.Windows.Forms.Label();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.recEndButton = new System.Windows.Forms.Button();
            this.grpConnection.SuspendLayout();
            this.grpIO.SuspendLayout();
            this.grpOther.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.grpAppFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.portNameComboBox);
            this.grpConnection.Controls.Add(this.sendButton);
            this.grpConnection.Controls.Add(this.connectButton);
            this.grpConnection.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpConnection.Location = new System.Drawing.Point(16, 8);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(346, 52);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "EV3接続";
            // 
            // portNameComboBox
            // 
            this.portNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portNameComboBox.DropDownWidth = 96;
            this.portNameComboBox.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.portNameComboBox.FormattingEnabled = true;
            this.portNameComboBox.Location = new System.Drawing.Point(16, 18);
            this.portNameComboBox.Name = "portNameComboBox";
            this.portNameComboBox.Size = new System.Drawing.Size(96, 23);
            this.portNameComboBox.TabIndex = 5;
            this.portNameComboBox.SelectedIndexChanged += new System.EventHandler(this.portNameComboBox_SelectedIndexChanged);
            // 
            // sendButton
            // 
            this.sendButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sendButton.Enabled = false;
            this.sendButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sendButton.Location = new System.Drawing.Point(236, 17);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(96, 25);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "appファイル送信";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Enabled = false;
            this.connectButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.connectButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.connectButton.Location = new System.Drawing.Point(126, 17);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(96, 25);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "接続";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // grpIO
            // 
            this.grpIO.Controls.Add(this.autoSaveCheckBox);
            this.grpIO.Controls.Add(this.saveDataButton);
            this.grpIO.Controls.Add(this.readFileButton);
            this.grpIO.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpIO.Location = new System.Drawing.Point(376, 8);
            this.grpIO.Name = "grpIO";
            this.grpIO.Size = new System.Drawing.Size(324, 52);
            this.grpIO.TabIndex = 1;
            this.grpIO.TabStop = false;
            this.grpIO.Text = "ファイル操作";
            // 
            // autoSaveCheckBox
            // 
            this.autoSaveCheckBox.AutoSize = true;
            this.autoSaveCheckBox.Checked = true;
            this.autoSaveCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoSaveCheckBox.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.autoSaveCheckBox.Location = new System.Drawing.Point(236, 22);
            this.autoSaveCheckBox.Name = "autoSaveCheckBox";
            this.autoSaveCheckBox.Size = new System.Drawing.Size(74, 19);
            this.autoSaveCheckBox.TabIndex = 6;
            this.autoSaveCheckBox.Text = "自動保存";
            this.autoSaveCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveDataButton
            // 
            this.saveDataButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.saveDataButton.Location = new System.Drawing.Point(126, 18);
            this.saveDataButton.Name = "saveDataButton";
            this.saveDataButton.Size = new System.Drawing.Size(96, 25);
            this.saveDataButton.TabIndex = 2;
            this.saveDataButton.Text = "ログデータ保存";
            this.saveDataButton.UseVisualStyleBackColor = true;
            this.saveDataButton.Click += new System.EventHandler(this.saveDataButton_Click);
            // 
            // readFileButton
            // 
            this.readFileButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.readFileButton.Location = new System.Drawing.Point(16, 18);
            this.readFileButton.Name = "readFileButton";
            this.readFileButton.Size = new System.Drawing.Size(96, 25);
            this.readFileButton.TabIndex = 6;
            this.readFileButton.Text = "ログファイル読込";
            this.readFileButton.UseVisualStyleBackColor = true;
            this.readFileButton.Click += new System.EventHandler(this.readFileButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.resetButton.Location = new System.Drawing.Point(16, 18);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(96, 25);
            this.resetButton.TabIndex = 6;
            this.resetButton.Text = "リセット";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "CSV ファイル (*.csv)|*.csv";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.ShowReadOnly = true;
            this.openFileDialog.Title = "ログファイルを開く";
            // 
            // grpOther
            // 
            this.grpOther.Controls.Add(this.modeChangeButton);
            this.grpOther.Controls.Add(this.resetButton);
            this.grpOther.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpOther.Location = new System.Drawing.Point(714, 8);
            this.grpOther.Name = "grpOther";
            this.grpOther.Size = new System.Drawing.Size(238, 52);
            this.grpOther.TabIndex = 7;
            this.grpOther.TabStop = false;
            this.grpOther.Text = "その他";
            // 
            // modeChangeButton
            // 
            this.modeChangeButton.Location = new System.Drawing.Point(126, 18);
            this.modeChangeButton.Name = "modeChangeButton";
            this.modeChangeButton.Size = new System.Drawing.Size(96, 25);
            this.modeChangeButton.TabIndex = 10;
            this.modeChangeButton.Text = "GRIDモードへ";
            this.modeChangeButton.UseVisualStyleBackColor = true;
            this.modeChangeButton.Click += new System.EventHandler(this.modeChangeButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.exitButton.Location = new System.Drawing.Point(1086, 527);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(112, 24);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "終了";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 559);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1210, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 9;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // pathSetButton
            // 
            this.pathSetButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pathSetButton.Location = new System.Drawing.Point(16, 18);
            this.pathSetButton.Name = "pathSetButton";
            this.pathSetButton.Size = new System.Drawing.Size(96, 25);
            this.pathSetButton.TabIndex = 0;
            this.pathSetButton.Text = "Path設定";
            this.pathSetButton.UseVisualStyleBackColor = true;
            this.pathSetButton.Click += new System.EventHandler(this.pathSetButton_Click);
            // 
            // grpAppFile
            // 
            this.grpAppFile.Controls.Add(this.pathSetButton);
            this.grpAppFile.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.grpAppFile.Location = new System.Drawing.Point(966, 8);
            this.grpAppFile.Name = "grpAppFile";
            this.grpAppFile.Size = new System.Drawing.Size(126, 52);
            this.grpAppFile.TabIndex = 11;
            this.grpAppFile.TabStop = false;
            this.grpAppFile.Text = "appファイル";
            // 
            // outsideTabControl
            // 
            this.outsideTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outsideTabControl.Font = new System.Drawing.Font("Meiryo", 9F);
            this.outsideTabControl.ItemSize = new System.Drawing.Size(140, 20);
            this.outsideTabControl.Location = new System.Drawing.Point(16, 74);
            this.outsideTabControl.Name = "outsideTabControl";
            this.outsideTabControl.SelectedIndex = 0;
            this.outsideTabControl.Size = new System.Drawing.Size(936, 435);
            this.outsideTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.outsideTabControl.TabIndex = 0;
            this.outsideTabControl.TabStop = false;
            this.outsideTabControl.Visible = false;
            // 
            // sendLabel
            // 
            this.sendLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sendLabel.AutoSize = true;
            this.sendLabel.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sendLabel.Location = new System.Drawing.Point(16, 532);
            this.sendLabel.Name = "sendLabel";
            this.sendLabel.Size = new System.Drawing.Size(59, 15);
            this.sendLabel.TabIndex = 12;
            this.sendLabel.Text = "データ送信";
            this.sendLabel.Visible = false;
            // 
            // sendTextBox
            // 
            this.sendTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sendTextBox.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sendTextBox.Location = new System.Drawing.Point(80, 527);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(150, 23);
            this.sendTextBox.TabIndex = 13;
            this.sendTextBox.Visible = false;
            // 
            // recEndButton
            // 
            this.recEndButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recEndButton.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.recEndButton.Location = new System.Drawing.Point(252, 525);
            this.recEndButton.Name = "recEndButton";
            this.recEndButton.Size = new System.Drawing.Size(96, 25);
            this.recEndButton.TabIndex = 14;
            this.recEndButton.Text = "受信終了";
            this.recEndButton.UseVisualStyleBackColor = true;
            this.recEndButton.Visible = false;
            this.recEndButton.Click += new System.EventHandler(this.recEndButton_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1210, 581);
            this.ControlBox = false;
            this.Controls.Add(this.recEndButton);
            this.Controls.Add(this.sendTextBox);
            this.Controls.Add(this.sendLabel);
            this.Controls.Add(this.grpAppFile);
            this.Controls.Add(this.outsideTabControl);
            this.Controls.Add(this.grpOther);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.grpIO);
            this.Controls.Add(this.grpConnection);
            this.Controls.Add(this.statusStrip);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "EV3VisualLogger";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.grpConnection.ResumeLayout(false);
            this.grpIO.ResumeLayout(false);
            this.grpIO.PerformLayout();
            this.grpOther.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.grpAppFile.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.GroupBox grpIO;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button saveDataButton;
        private System.Windows.Forms.Button readFileButton;
        private View.AutoSaveCheckBox autoSaveCheckBox;
        private System.Windows.Forms.Button resetButton;
        private View.PortNameComboBox portNameComboBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox grpOther;
        private View.OutsideTabControl outsideTabControl;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button modeChangeButton;
        private System.Windows.Forms.Button pathSetButton;
        private System.Windows.Forms.GroupBox grpAppFile;
        private System.Windows.Forms.Label sendLabel;
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.Button recEndButton;
    }
}

