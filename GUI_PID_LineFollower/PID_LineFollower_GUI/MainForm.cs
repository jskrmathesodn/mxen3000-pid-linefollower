using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PIDLineFollowerGUI
{
    public partial class MainForm : Form
    {
        byte[] outgoing = new byte[4]; // create a buffer for outgoing data
        byte[] incoming = new byte[4]; // create a buffer for incoming data

        const byte START = 255;
        const byte ZERO = 0;

        const byte INPUT1 = 0; // input 1 is A6 (left sensor)
        const byte INPUT2 = 1; // input 2 is A7 (right sensor)
        const byte OUTPUT1 = 2; // output 1 is the left motor
        const byte OUTPUT2 = 3; // output 2 is the right motor

        const decimal VMIN = -15m;
        const decimal VMAX = 15m;

        enum LoopState { Idle, AwaitingInput1, AwaitingInput2 } // tracks state of the loop
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
        const double INTEGRAL_EFFORT_LIMIT = 100.0; // maximum control effort (in DAC codes) the integral term is allowed to contribute
        readonly Stopwatch loopStopwatch = new Stopwatch(); // stopwatch to measure real loop time (for dt)

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

        double Clamp(double value, double min, double max) // clamp function
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        byte ClampToByte(double value) // clamps a double and turns to a byte (0-255) for PWM output
        {
            return (byte)Clamp(Math.Round(value), 0, 255);
        }

        private byte VoltageToCode(decimal voltage) // converts a voltage in the range of VMIN to VMAX to a byte code in the range of 0 to 255
        {
            if (voltage > VMAX) voltage = VMAX;
            if (voltage < VMIN) voltage = VMIN;

            decimal fraction = (voltage - VMIN) / (VMAX - VMIN);
            return (byte)Math.Round(fraction * 255m, MidpointRounding.AwayFromZero);
        }

        private decimal CodeToVoltage(byte code) // converts a byte code in the range of 0 to 255 to a voltage in the range of VMIN to VMAX
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
                statusBox.Text = "ERROR: Serial not connected."; // display error if serial is not connected
            }
        }

        private void SetManualControlsEnabled(bool enabled) // enables or disables the manual control buttons
        {
            Send1.Enabled = enabled;
            Send2.Enabled = enabled;
            Get1.Enabled = enabled;
            Get2.Enabled = enabled;
        }

        private void StartLoop() // starts the PID loop
        {
            if (running) return; // if already running, do nothing
            if (!serial.IsOpen) // if serial is not open, display error and return
            {
                statusBox.Text = "ERROR: Serial not connected.";
                return;
            }

            running = true;
            errorSum = 0.0; // reset the integral term
            haveLastError = false; // reset the derivative term
            haveFilteredError = false; // reset the filtered error
            filteredError = 0.0;
            loopStopwatch.Restart(); // start the stopwatch to measure loop time
            SetManualControlsEnabled(false); // disable manual controls while running
            statusBox.Text = "Running";

            state = LoopState.AwaitingInput1; // set state to awaiting input 1
            SendIO(INPUT1, ZERO); // request input 1 from the microcontroller
        }

        private void StopLoop()
        {
            running = false; // stop the loop
            state = LoopState.Idle; // set state to idle
            SetManualControlsEnabled(true); // enable manual controls
            statusBox.Text = "Stopped";

            const byte STOP_CODE = 128; // 128 is the code for 0V output, which stops the motors
            SendIO(OUTPUT1, STOP_CODE); // send stop code to left motor
            SendIO(OUTPUT2, STOP_CODE); // send stop code to right motor
            leftOutLiveBox.Text = STOP_CODE.ToString();
            rightOutLiveBox.Text = STOP_CODE.ToString();
        }

        private void RunPidCycle()
        {
            double sensorMin = (double)sensorMinBox.Value; // get the sensor min value from the UI
            double sensorMax = (double)sensorMaxBox.Value; // get the sensor max value from the UI
            double range = sensorMax - sensorMin; // calculate the range of the sensor values
            if (range <= 0) range = 1; // gives range as a no-op value if the user has not set the sensor min and max values correctly

            double rawNorm1 = (input1Raw - sensorMin) / range; // normalize the first sensor value
            double rawNorm2 = (input2Raw - sensorMin) / range; // normalize the second sensor value

            const bool INVERT_SENSOR1 = false;
            const bool INVERT_SENSOR2 = false;
            double norm1 = INVERT_SENSOR1 ? 1.0 - rawNorm1 : rawNorm1; // invert the first sensor value if needed
            double norm2 = INVERT_SENSOR2 ? 1.0 - rawNorm2 : rawNorm2; // invert the second sensor value if needed

            double error = norm1 - norm2; // calculate the error as the difference between the two normalized sensor values

            double dt = loopStopwatch.Elapsed.TotalSeconds; // get the elapsed time since the last loop cycle
            loopStopwatch.Restart();
            if (dt <= 0) dt = 0.001; // guards against division by zero if the loop is running too fast

            double kP = (double)kPBox.Value;
            double kI = (double)kIBox.Value;
            double kD = (double)kDBox.Value;

            errorSum += error * dt; // integrate the error over time for the integral term
            if (kI > 0)
            {
                double maxErrorSum = INTEGRAL_EFFORT_LIMIT / kI; // largest errorSum that keeps the integral contribution within the limit
                errorSum = Math.Max(-maxErrorSum, Math.Min(maxErrorSum, errorSum)); // clamp errorSum to stop integral windup
            }

            if (!haveFilteredError)
            {
                filteredError = error; // on first cycle, set the filtered error to the current error
                haveFilteredError = true;
            }
            else
            {
                filteredError = D_FILTER_ALPHA * error + (1.0 - D_FILTER_ALPHA) * filteredError; // apply low-pass filter to the error for the derivative term
            }

            double derivative = haveLastError ? (filteredError - errorLast) / dt : 0.0; // calculate the derivative of the error
            errorLast = filteredError;
            haveLastError = true;

            double controlEffort = error * kP + errorSum * kI + derivative * kD; // calculate the control effort using the PID formula

            const bool INVERT_STEERING = true;
            double correction = INVERT_STEERING ? -controlEffort : controlEffort;

            double trim = (double)motorTrimBox.Value; // get the motor trim value from the UI if motors inbalanced

            double basePWM = (double)basePWMBox.Value; // get the base PWM value from the UI
            bool reversing = basePWM < 128; // a base below the stop code (128) means the vehicle is driving backwards
            double steer = reversing ? -correction : correction; // flip the steering correction when reversing, as the feedback sign is opposite
            byte leftCode = ClampToByte(basePWM - steer - trim); // calculate the left motor PWM code
            byte rightCode = ClampToByte(basePWM + steer + trim); // calculate the right motor PWM code

            SendIO(OUTPUT1, leftCode); // send the left motor PWM code to the microcontroller
            SendIO(OUTPUT2, rightCode); // send the right motor PWM code to the microcontroller

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

        private void ApplyPreset(decimal p, decimal i, decimal d, decimal basePwm) // applies a preset PID configuration for each track
        {
            kPBox.Value = p;
            kIBox.Value = i;
            kDBox.Value = d;
            basePWMBox.Value = basePwm;
        }

        private void presetCWCircle_Click(object sender, EventArgs e) // configuration for clockwise circle
        {
            ApplyPreset(9.9m, 5m, 2.6m, 195m);
        }

        private void presetACWCircle_Click(object sender, EventArgs e) // configuration for counter-clockwise circle
        {
            ApplyPreset(9.9m, 5m, 2.6m, 195m);
        }

        private void presetSquiggle_Click(object sender, EventArgs e) // configuration for squiggle track
        {
            ApplyPreset(9.9m, 0m, 2.6m, 185m);
        }

        private void presetReverse_Click(object sender, EventArgs e) // configuration for reverse track
        {
            ApplyPreset(2m, 0m, 1.5m, 80m);
        }

        protected override void OnKeyDown(KeyEventArgs e) // allows user to stop and start by pressing S or X
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

        private void Send1_Click(object sender, EventArgs e) // sends the value from OutputBox1 to the microcontroller
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
            if (!serial.IsOpen) return; // if serial is not open, do nothing
            if (serial.BytesToRead < 4) return; // if there are not enough bytes to read, do nothing

            incoming[0] = (byte)serial.ReadByte(); // read the first byte from the serial port
            if (incoming[0] != START) return; // if the first byte is not the start byte, do nothing

            incoming[1] = (byte)serial.ReadByte();
            incoming[2] = (byte)serial.ReadByte();
            incoming[3] = (byte)serial.ReadByte();

            byte checkSum = (byte)(incoming[0] + incoming[1] + incoming[2]); // calculate the checksum of the incoming data
            if (incoming[3] != checkSum) return; // if the checksum does not match, do nothing

            switch (incoming[1])
            {
                case INPUT1: // if the incoming data is from input 1
                    input1Raw = incoming[2]; // store the raw value of input 1
                    input1LiveBox.Text = input1Raw.ToString();
                    InputBox1.Text = input1Raw.ToString();

                    if (running && state == LoopState.AwaitingInput1) // if the loop is running and we are awaiting input 1 advance the state
                    {
                        state = LoopState.AwaitingInput2;
                        SendIO(INPUT2, ZERO);
                    }
                    break;

                case INPUT2:
                    input2Raw = incoming[2];
                    input2LiveBox.Text = input2Raw.ToString();
                    InputBox2.Text = input2Raw.ToString();

                    if (running && state == LoopState.AwaitingInput2) // if the loop is running and we are awaiting input 2 advance the state
                    {
                        RunPidCycle(); // run the PID cycle with the new input values

                        state = LoopState.AwaitingInput1;
                        SendIO(INPUT1, ZERO);
                    }
                    break;
            }
        }
    }
}
