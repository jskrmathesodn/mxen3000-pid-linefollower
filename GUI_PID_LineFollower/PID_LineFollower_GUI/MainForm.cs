using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PIDLineFollowerGUI
{
    public partial class MainForm : Form
    {
        byte[] outgoing = new byte[4];
        byte[] incoming = new byte[4];

        const byte START = 255;
        const byte ZERO = 0;

        const byte INPUT1 = 0;
        const byte INPUT2 = 1;
        const byte OUTPUT1 = 2;
        const byte OUTPUT2 = 3;

        const decimal VMIN = -15m;
        const decimal VMAX = 15m;

        enum LoopState { Idle, AwaitingInput1, AwaitingInput2 }
        LoopState state = LoopState.Idle;
        bool running = false;

        int input1Raw = 0;
        int input2Raw = 0;

        double errorSum = 0.0;
        double errorLast = 0.0;
        bool haveLastError = false;

        double filteredError = 0.0;
        bool haveFilteredError = false;
        const double D_FILTER_ALPHA = 0.35;
        readonly Stopwatch loopStopwatch = new Stopwatch();

        public MainForm()
        {
            InitializeComponent();
            this.KeyPreview = true;

            if (!serial.IsOpen)
            {
                try
                {
                    serial.Open();
                }
                catch
                {
                    statusBox.Text = "ERROR: Failed to connect to " + serial.PortName + ".";
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        byte ClampToByte(double value)
        {
            return (byte)Clamp(Math.Round(value), 0, 255);
        }

        private byte VoltageToCode(decimal voltage)
        {
            if (voltage > VMAX) voltage = VMAX;
            if (voltage < VMIN) voltage = VMIN;

            decimal fraction = (voltage - VMIN) / (VMAX - VMIN);
            return (byte)Math.Round(fraction * 255m, MidpointRounding.AwayFromZero);
        }

        private decimal CodeToVoltage(byte code)
        {
            return VMIN + (code / 255m) * (VMAX - VMIN);
        }

        private void SendIO(byte port, byte data)
        {
            outgoing[0] = START;
            outgoing[1] = port;
            outgoing[2] = data;
            outgoing[3] = (byte)(START + port + data);

            if (serial.IsOpen)
            {
                serial.Write(outgoing, 0, 4);
            }
            else
            {
                statusBox.Text = "ERROR: Serial not connected.";
            }
        }

        private void SetManualControlsEnabled(bool enabled)
        {
            Send1.Enabled = enabled;
            Send2.Enabled = enabled;
            Get1.Enabled = enabled;
            Get2.Enabled = enabled;
        }

        private void StartLoop()
        {
            if (running) return;
            if (!serial.IsOpen)
            {
                statusBox.Text = "ERROR: Serial not connected.";
                return;
            }

            running = true;
            errorSum = 0.0;
            haveLastError = false;
            haveFilteredError = false;
            filteredError = 0.0;
            loopStopwatch.Restart();
            SetManualControlsEnabled(false);
            statusBox.Text = "Running";

            state = LoopState.AwaitingInput1;
            SendIO(INPUT1, ZERO);
        }

        private void StopLoop()
        {
            running = false;
            state = LoopState.Idle;
            SetManualControlsEnabled(true);
            statusBox.Text = "Stopped";

            const byte STOP_CODE = 128;
            SendIO(OUTPUT1, STOP_CODE);
            SendIO(OUTPUT2, STOP_CODE);
            leftOutLiveBox.Text = STOP_CODE.ToString();
            rightOutLiveBox.Text = STOP_CODE.ToString();
        }

        private void RunPidCycle()
        {
            double sensorMin = (double)sensorMinBox.Value;
            double sensorMax = (double)sensorMaxBox.Value;
            double range = sensorMax - sensorMin;
            if (range <= 0) range = 1;

            double rawNorm1 = (input1Raw - sensorMin) / range;
            double rawNorm2 = (input2Raw - sensorMin) / range;

            const bool INVERT_SENSOR1 = false;
            const bool INVERT_SENSOR2 = false;
            double norm1 = INVERT_SENSOR1 ? 1.0 - rawNorm1 : rawNorm1;
            double norm2 = INVERT_SENSOR2 ? 1.0 - rawNorm2 : rawNorm2;

            double error = norm1 - norm2;

            double dt = loopStopwatch.Elapsed.TotalSeconds;
            loopStopwatch.Restart();
            if (dt <= 0) dt = 0.001;

            double kP = (double)kPBox.Value;
            double kI = (double)kIBox.Value;
            double kD = (double)kDBox.Value;

            errorSum += error * dt;

            if (!haveFilteredError)
            {
                filteredError = error;
                haveFilteredError = true;
            }
            else
            {
                filteredError = D_FILTER_ALPHA * error + (1.0 - D_FILTER_ALPHA) * filteredError;
            }

            double derivative = haveLastError ? (filteredError - errorLast) / dt : 0.0;
            errorLast = filteredError;
            haveLastError = true;

            double controlEffort = error * kP + errorSum * kI + derivative * kD;

            const bool INVERT_STEERING = true;
            double correction = INVERT_STEERING ? -controlEffort : controlEffort;

            double trim = (double)motorTrimBox.Value;

            double basePWM = (double)basePWMBox.Value;
            byte leftCode = ClampToByte(basePWM - correction - trim);
            byte rightCode = ClampToByte(basePWM + correction + trim);

            SendIO(OUTPUT1, leftCode);
            SendIO(OUTPUT2, rightCode);

            errorLiveBox.Text = error.ToString("F3");
            effortLiveBox.Text = controlEffort.ToString("F2");
            leftOutLiveBox.Text = leftCode.ToString();
            rightOutLiveBox.Text = rightCode.ToString();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartLoop();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopLoop();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.S)
            {
                StartLoop();
            }
            else if (e.KeyCode == Keys.X || e.KeyCode == Keys.Escape)
            {
                StopLoop();
            }
        }

        private void Send1_Click(object sender, EventArgs e)
        {
            if (!serial.IsOpen)
            {
                statusBox.Text = "ERROR: Serial not connected.";
                return;
            }

            byte code = VoltageToCode(OutputBox1.Value);
            statusBox.Text = "Output 1: " + code + " = " + Math.Round(CodeToVoltage(code), 2) + "V";
            SendIO(OUTPUT1, code);
        }

        private void Send2_Click(object sender, EventArgs e)
        {
            if (!serial.IsOpen)
            {
                statusBox.Text = "ERROR: Serial not connected.";
                return;
            }

            byte code = VoltageToCode(OutputBox2.Value);
            statusBox.Text = "Output 2: " + code + " = " + Math.Round(CodeToVoltage(code), 2) + "V";
            SendIO(OUTPUT2, code);
        }

        private void Get1_Click(object sender, EventArgs e)
        {
            SendIO(INPUT1, ZERO);
        }

        private void Get2_Click(object sender, EventArgs e)
        {
            SendIO(INPUT2, ZERO);
        }

        private void ioTimer_Tick(object sender, EventArgs e)
        {
            if (!serial.IsOpen) return;
            if (serial.BytesToRead < 4) return;

            incoming[0] = (byte)serial.ReadByte();
            if (incoming[0] != START) return;

            incoming[1] = (byte)serial.ReadByte();
            incoming[2] = (byte)serial.ReadByte();
            incoming[3] = (byte)serial.ReadByte();

            byte checkSum = (byte)(incoming[0] + incoming[1] + incoming[2]);
            if (incoming[3] != checkSum) return;

            switch (incoming[1])
            {
                case INPUT1:
                    input1Raw = incoming[2];
                    input1LiveBox.Text = input1Raw.ToString();
                    InputBox1.Text = input1Raw.ToString();

                    if (running && state == LoopState.AwaitingInput1)
                    {
                        state = LoopState.AwaitingInput2;
                        SendIO(INPUT2, ZERO);
                    }
                    break;

                case INPUT2:
                    input2Raw = incoming[2];
                    input2LiveBox.Text = input2Raw.ToString();
                    InputBox2.Text = input2Raw.ToString();

                    if (running && state == LoopState.AwaitingInput2)
                    {
                        RunPidCycle();

                        if (running)
                        {
                            state = LoopState.AwaitingInput1;
                            SendIO(INPUT1, ZERO);
                        }
                        else
                        {
                            state = LoopState.Idle;
                        }
                    }
                    break;
            }
        }
    }
}
