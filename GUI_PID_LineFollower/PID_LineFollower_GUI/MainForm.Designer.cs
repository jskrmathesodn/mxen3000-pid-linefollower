namespace PIDLineFollowerGUI
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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serial = new System.IO.Ports.SerialPort(this.components);
            this.ioTimer = new System.Windows.Forms.Timer(this.components);
            this.pidGroup = new System.Windows.Forms.GroupBox();
            this.lblKP = new System.Windows.Forms.Label();
            this.kPBox = new System.Windows.Forms.NumericUpDown();
            this.lblKI = new System.Windows.Forms.Label();
            this.kIBox = new System.Windows.Forms.NumericUpDown();
            this.lblKD = new System.Windows.Forms.Label();
            this.kDBox = new System.Windows.Forms.NumericUpDown();
            this.lblBasePWM = new System.Windows.Forms.Label();
            this.basePWMBox = new System.Windows.Forms.NumericUpDown();
            this.lblIntegralClamp = new System.Windows.Forms.Label();
            this.integralClampBox = new System.Windows.Forms.NumericUpDown();
            this.lblSensorMin = new System.Windows.Forms.Label();
            this.sensorMinBox = new System.Windows.Forms.NumericUpDown();
            this.lblSensorMax = new System.Windows.Forms.Label();
            this.sensorMaxBox = new System.Windows.Forms.NumericUpDown();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.lblA6 = new System.Windows.Forms.Label();
            this.input1LiveBox = new System.Windows.Forms.TextBox();
            this.lblA7 = new System.Windows.Forms.Label();
            this.input2LiveBox = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.errorLiveBox = new System.Windows.Forms.TextBox();
            this.lblEffort = new System.Windows.Forms.Label();
            this.effortLiveBox = new System.Windows.Forms.TextBox();
            this.lblLeftOut = new System.Windows.Forms.Label();
            this.leftOutLiveBox = new System.Windows.Forms.TextBox();
            this.lblRightOut = new System.Windows.Forms.Label();
            this.rightOutLiveBox = new System.Windows.Forms.TextBox();
            this.manualGroup = new System.Windows.Forms.GroupBox();
            this.lblOut1 = new System.Windows.Forms.Label();
            this.OutputBox1 = new System.Windows.Forms.NumericUpDown();
            this.Send1 = new System.Windows.Forms.Button();
            this.lblOut2 = new System.Windows.Forms.Label();
            this.OutputBox2 = new System.Windows.Forms.NumericUpDown();
            this.Send2 = new System.Windows.Forms.Button();
            this.Get1 = new System.Windows.Forms.Button();
            this.InputBox1 = new System.Windows.Forms.TextBox();
            this.Get2 = new System.Windows.Forms.Button();
            this.InputBox2 = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.pidGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kIBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kDBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.basePWMBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integralClampBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorMinBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorMaxBox)).BeginInit();
            this.manualGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // serial
            // 
            this.serial.PortName = "COM6";
            // 
            // ioTimer
            // 
            this.ioTimer.Enabled = true;
            this.ioTimer.Interval = 10;
            this.ioTimer.Tick += new System.EventHandler(this.ioTimer_Tick);
            // 
            // pidGroup
            // 
            this.pidGroup.Controls.Add(this.lblKP);
            this.pidGroup.Controls.Add(this.kPBox);
            this.pidGroup.Controls.Add(this.lblKI);
            this.pidGroup.Controls.Add(this.kIBox);
            this.pidGroup.Controls.Add(this.lblKD);
            this.pidGroup.Controls.Add(this.kDBox);
            this.pidGroup.Controls.Add(this.lblBasePWM);
            this.pidGroup.Controls.Add(this.basePWMBox);
            this.pidGroup.Controls.Add(this.lblIntegralClamp);
            this.pidGroup.Controls.Add(this.integralClampBox);
            this.pidGroup.Controls.Add(this.lblSensorMin);
            this.pidGroup.Controls.Add(this.sensorMinBox);
            this.pidGroup.Controls.Add(this.lblSensorMax);
            this.pidGroup.Controls.Add(this.sensorMaxBox);
            this.pidGroup.Controls.Add(this.startButton);
            this.pidGroup.Controls.Add(this.stopButton);
            this.pidGroup.Controls.Add(this.lblA6);
            this.pidGroup.Controls.Add(this.input1LiveBox);
            this.pidGroup.Controls.Add(this.lblA7);
            this.pidGroup.Controls.Add(this.input2LiveBox);
            this.pidGroup.Controls.Add(this.lblError);
            this.pidGroup.Controls.Add(this.errorLiveBox);
            this.pidGroup.Controls.Add(this.lblEffort);
            this.pidGroup.Controls.Add(this.effortLiveBox);
            this.pidGroup.Controls.Add(this.lblLeftOut);
            this.pidGroup.Controls.Add(this.leftOutLiveBox);
            this.pidGroup.Controls.Add(this.lblRightOut);
            this.pidGroup.Controls.Add(this.rightOutLiveBox);
            this.pidGroup.Location = new System.Drawing.Point(24, 23);
            this.pidGroup.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pidGroup.Name = "pidGroup";
            this.pidGroup.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pidGroup.Size = new System.Drawing.Size(1300, 558);
            this.pidGroup.TabIndex = 0;
            this.pidGroup.TabStop = false;
            this.pidGroup.Text = "Autonomous PID control (Milestone 4)";
            // 
            // lblKP
            // 
            this.lblKP.Location = new System.Drawing.Point(40, 52);
            this.lblKP.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblKP.Name = "lblKP";
            this.lblKP.Size = new System.Drawing.Size(100, 44);
            this.lblKP.TabIndex = 0;
            this.lblKP.Text = "kP:";
            // 
            // kPBox
            // 
            this.kPBox.DecimalPlaces = 3;
            this.kPBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.kPBox.Location = new System.Drawing.Point(150, 48);
            this.kPBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.kPBox.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.kPBox.Name = "kPBox";
            this.kPBox.Size = new System.Drawing.Size(160, 31);
            this.kPBox.TabIndex = 1;
            this.kPBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // lblKI
            // 
            this.lblKI.Location = new System.Drawing.Point(360, 52);
            this.lblKI.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblKI.Name = "lblKI";
            this.lblKI.Size = new System.Drawing.Size(100, 44);
            this.lblKI.TabIndex = 2;
            this.lblKI.Text = "kI:";
            // 
            // kIBox
            // 
            this.kIBox.DecimalPlaces = 3;
            this.kIBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.kIBox.Location = new System.Drawing.Point(470, 48);
            this.kIBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.kIBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.kIBox.Name = "kIBox";
            this.kIBox.Size = new System.Drawing.Size(160, 31);
            this.kIBox.TabIndex = 3;
            // 
            // lblKD
            // 
            this.lblKD.Location = new System.Drawing.Point(680, 52);
            this.lblKD.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblKD.Name = "lblKD";
            this.lblKD.Size = new System.Drawing.Size(100, 44);
            this.lblKD.TabIndex = 4;
            this.lblKD.Text = "kD:";
            // 
            // kDBox
            // 
            this.kDBox.DecimalPlaces = 3;
            this.kDBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.kDBox.Location = new System.Drawing.Point(790, 48);
            this.kDBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.kDBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.kDBox.Name = "kDBox";
            this.kDBox.Size = new System.Drawing.Size(160, 31);
            this.kDBox.TabIndex = 5;
            // 
            // lblBasePWM
            // 
            this.lblBasePWM.Location = new System.Drawing.Point(40, 119);
            this.lblBasePWM.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblBasePWM.Name = "lblBasePWM";
            this.lblBasePWM.Size = new System.Drawing.Size(280, 44);
            this.lblBasePWM.TabIndex = 6;
            this.lblBasePWM.Text = "Base PWM (0-255):";
            // 
            // basePWMBox
            // 
            this.basePWMBox.Location = new System.Drawing.Point(330, 115);
            this.basePWMBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.basePWMBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.basePWMBox.Name = "basePWMBox";
            this.basePWMBox.Size = new System.Drawing.Size(160, 31);
            this.basePWMBox.TabIndex = 7;
            this.basePWMBox.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // lblIntegralClamp
            // 
            this.lblIntegralClamp.Location = new System.Drawing.Point(560, 119);
            this.lblIntegralClamp.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblIntegralClamp.Name = "lblIntegralClamp";
            this.lblIntegralClamp.Size = new System.Drawing.Size(240, 44);
            this.lblIntegralClamp.TabIndex = 8;
            this.lblIntegralClamp.Text = "Integral clamp:";
            // 
            // integralClampBox
            // 
            this.integralClampBox.DecimalPlaces = 1;
            this.integralClampBox.Location = new System.Drawing.Point(810, 115);
            this.integralClampBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.integralClampBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.integralClampBox.Name = "integralClampBox";
            this.integralClampBox.Size = new System.Drawing.Size(160, 31);
            this.integralClampBox.TabIndex = 9;
            this.integralClampBox.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblSensorMin
            // 
            this.lblSensorMin.Location = new System.Drawing.Point(40, 187);
            this.lblSensorMin.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSensorMin.Name = "lblSensorMin";
            this.lblSensorMin.Size = new System.Drawing.Size(300, 44);
            this.lblSensorMin.TabIndex = 10;
            this.lblSensorMin.Text = "Sensor min (raw 0-255):";
            // 
            // sensorMinBox
            // 
            this.sensorMinBox.Location = new System.Drawing.Point(350, 183);
            this.sensorMinBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.sensorMinBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.sensorMinBox.Name = "sensorMinBox";
            this.sensorMinBox.Size = new System.Drawing.Size(160, 31);
            this.sensorMinBox.TabIndex = 11;
            // 
            // lblSensorMax
            // 
            this.lblSensorMax.Location = new System.Drawing.Point(560, 187);
            this.lblSensorMax.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSensorMax.Name = "lblSensorMax";
            this.lblSensorMax.Size = new System.Drawing.Size(300, 44);
            this.lblSensorMax.TabIndex = 12;
            this.lblSensorMax.Text = "Sensor max (raw 0-255):";
            // 
            // sensorMaxBox
            // 
            this.sensorMaxBox.Location = new System.Drawing.Point(870, 183);
            this.sensorMaxBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.sensorMaxBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.sensorMaxBox.Name = "sensorMaxBox";
            this.sensorMaxBox.Size = new System.Drawing.Size(160, 31);
            this.sensorMaxBox.TabIndex = 13;
            this.sensorMaxBox.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(40, 265);
            this.startButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(300, 77);
            this.startButton.TabIndex = 14;
            this.startButton.Text = "Start (S)";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(370, 265);
            this.stopButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(300, 77);
            this.stopButton.TabIndex = 15;
            this.stopButton.Text = "Stop (X)";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // lblA6
            // 
            this.lblA6.Location = new System.Drawing.Point(40, 379);
            this.lblA6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblA6.Name = "lblA6";
            this.lblA6.Size = new System.Drawing.Size(80, 44);
            this.lblA6.TabIndex = 16;
            this.lblA6.Text = "A6:";
            // 
            // input1LiveBox
            // 
            this.input1LiveBox.Location = new System.Drawing.Point(120, 375);
            this.input1LiveBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.input1LiveBox.Name = "input1LiveBox";
            this.input1LiveBox.ReadOnly = true;
            this.input1LiveBox.Size = new System.Drawing.Size(136, 31);
            this.input1LiveBox.TabIndex = 17;
            this.input1LiveBox.Text = "0";
            // 
            // lblA7
            // 
            this.lblA7.Location = new System.Drawing.Point(300, 379);
            this.lblA7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblA7.Name = "lblA7";
            this.lblA7.Size = new System.Drawing.Size(80, 44);
            this.lblA7.TabIndex = 18;
            this.lblA7.Text = "A7:";
            // 
            // input2LiveBox
            // 
            this.input2LiveBox.Location = new System.Drawing.Point(380, 375);
            this.input2LiveBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.input2LiveBox.Name = "input2LiveBox";
            this.input2LiveBox.ReadOnly = true;
            this.input2LiveBox.Size = new System.Drawing.Size(136, 31);
            this.input2LiveBox.TabIndex = 19;
            this.input2LiveBox.Text = "0";
            // 
            // lblError
            // 
            this.lblError.Location = new System.Drawing.Point(560, 379);
            this.lblError.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(100, 44);
            this.lblError.TabIndex = 20;
            this.lblError.Text = "Error:";
            // 
            // errorLiveBox
            // 
            this.errorLiveBox.Location = new System.Drawing.Point(660, 375);
            this.errorLiveBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.errorLiveBox.Name = "errorLiveBox";
            this.errorLiveBox.ReadOnly = true;
            this.errorLiveBox.Size = new System.Drawing.Size(136, 31);
            this.errorLiveBox.TabIndex = 21;
            this.errorLiveBox.Text = "0";
            // 
            // lblEffort
            // 
            this.lblEffort.Location = new System.Drawing.Point(840, 379);
            this.lblEffort.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblEffort.Name = "lblEffort";
            this.lblEffort.Size = new System.Drawing.Size(120, 44);
            this.lblEffort.TabIndex = 22;
            this.lblEffort.Text = "Effort:";
            // 
            // effortLiveBox
            // 
            this.effortLiveBox.Location = new System.Drawing.Point(960, 375);
            this.effortLiveBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.effortLiveBox.Name = "effortLiveBox";
            this.effortLiveBox.ReadOnly = true;
            this.effortLiveBox.Size = new System.Drawing.Size(136, 31);
            this.effortLiveBox.TabIndex = 23;
            this.effortLiveBox.Text = "0";
            // 
            // lblLeftOut
            // 
            this.lblLeftOut.Location = new System.Drawing.Point(40, 456);
            this.lblLeftOut.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblLeftOut.Name = "lblLeftOut";
            this.lblLeftOut.Size = new System.Drawing.Size(140, 44);
            this.lblLeftOut.TabIndex = 24;
            this.lblLeftOut.Text = "Left out:";
            // 
            // leftOutLiveBox
            // 
            this.leftOutLiveBox.Location = new System.Drawing.Point(180, 452);
            this.leftOutLiveBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.leftOutLiveBox.Name = "leftOutLiveBox";
            this.leftOutLiveBox.ReadOnly = true;
            this.leftOutLiveBox.Size = new System.Drawing.Size(136, 31);
            this.leftOutLiveBox.TabIndex = 25;
            this.leftOutLiveBox.Text = "0";
            // 
            // lblRightOut
            // 
            this.lblRightOut.Location = new System.Drawing.Point(360, 456);
            this.lblRightOut.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRightOut.Name = "lblRightOut";
            this.lblRightOut.Size = new System.Drawing.Size(160, 44);
            this.lblRightOut.TabIndex = 26;
            this.lblRightOut.Text = "Right out:";
            // 
            // rightOutLiveBox
            // 
            this.rightOutLiveBox.Location = new System.Drawing.Point(520, 452);
            this.rightOutLiveBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.rightOutLiveBox.Name = "rightOutLiveBox";
            this.rightOutLiveBox.ReadOnly = true;
            this.rightOutLiveBox.Size = new System.Drawing.Size(136, 31);
            this.rightOutLiveBox.TabIndex = 27;
            this.rightOutLiveBox.Text = "0";
            // 
            // manualGroup
            // 
            this.manualGroup.Controls.Add(this.lblOut1);
            this.manualGroup.Controls.Add(this.OutputBox1);
            this.manualGroup.Controls.Add(this.Send1);
            this.manualGroup.Controls.Add(this.lblOut2);
            this.manualGroup.Controls.Add(this.OutputBox2);
            this.manualGroup.Controls.Add(this.Send2);
            this.manualGroup.Controls.Add(this.Get1);
            this.manualGroup.Controls.Add(this.InputBox1);
            this.manualGroup.Controls.Add(this.Get2);
            this.manualGroup.Controls.Add(this.InputBox2);
            this.manualGroup.Controls.Add(this.lblStatus);
            this.manualGroup.Controls.Add(this.statusBox);
            this.manualGroup.Location = new System.Drawing.Point(24, 600);
            this.manualGroup.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.manualGroup.Name = "manualGroup";
            this.manualGroup.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.manualGroup.Size = new System.Drawing.Size(1300, 490);
            this.manualGroup.TabIndex = 1;
            this.manualGroup.TabStop = false;
            this.manualGroup.Text = "Manual / bench (debug)";
            // 
            // lblOut1
            // 
            this.lblOut1.Location = new System.Drawing.Point(40, 58);
            this.lblOut1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblOut1.Name = "lblOut1";
            this.lblOut1.Size = new System.Drawing.Size(440, 44);
            this.lblOut1.TabIndex = 0;
            this.lblOut1.Text = "Output 1 (DAC code 0-255):";
            // 
            // OutputBox1
            // 
            this.OutputBox1.Location = new System.Drawing.Point(500, 54);
            this.OutputBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.OutputBox1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.OutputBox1.Name = "OutputBox1";
            this.OutputBox1.Size = new System.Drawing.Size(160, 31);
            this.OutputBox1.TabIndex = 1;
            // 
            // Send1
            // 
            this.Send1.Location = new System.Drawing.Point(700, 48);
            this.Send1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Send1.Name = "Send1";
            this.Send1.Size = new System.Drawing.Size(260, 62);
            this.Send1.TabIndex = 2;
            this.Send1.Text = "Send Output 1";
            this.Send1.UseVisualStyleBackColor = true;
            this.Send1.Click += new System.EventHandler(this.Send1_Click);
            // 
            // lblOut2
            // 
            this.lblOut2.Location = new System.Drawing.Point(40, 135);
            this.lblOut2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblOut2.Name = "lblOut2";
            this.lblOut2.Size = new System.Drawing.Size(440, 44);
            this.lblOut2.TabIndex = 3;
            this.lblOut2.Text = "Output 2 (DAC code 0-255):";
            // 
            // OutputBox2
            // 
            this.OutputBox2.Location = new System.Drawing.Point(500, 131);
            this.OutputBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.OutputBox2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.OutputBox2.Name = "OutputBox2";
            this.OutputBox2.Size = new System.Drawing.Size(160, 31);
            this.OutputBox2.TabIndex = 4;
            // 
            // Send2
            // 
            this.Send2.Location = new System.Drawing.Point(700, 125);
            this.Send2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Send2.Name = "Send2";
            this.Send2.Size = new System.Drawing.Size(260, 62);
            this.Send2.TabIndex = 5;
            this.Send2.Text = "Send Output 2";
            this.Send2.UseVisualStyleBackColor = true;
            this.Send2.Click += new System.EventHandler(this.Send2_Click);
            // 
            // Get1
            // 
            this.Get1.Location = new System.Drawing.Point(40, 221);
            this.Get1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Get1.Name = "Get1";
            this.Get1.Size = new System.Drawing.Size(300, 62);
            this.Get1.TabIndex = 6;
            this.Get1.Text = "Get Input 1";
            this.Get1.UseVisualStyleBackColor = true;
            this.Get1.Click += new System.EventHandler(this.Get1_Click);
            // 
            // InputBox1
            // 
            this.InputBox1.Location = new System.Drawing.Point(360, 227);
            this.InputBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.InputBox1.Name = "InputBox1";
            this.InputBox1.ReadOnly = true;
            this.InputBox1.Size = new System.Drawing.Size(296, 31);
            this.InputBox1.TabIndex = 7;
            this.InputBox1.Text = "0";
            // 
            // Get2
            // 
            this.Get2.Location = new System.Drawing.Point(40, 308);
            this.Get2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Get2.Name = "Get2";
            this.Get2.Size = new System.Drawing.Size(300, 62);
            this.Get2.TabIndex = 8;
            this.Get2.Text = "Get Input 2";
            this.Get2.UseVisualStyleBackColor = true;
            this.Get2.Click += new System.EventHandler(this.Get2_Click);
            // 
            // InputBox2
            // 
            this.InputBox2.Location = new System.Drawing.Point(360, 313);
            this.InputBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.InputBox2.Name = "InputBox2";
            this.InputBox2.ReadOnly = true;
            this.InputBox2.Size = new System.Drawing.Size(296, 31);
            this.InputBox2.TabIndex = 9;
            this.InputBox2.Text = "0";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(40, 394);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(120, 44);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status:";
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(170, 390);
            this.statusBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(1076, 31);
            this.statusBox.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 1154);
            this.Controls.Add(this.pidGroup);
            this.Controls.Add(this.manualGroup);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainForm";
            this.Text = "MXEN3000 - PID Line Follower (Milestone 4)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pidGroup.ResumeLayout(false);
            this.pidGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kIBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kDBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.basePWMBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integralClampBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorMinBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorMaxBox)).EndInit();
            this.manualGroup.ResumeLayout(false);
            this.manualGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.IO.Ports.SerialPort serial;
        private System.Windows.Forms.Timer ioTimer;

        private System.Windows.Forms.GroupBox pidGroup;
        private System.Windows.Forms.Label lblKP;
        private System.Windows.Forms.NumericUpDown kPBox;
        private System.Windows.Forms.Label lblKI;
        private System.Windows.Forms.NumericUpDown kIBox;
        private System.Windows.Forms.Label lblKD;
        private System.Windows.Forms.NumericUpDown kDBox;
        private System.Windows.Forms.Label lblBasePWM;
        private System.Windows.Forms.NumericUpDown basePWMBox;
        private System.Windows.Forms.Label lblIntegralClamp;
        private System.Windows.Forms.NumericUpDown integralClampBox;
        private System.Windows.Forms.Label lblSensorMin;
        private System.Windows.Forms.NumericUpDown sensorMinBox;
        private System.Windows.Forms.Label lblSensorMax;
        private System.Windows.Forms.NumericUpDown sensorMaxBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label lblA6;
        private System.Windows.Forms.TextBox input1LiveBox;
        private System.Windows.Forms.Label lblA7;
        private System.Windows.Forms.TextBox input2LiveBox;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TextBox errorLiveBox;
        private System.Windows.Forms.Label lblEffort;
        private System.Windows.Forms.TextBox effortLiveBox;
        private System.Windows.Forms.Label lblLeftOut;
        private System.Windows.Forms.TextBox leftOutLiveBox;
        private System.Windows.Forms.Label lblRightOut;
        private System.Windows.Forms.TextBox rightOutLiveBox;

        private System.Windows.Forms.GroupBox manualGroup;
        private System.Windows.Forms.Label lblOut1;
        private System.Windows.Forms.NumericUpDown OutputBox1;
        private System.Windows.Forms.Button Send1;
        private System.Windows.Forms.Label lblOut2;
        private System.Windows.Forms.NumericUpDown OutputBox2;
        private System.Windows.Forms.Button Send2;
        private System.Windows.Forms.Button Get1;
        private System.Windows.Forms.TextBox InputBox1;
        private System.Windows.Forms.Button Get2;
        private System.Windows.Forms.TextBox InputBox2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox statusBox;
    }
}
