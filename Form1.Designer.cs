namespace Andropeak
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblConnectInfo = new Label();
            comboAppPicker = new ComboBox();
            buttonListAPK = new Button();
            buttonPullAPK = new Button();
            lblErrorPull = new Label();
            btnDecompile = new Button();
            comboDecompile = new ComboBox();
            btnDecompileSelection = new Button();
            grpProxy = new GroupBox();
            txtBoxProxySet = new TextBox();
            label8 = new Label();
            lblUnsetMsg = new Label();
            btnProxyOff = new Button();
            btnProxyOn = new Button();
            label1 = new Label();
            grpPortReverse = new GroupBox();
            txtBoxPortRemote = new TextBox();
            txtBoxPortLocal = new TextBox();
            label7 = new Label();
            label4 = new Label();
            label2 = new Label();
            btnPortForwardOff = new Button();
            btnPortForwardOn = new Button();
            label6 = new Label();
            grpApkExtract = new GroupBox();
            grpDecompile = new GroupBox();
            btnAppPermissions = new Button();
            btnUnzipAPK = new Button();
            label5 = new Label();
            btnWorkingDirectory = new Button();
            lblWorkingDirectory = new Label();
            grpAdbShell = new GroupBox();
            btnAdbShell = new Button();
            groupBox1 = new GroupBox();
            btnWorkingDir = new Button();
            grpDeviceConnect = new GroupBox();
            lblConnectionStatus = new Label();
            grpDeviceScreenshot = new GroupBox();
            btnDevScreenshot = new Button();
            grpProxy.SuspendLayout();
            grpPortReverse.SuspendLayout();
            grpApkExtract.SuspendLayout();
            grpDecompile.SuspendLayout();
            grpAdbShell.SuspendLayout();
            groupBox1.SuspendLayout();
            grpDeviceConnect.SuspendLayout();
            grpDeviceScreenshot.SuspendLayout();
            SuspendLayout();
            // 
            // lblConnectInfo
            // 
            lblConnectInfo.AutoSize = true;
            lblConnectInfo.Location = new Point(7, 37);
            lblConnectInfo.Margin = new Padding(4, 0, 4, 0);
            lblConnectInfo.Name = "lblConnectInfo";
            lblConnectInfo.Size = new Size(9, 20);
            lblConnectInfo.TabIndex = 1;
            lblConnectInfo.Text = "\r\n";
            // 
            // comboAppPicker
            // 
            comboAppPicker.FormattingEnabled = true;
            comboAppPicker.Location = new Point(180, 40);
            comboAppPicker.Margin = new Padding(4, 3, 4, 3);
            comboAppPicker.Name = "comboAppPicker";
            comboAppPicker.Size = new Size(988, 28);
            comboAppPicker.TabIndex = 2;
            comboAppPicker.SelectedIndexChanged += comboAppPicker_SelectedIndexChanged;
            // 
            // buttonListAPK
            // 
            buttonListAPK.Location = new Point(7, 34);
            buttonListAPK.Margin = new Padding(4, 3, 4, 3);
            buttonListAPK.Name = "buttonListAPK";
            buttonListAPK.RightToLeft = RightToLeft.No;
            buttonListAPK.Size = new Size(167, 42);
            buttonListAPK.TabIndex = 3;
            buttonListAPK.Text = "List APKs";
            buttonListAPK.TextImageRelation = TextImageRelation.TextBeforeImage;
            buttonListAPK.UseVisualStyleBackColor = true;
            buttonListAPK.Click += buttonListAPK_Click;
            // 
            // buttonPullAPK
            // 
            buttonPullAPK.Location = new Point(6, 82);
            buttonPullAPK.Margin = new Padding(4, 3, 4, 3);
            buttonPullAPK.Name = "buttonPullAPK";
            buttonPullAPK.Size = new Size(167, 44);
            buttonPullAPK.TabIndex = 4;
            buttonPullAPK.Text = "Pull the APK";
            buttonPullAPK.UseVisualStyleBackColor = true;
            buttonPullAPK.Visible = false;
            buttonPullAPK.Click += buttonPullAPK_Click;
            // 
            // lblErrorPull
            // 
            lblErrorPull.AutoSize = true;
            lblErrorPull.Location = new Point(358, 315);
            lblErrorPull.Margin = new Padding(4, 0, 4, 0);
            lblErrorPull.Name = "lblErrorPull";
            lblErrorPull.Size = new Size(0, 20);
            lblErrorPull.TabIndex = 5;
            // 
            // btnDecompile
            // 
            btnDecompile.Location = new Point(8, 33);
            btnDecompile.Margin = new Padding(4, 3, 4, 3);
            btnDecompile.Name = "btnDecompile";
            btnDecompile.Size = new Size(167, 43);
            btnDecompile.TabIndex = 6;
            btnDecompile.Text = "List Local APKs";
            btnDecompile.UseVisualStyleBackColor = true;
            btnDecompile.Click += btnDecompile_Click;
            // 
            // comboDecompile
            // 
            comboDecompile.FormattingEnabled = true;
            comboDecompile.Location = new Point(180, 41);
            comboDecompile.Margin = new Padding(4, 3, 4, 3);
            comboDecompile.Name = "comboDecompile";
            comboDecompile.Size = new Size(883, 28);
            comboDecompile.TabIndex = 7;
            comboDecompile.SelectedIndexChanged += comboDecompile_SelectedIndexChanged;
            // 
            // btnDecompileSelection
            // 
            btnDecompileSelection.Location = new Point(8, 82);
            btnDecompileSelection.Margin = new Padding(4, 3, 4, 3);
            btnDecompileSelection.Name = "btnDecompileSelection";
            btnDecompileSelection.Size = new Size(167, 45);
            btnDecompileSelection.TabIndex = 8;
            btnDecompileSelection.Text = "Decompile Selection";
            btnDecompileSelection.UseVisualStyleBackColor = true;
            btnDecompileSelection.Visible = false;
            btnDecompileSelection.Click += btnDecompileSelection_Click;
            // 
            // grpProxy
            // 
            grpProxy.Controls.Add(txtBoxProxySet);
            grpProxy.Controls.Add(label8);
            grpProxy.Controls.Add(lblUnsetMsg);
            grpProxy.Controls.Add(btnProxyOff);
            grpProxy.Controls.Add(btnProxyOn);
            grpProxy.Controls.Add(label1);
            grpProxy.Location = new Point(358, 439);
            grpProxy.Margin = new Padding(4, 3, 4, 3);
            grpProxy.Name = "grpProxy";
            grpProxy.Padding = new Padding(4, 3, 4, 3);
            grpProxy.Size = new Size(416, 208);
            grpProxy.TabIndex = 10;
            grpProxy.TabStop = false;
            grpProxy.Text = "Device Proxy";
            grpProxy.Visible = false;
            // 
            // txtBoxProxySet
            // 
            txtBoxProxySet.Location = new Point(71, 45);
            txtBoxProxySet.Name = "txtBoxProxySet";
            txtBoxProxySet.Size = new Size(111, 27);
            txtBoxProxySet.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(18, 45);
            label8.Name = "label8";
            label8.Size = new Size(47, 20);
            label8.TabIndex = 4;
            label8.Text = "Local:";
            // 
            // lblUnsetMsg
            // 
            lblUnsetMsg.AutoSize = true;
            lblUnsetMsg.ForeColor = Color.Red;
            lblUnsetMsg.Location = new Point(262, 23);
            lblUnsetMsg.Margin = new Padding(4, 0, 4, 0);
            lblUnsetMsg.Name = "lblUnsetMsg";
            lblUnsetMsg.Size = new Size(141, 20);
            lblUnsetMsg.TabIndex = 3;
            lblUnsetMsg.Text = "Unset when finished";
            lblUnsetMsg.Visible = false;
            // 
            // btnProxyOff
            // 
            btnProxyOff.Location = new Point(180, 120);
            btnProxyOff.Margin = new Padding(4, 3, 4, 3);
            btnProxyOff.Name = "btnProxyOff";
            btnProxyOff.Size = new Size(164, 33);
            btnProxyOff.TabIndex = 2;
            btnProxyOff.Text = "Unset";
            btnProxyOff.UseVisualStyleBackColor = true;
            btnProxyOff.Visible = false;
            btnProxyOff.Click += btnProxyOff_Click;
            // 
            // btnProxyOn
            // 
            btnProxyOn.Location = new Point(8, 120);
            btnProxyOn.Margin = new Padding(4, 3, 4, 3);
            btnProxyOn.Name = "btnProxyOn";
            btnProxyOn.Size = new Size(164, 33);
            btnProxyOn.TabIndex = 1;
            btnProxyOn.Text = "Set";
            btnProxyOn.UseVisualStyleBackColor = true;
            btnProxyOn.Click += btnProxyOn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 174);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(397, 20);
            label1.TabIndex = 0;
            label1.Text = "Set the internal device proxy to 127.0.0.1:localport for Burp";
            // 
            // grpPortReverse
            // 
            grpPortReverse.Controls.Add(txtBoxPortRemote);
            grpPortReverse.Controls.Add(txtBoxPortLocal);
            grpPortReverse.Controls.Add(label7);
            grpPortReverse.Controls.Add(label4);
            grpPortReverse.Controls.Add(label2);
            grpPortReverse.Controls.Add(btnPortForwardOff);
            grpPortReverse.Controls.Add(btnPortForwardOn);
            grpPortReverse.Location = new Point(12, 439);
            grpPortReverse.Margin = new Padding(4, 3, 4, 3);
            grpPortReverse.Name = "grpPortReverse";
            grpPortReverse.Padding = new Padding(4, 3, 4, 3);
            grpPortReverse.Size = new Size(338, 208);
            grpPortReverse.TabIndex = 11;
            grpPortReverse.TabStop = false;
            grpPortReverse.Text = "Device Reverse Socket";
            grpPortReverse.Visible = false;
            // 
            // txtBoxPortRemote
            // 
            txtBoxPortRemote.Location = new Point(215, 45);
            txtBoxPortRemote.Name = "txtBoxPortRemote";
            txtBoxPortRemote.Size = new Size(87, 27);
            txtBoxPortRemote.TabIndex = 6;
            // 
            // txtBoxPortLocal
            // 
            txtBoxPortLocal.Location = new Point(58, 45);
            txtBoxPortLocal.Name = "txtBoxPortLocal";
            txtBoxPortLocal.Size = new Size(87, 27);
            txtBoxPortLocal.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(151, 45);
            label7.Name = "label7";
            label7.Size = new Size(64, 20);
            label7.TabIndex = 4;
            label7.Text = "Remote:";
            label7.Click += label7_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 45);
            label4.Name = "label4";
            label4.Size = new Size(47, 20);
            label4.TabIndex = 3;
            label4.Text = "Local:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 174);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(316, 20);
            label2.TabIndex = 2;
            label2.Text = "Set \"adb reverse tcp:local tcp:remote\" or unset";
            // 
            // btnPortForwardOff
            // 
            btnPortForwardOff.Location = new Point(159, 120);
            btnPortForwardOff.Margin = new Padding(4, 3, 4, 3);
            btnPortForwardOff.Name = "btnPortForwardOff";
            btnPortForwardOff.Size = new Size(143, 33);
            btnPortForwardOff.TabIndex = 1;
            btnPortForwardOff.Text = "Unset";
            btnPortForwardOff.UseVisualStyleBackColor = true;
            btnPortForwardOff.Visible = false;
            btnPortForwardOff.Click += btnPortForwardOff_Click;
            // 
            // btnPortForwardOn
            // 
            btnPortForwardOn.Location = new Point(16, 120);
            btnPortForwardOn.Margin = new Padding(4, 3, 4, 3);
            btnPortForwardOn.Name = "btnPortForwardOn";
            btnPortForwardOn.Size = new Size(137, 33);
            btnPortForwardOn.TabIndex = 0;
            btnPortForwardOn.Text = "Set";
            btnPortForwardOn.UseVisualStyleBackColor = true;
            btnPortForwardOn.Click += btnPortForwardOn_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(180, 17);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(80, 20);
            label6.TabIndex = 14;
            label6.Text = "Select APK";
            // 
            // grpApkExtract
            // 
            grpApkExtract.Controls.Add(buttonListAPK);
            grpApkExtract.Controls.Add(label6);
            grpApkExtract.Controls.Add(buttonPullAPK);
            grpApkExtract.Controls.Add(comboAppPicker);
            grpApkExtract.Location = new Point(12, 139);
            grpApkExtract.Margin = new Padding(4, 3, 4, 3);
            grpApkExtract.Name = "grpApkExtract";
            grpApkExtract.Padding = new Padding(4, 3, 4, 3);
            grpApkExtract.Size = new Size(1238, 135);
            grpApkExtract.TabIndex = 15;
            grpApkExtract.TabStop = false;
            grpApkExtract.Text = "Extract APK from Device";
            grpApkExtract.Visible = false;
            // 
            // grpDecompile
            // 
            grpDecompile.Controls.Add(btnAppPermissions);
            grpDecompile.Controls.Add(btnUnzipAPK);
            grpDecompile.Controls.Add(label5);
            grpDecompile.Controls.Add(btnDecompile);
            grpDecompile.Controls.Add(btnDecompileSelection);
            grpDecompile.Controls.Add(comboDecompile);
            grpDecompile.Location = new Point(12, 280);
            grpDecompile.Margin = new Padding(4, 3, 4, 3);
            grpDecompile.Name = "grpDecompile";
            grpDecompile.Padding = new Padding(4, 3, 4, 3);
            grpDecompile.Size = new Size(1238, 141);
            grpDecompile.TabIndex = 16;
            grpDecompile.TabStop = false;
            grpDecompile.Text = "Local APK Functions";
            grpDecompile.Visible = false;
            // 
            // btnAppPermissions
            // 
            btnAppPermissions.Location = new Point(182, 82);
            btnAppPermissions.Name = "btnAppPermissions";
            btnAppPermissions.Size = new Size(132, 45);
            btnAppPermissions.TabIndex = 11;
            btnAppPermissions.Text = "App Permissions";
            btnAppPermissions.UseVisualStyleBackColor = true;
            btnAppPermissions.Click += btnAppPermissions_Click;
            // 
            // btnUnzipAPK
            // 
            btnUnzipAPK.Location = new Point(1067, 82);
            btnUnzipAPK.Name = "btnUnzipAPK";
            btnUnzipAPK.Size = new Size(164, 45);
            btnUnzipAPK.TabIndex = 10;
            btnUnzipAPK.Text = "Unzip APK";
            btnUnzipAPK.UseVisualStyleBackColor = true;
            btnUnzipAPK.Visible = false;
            btnUnzipAPK.Click += btnUnzipAPK_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(180, 18);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(134, 20);
            label5.TabIndex = 9;
            label5.Text = "Select Local Folder";
            // 
            // btnWorkingDirectory
            // 
            btnWorkingDirectory.Location = new Point(159, 33);
            btnWorkingDirectory.Margin = new Padding(4, 3, 4, 3);
            btnWorkingDirectory.Name = "btnWorkingDirectory";
            btnWorkingDirectory.Size = new Size(143, 29);
            btnWorkingDirectory.TabIndex = 11;
            btnWorkingDirectory.Text = "Open Folder";
            btnWorkingDirectory.UseVisualStyleBackColor = true;
            btnWorkingDirectory.Visible = false;
            btnWorkingDirectory.Click += btnWorkingDirectory_Click;
            // 
            // lblWorkingDirectory
            // 
            lblWorkingDirectory.AutoSize = true;
            lblWorkingDirectory.Location = new Point(7, 82);
            lblWorkingDirectory.Margin = new Padding(4, 0, 4, 0);
            lblWorkingDirectory.Name = "lblWorkingDirectory";
            lblWorkingDirectory.Size = new Size(188, 20);
            lblWorkingDirectory.TabIndex = 10;
            lblWorkingDirectory.Text = "Current Working Directory: \r\n";
            // 
            // grpAdbShell
            // 
            grpAdbShell.Controls.Add(btnAdbShell);
            grpAdbShell.Location = new Point(781, 439);
            grpAdbShell.Name = "grpAdbShell";
            grpAdbShell.Size = new Size(144, 208);
            grpAdbShell.TabIndex = 17;
            grpAdbShell.TabStop = false;
            grpAdbShell.Text = "ADB Shell";
            grpAdbShell.Visible = false;
            // 
            // btnAdbShell
            // 
            btnAdbShell.Location = new Point(22, 120);
            btnAdbShell.Name = "btnAdbShell";
            btnAdbShell.Size = new Size(94, 33);
            btnAdbShell.TabIndex = 0;
            btnAdbShell.Text = "Shell";
            btnAdbShell.UseVisualStyleBackColor = true;
            btnAdbShell.Click += btnAdbShell_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnWorkingDirectory);
            groupBox1.Controls.Add(btnWorkingDir);
            groupBox1.Controls.Add(lblWorkingDirectory);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1238, 121);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Working Directory";
            // 
            // btnWorkingDir
            // 
            btnWorkingDir.Location = new Point(10, 33);
            btnWorkingDir.Name = "btnWorkingDir";
            btnWorkingDir.Size = new Size(135, 29);
            btnWorkingDir.TabIndex = 0;
            btnWorkingDir.Text = "Choose Folder";
            btnWorkingDir.UseVisualStyleBackColor = true;
            btnWorkingDir.Click += btnWorkingDir_Click;
            // 
            // grpDeviceConnect
            // 
            grpDeviceConnect.Controls.Add(lblConnectionStatus);
            grpDeviceConnect.Controls.Add(lblConnectInfo);
            grpDeviceConnect.Location = new Point(1256, 12);
            grpDeviceConnect.Name = "grpDeviceConnect";
            grpDeviceConnect.Size = new Size(494, 121);
            grpDeviceConnect.TabIndex = 19;
            grpDeviceConnect.TabStop = false;
            grpDeviceConnect.Text = "Device Connect";
            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.Location = new Point(6, 78);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new Size(297, 40);
            lblConnectionStatus.TabIndex = 2;
            lblConnectionStatus.Text = "Auto device connection will show here.\r\nAn error will show the USB is disconnected. ";
            // 
            // grpDeviceScreenshot
            // 
            grpDeviceScreenshot.Controls.Add(btnDevScreenshot);
            grpDeviceScreenshot.Location = new Point(950, 439);
            grpDeviceScreenshot.Name = "grpDeviceScreenshot";
            grpDeviceScreenshot.Size = new Size(133, 208);
            grpDeviceScreenshot.TabIndex = 20;
            grpDeviceScreenshot.TabStop = false;
            grpDeviceScreenshot.Text = "Device Screenshot";
            grpDeviceScreenshot.Visible = false;
            // 
            // btnDevScreenshot
            // 
            btnDevScreenshot.Location = new Point(17, 120);
            btnDevScreenshot.Name = "btnDevScreenshot";
            btnDevScreenshot.Size = new Size(94, 29);
            btnDevScreenshot.TabIndex = 0;
            btnDevScreenshot.Text = "Snap";
            btnDevScreenshot.UseVisualStyleBackColor = true;
            btnDevScreenshot.Click += btnDevScreenshot_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1783, 664);
            Controls.Add(grpDeviceScreenshot);
            Controls.Add(grpDeviceConnect);
            Controls.Add(groupBox1);
            Controls.Add(grpAdbShell);
            Controls.Add(grpDecompile);
            Controls.Add(grpApkExtract);
            Controls.Add(grpPortReverse);
            Controls.Add(grpProxy);
            Controls.Add(lblErrorPull);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "AndroPeak";
            Load += Form1_Load;
            grpProxy.ResumeLayout(false);
            grpProxy.PerformLayout();
            grpPortReverse.ResumeLayout(false);
            grpPortReverse.PerformLayout();
            grpApkExtract.ResumeLayout(false);
            grpApkExtract.PerformLayout();
            grpDecompile.ResumeLayout(false);
            grpDecompile.PerformLayout();
            grpAdbShell.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            grpDeviceConnect.ResumeLayout(false);
            grpDeviceConnect.PerformLayout();
            grpDeviceScreenshot.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblConnectInfo;
        private ComboBox comboAppPicker;
        private Button buttonListAPK;
        private Button buttonPullAPK;
        private Label lblErrorPull;
        private Button btnDecompile;
        private ComboBox comboDecompile;
        private Button btnDecompileSelection;
        private GroupBox grpProxy;
        private Button btnProxyOn;
        private Label label1;
        private Button btnProxyOff;
        private GroupBox grpPortReverse;
        private Label label2;
        private Button btnPortForwardOff;
        private Button btnPortForwardOn;
        private Label lblUnsetMsg;
        private Label label6;
        private GroupBox grpApkExtract;
        private GroupBox grpDecompile;
        private Label label5;
        private Label lblWorkingDirectory;
        private Button btnWorkingDirectory;
        private GroupBox grpAdbShell;
        private Button btnAdbShell;
        private TextBox txtBoxPortRemote;
        private TextBox txtBoxPortLocal;
        private Label label7;
        private Label label4;
        private TextBox txtBoxProxySet;
        private Label label8;
        private GroupBox groupBox1;
        private Button btnWorkingDir;
        private GroupBox grpDeviceConnect;
        private Button btnUnzipAPK;
        private Label lblConnectionStatus;
        private Button btnAppPermissions;
        private GroupBox grpDeviceScreenshot;
        private Button btnDevScreenshot;
    }
}