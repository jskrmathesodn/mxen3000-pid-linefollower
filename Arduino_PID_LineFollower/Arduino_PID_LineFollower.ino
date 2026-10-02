byte output1 = 128;
byte output2 = 128;
byte input1 = 0;
byte input2 = 0;

byte startByte = 0;
byte commandByte = 0;
byte dataByte = 0;
byte checkByte = 0;

byte checkSum = 0;

const byte START = 255;

const byte INPUT1 = 0;
const byte INPUT2 = 1;
const byte OUTPUT1 = 2;
const byte OUTPUT2 = 3;

const byte DACPIN1[8] = {2, 3, 4, 5, 9, 8, 7, 6};
const byte DACPIN2[8] = {A2, A3, A4, A5, A1, A0, 11, 10};
const byte SENSOR1 = A6;
const byte SENSOR2 = A7;

void outputToDAC1( byte data )
{
  for( int i = 0; i<=7; i++ )
  {
    digitalWrite( DACPIN1[i] , ((data>>i)&1 ? HIGH : LOW));
  }
}

void outputToDAC2( byte data )
{
  for( int i = 0; i<=7; i++ )
  {
    digitalWrite( DACPIN2[i] , ((data>>i)&1 ? HIGH : LOW));
  }
}

void initDACs()
{
  for( int i = 0; i<=7; i++ )
  {
    pinMode( DACPIN1[i] , OUTPUT);
    pinMode( DACPIN2[i] , OUTPUT);
  }
}

void setup()
{
  Serial.begin(9600);
  initDACs();

  outputToDAC1(output1);
  outputToDAC2(output2);
}

void loop()
{
  if (Serial.available() >= 4)
  {
    startByte = Serial.read();

    if(startByte == START)
    {
      commandByte = Serial.read();
      dataByte = Serial.read();
      checkByte = Serial.read();

      checkSum = startByte + commandByte + dataByte;

      if(checkByte == checkSum)
      {
        switch(commandByte)
        {
          case INPUT1:
          {
            input1 = analogRead(SENSOR1) >> 2;

            Serial.write(START);
            Serial.write(commandByte);
            Serial.write(input1);
            int checkSum1 = START + commandByte + input1;
            Serial.write(checkSum1);
          }
          break;
          case INPUT2:
          {
            input2 = analogRead(SENSOR2) >> 2;

            Serial.write(START);
            Serial.write(commandByte);
            Serial.write(input2);
            int checkSum2 = START + commandByte + input2;
            Serial.write(checkSum2);
          }
          break;
          case OUTPUT1:
          {
            output1 = dataByte;
            outputToDAC1(output1);
          }
          break;
          case OUTPUT2:
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
