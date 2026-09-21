// Curtin University
// Mechatronics Engineering
// MXEN3000 - Milestone 4 (Final Vehicle) - PID line-follower controller
//
// Separate project from SerialIO_GUI_Sample_2016_4byte (the Milestone 2/3 bench
// tester). That project only sent one value on a button click and displayed one
// reading at a time - there was no loop that ran the vehicle on its own. This
// project adds that loop: an autonomous PID control loop (Start/Stop, keyboard
// S/X), plus the original manual Send/Get controls kept for bench diagnostics.
//
// Serial protocol (unchanged from the original driver / Arduino sketch):
// <startByte><commandByte><dataByte><checkByte>, checkByte = sum of the first three.

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PIDLineFollowerGUI
{
    public partial class MainForm : Form
    {
        // ---- Serial message plumbing (same protocol as the original driver) ----
        byte[] outgoing = new byte[4];
        byte[] incoming = new byte[4];

        const byte START = 255;
        const byte ZERO = 0;

        const byte INPUT1 = 0;
        const byte INPUT2 = 1;
        const byte OUTPUT1 = 2;
        const byte OUTPUT2 = 3;

        // ---- Autonomous control loop state ----
        // The link is half-duplex request/response: ask for one sensor value, wait
        // for the reply. Getting both sensor readings each control cycle means
        // sequencing the requests - that's what this state machine does.
        enum LoopState { Idle, AwaitingInput1, AwaitingInput2 }
        LoopState state = LoopState.Idle;
        bool running = false;

        int input1Raw = 0;
        int input2Raw = 0;

        double errorSum = 0.0;
        double errorLast = 0.0;
        bool haveLastError = false;
        readonly Stopwatch loopStopwatch = new Stopwatch();

        public MainForm()
        {
            InitializeComponent();
            this.KeyPreview = true; // so S / X are caught here rather than needing focus on a specific control

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
            // Added to match the Load event wiring Visual Studio's designer
            // inserted into InitializeComponent(). Nothing needs to happen
            // here - initialization is already done in the constructor above.
        }

        // ==================== Autonomous PID loop ====================

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

        // Sends a four byte message using the same format/checksum as the Arduino side.
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

            // Command both outputs to 0 so the vehicle doesn't keep driving on the
            // last PWM command sent before Stop was pressed.
            SendIO(OUTPUT1, 0);
            SendIO(OUTPUT2, 0);
            leftOutLiveBox.Text = "0";
            rightOutLiveBox.Text = "0";
        }

        // Runs one PID iteration once both sensor readings for this cycle are in.
        // Extends the PI form used in the unit's own notes
        // (Control Effort = Error*kP + SumError*kI) with a derivative term, using a
        // measured dt rather than the timer's nominal interval - see README.
        private void RunPidCycle()
        {
            double sensorMin = (double)sensorMinBox.Value;
            double sensorMax = (double)sensorMaxBox.Value;
            double range = sensorMax - sensorMin;
            if (range <= 0) range = 1; // guard against a bad calibration entry

            double norm1 = (input1Raw - sensorMin) / range;
            double norm2 = (input2Raw - sensorMin) / range;
            double error = norm1 - norm2;

            double dt = loopStopwatch.Elapsed.TotalSeconds;
            loopStopwatch.Restart();
            if (dt <= 0) dt = 0.001; // guard against a zero interval on the very first sample

            double kP = (double)kPBox.Value;
            double kI = (double)kIBox.Value;
            double kD = (double)kDBox.Value;
            double integralClamp = (double)integralClampBox.Value;

            errorSum += error * dt;
            errorSum = Clamp(errorSum, -integralClamp, integralClamp); // anti-windup

            double derivative = haveLastError ? (error - errorLast) / dt : 0.0;
            errorLast = error;
            haveLastError = true;

            double controlEffort = error * kP + errorSum * kI + derivative * kD;

            double basePWM = (double)basePWMBox.Value;
            byte leftCode = ClampToByte(basePWM - controlEffort);
            byte rightCode = ClampToByte(basePWM + controlEffort);

            SendIO(OUTPUT1, leftCode);
            SendIO(OUTPUT2, rightCode);

            // Live readout for tuning.
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

        // Vehicles must be keyboard controlled for both starting and stopping
        // (Milestone 4 final demonstration rule) - S starts, X or Escape stops.
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

        // ==================== Manual / bench controls (kept from the original GUI) ====================

        private void Send1_Click(object sender, EventArgs e)
        {
            if (!serial.IsOpen)
            {
                statusBox.Text = "ERROR: Serial not connected.";
                return;
            }

            byte code = (byte)OutputBox1.Value;
            statusBox.Text = "Sent Output 1 = " + code;
            SendIO(OUTPUT1, code);
        }

        private void Send2_Click(object sender, EventArgs e)
        {
            if (!serial.IsOpen)
            {
                statusBox.Text = "ERROR: Serial not connected.";
                return;
            }

            byte code = (byte)OutputBox2.Value;
            statusBox.Text = "Sent Output 2 = " + code;
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

        // ==================== Incoming serial data ====================
        // Handles both the autonomous loop's request/response sequencing and the
        // manual bench Get1/Get2 button replies, using the same parsing the
        // original GUI used.
        private void ioTimer_Tick(object sender, EventArgs e)
        {
            if (!serial.IsOpen) return;
            if (serial.BytesToRead < 4) return;

            incoming[0] = (byte)serial.ReadByte();
            if (incoming[0] != START) return; // not aligned to a message start; drop and wait for the next tick

            incoming[1] = (byte)serial.ReadByte();
            incoming[2] = (byte)serial.ReadByte();
            incoming[3] = (byte)serial.ReadByte();

            byte checkSum = (byte)(incoming[0] + incoming[1] + incoming[2]);
            if (incoming[3] != checkSum) return; // corrupt message; drop it

            switch (incoming[1])
            {
                case INPUT1:
                    input1Raw = incoming[2];
                    input1LiveBox.Text = input1Raw.ToString(); // A6 readout (PID group)
                    InputBox1.Text = input1Raw.ToString();     // Input 1 readout (manual/bench group)

                    if (running && state == LoopState.AwaitingInput1)
                    {
                        state = LoopState.AwaitingInput2;
                        SendIO(INPUT2, ZERO);
                    }
                    break;

                case INPUT2:
                    input2Raw = incoming[2];
                    input2LiveBox.Text = input2Raw.ToString(); // A7 readout (PID group)
                    InputBox2.Text = input2Raw.ToString();     // Input 2 readout (manual/bench group)

                    if (running && state == LoopState.AwaitingInput2)
                    {
                        RunPidCycle();

                        if (running) // Stop() may have been pressed while this cycle was in flight
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
