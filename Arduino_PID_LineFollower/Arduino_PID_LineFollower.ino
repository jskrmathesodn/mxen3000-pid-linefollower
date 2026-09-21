//Curtin University
//Mechatronics Engineering
//MXEN3000 - Milestone 4 (Final Vehicle) - Arduino I/O driver
//
//Derived from SerialIO_Arduino_Driver_4BytePackage.ino (the Milestone 2/3 bench-test
//driver). Same 4-byte serial protocol and pin map, kept unchanged so this still talks
//to the same protocol as the PC-side driver. The Arduino stays a pure I/O relay here;
//the PID control algorithm runs on the PC side (see GUI_PID_LineFollower), because the
//final demo requires a single PC executable with keyboard start/stop, and this board
//has no logic beyond checksum + port switch either way.
//

//Declare variables for storing the port values.
byte output1 = 255;
byte output2 = 255;
byte input1 = 0;
byte input2 = 0;

//Declare variables for each byte of the message.
byte startByte = 0;
byte commandByte = 0;
byte dataByte = 0;
byte checkByte = 0;

//Declare variable for calculating the check sum which is used to confirm that the correct bytes were identified as the four message bytes.
byte checkSum = 0;

//Declare a constant for the start byte ensuring that the value is static.
const byte START = 255;

//Declare constants to enumerate the port values.
const byte INPUT1 = 0;
const byte INPUT2 = 1;
const byte OUTPUT1 = 2;
const byte OUTPUT2 = 3;

const byte DACPIN1[8] = {2, 3, 4, 5, 9, 8, 7, 6};
const byte DACPIN2[8] = {A2, A3, A4, A5, A1, A0, 11, 10};
const byte SENSOR1 = A6;
const byte SENSOR2 = A7;

void outputToDAC1( byte data ) //loop through lookup table of Arduino pins connected to DAC and write correct bit to each
{
  for( int i = 0; i<=7; i++ )
  {
    digitalWrite( DACPIN1[i] , ((data>>i)&1 ? HIGH : LOW));
  }
}

void outputToDAC2( byte data ) //loop through lookup table of Arduino pins connected to DAC and write correct bit to each
{
  for( int i = 0; i<=7; i++ )
  {
    digitalWrite( DACPIN2[i] , ((data>>i)&1 ? HIGH : LOW));
  }
}

void initDACs() //loop through lookup table of Arduino pins connected to DAC and set to output mode
{
  for( int i = 0; i<=7; i++ )
  {
    pinMode( DACPIN1[i] , OUTPUT);
    pinMode( DACPIN2[i] , OUTPUT);
  }
}

//Setup initialises pins as inputs or outputs and begins the serial.
//Note: A6/A7 need no pinMode() call - they are analog-input-only pins, unlike a
//normal digital pin, and are read directly with analogRead() below.
void setup()
{
  Serial.begin(9600);
  initDACs();
}

//Main program manages setting/reading of ports via serial.
void loop()
{
  if (Serial.available() >= 4) // Check that a full package of four bytes has arrived in the buffer.
  {
    startByte = Serial.read(); // Get the first available byte from the buffer, assuming that it is the start byte.

    if(startByte == START) // Confirm that the first byte was the start byte, otherwise begin again checking the next byte.
    {
      //Read the remaining three bytes of the package into the respective variables.
      commandByte = Serial.read();
      dataByte = Serial.read();
      checkByte = Serial.read();

      checkSum = startByte + commandByte + dataByte; // Calculate the check sum, this is also calculated on the PC side and is sent as the final byte of the package.

      if(checkByte == checkSum) //Confirm that the calculated and sent check sum match, if so it is safe to process the data.
      {
        //Check the command byte to determine which port is being called and respond accordingly.
        switch(commandByte)
        {
          case INPUT1: //CHANGED: analogRead (10-bit, 0-1023), not digitalRead - see header comment.
          {
            input1 = analogRead(SENSOR1) >> 2; //Keep the top 8 bits so it fits the protocol's single data byte.

            Serial.write(START); //Send the start byte indicating the start of a package.
            Serial.write(commandByte); //Echo the command byte to inform the PC which port value is being sent.
            Serial.write(input1); //Send the value read.
            int checkSum1 = START + commandByte + input1; //Calculate the check sum.
            Serial.write(checkSum1); //Send the check sum.
          }
          break;
          case INPUT2: //CHANGED: analogRead, same reasoning as INPUT1.
          {
            input2 = analogRead(SENSOR2) >> 2;

            Serial.write(START); //Send the start byte indicating the start of a package.
            Serial.write(commandByte); //Echo the command byte to inform the PC which port value is being sent.
            Serial.write(input2); //Send the value read.
            int checkSum2 = START + commandByte + input2; //Calculate the check sum.
            Serial.write(checkSum2); //Send the check sum.
          }
          break;
          case OUTPUT1: //For Output 1 the value of the data byte is written to pins in DACPIN1. Unchanged.
          {
            output1 = dataByte;
            outputToDAC1(output1);
          }
          break;
          case OUTPUT2: //For Output 2 the value of the data byte is written to pins in DACPIN2. Unchanged.
          {
            output2 = dataByte;
            outputToDAC2(output2);
          }
          break;
        }
      }
    }
  }
}

//Function to reverse the order of the bits. Unused by the protocol above; kept from
//the original driver in case downstream code still calls it.
byte bitFlip(byte value)
{
       byte bFlip = 0;
       byte j=7;
       for (byte i=0; i<8; i++) {
         bitWrite(bFlip, i, bitRead(value, j));
         j--;
       }
       return bFlip;
}
