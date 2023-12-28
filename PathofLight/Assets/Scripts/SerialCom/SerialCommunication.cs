using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class SerialCommunication : MonoBehaviour
{
    // Start is called before the first frame update

    //variables to read out from serial
     //binary to state
    public bool inputState01 = false;
     //analog to state
    public int inputAnaState01 = 0;
     //brightness data remapped
    public float brightness = 0;

    //converted serial data array
   
    //input state array read from photo transistor
    public int[] inputAnaStateArr;
    //brightness array
    public float[] brightnessArr;


    //serial name and serial nums from the array
    SerialPort stream = new SerialPort("COM11", 9600);
    int previousBiValue01 = 0;
    float previousAnaValue01 = 0;
    float analogOffset = 0;

    //brightness to load after treshohold
    public float brightnessTreshold = 100;

    void Start()
    {
        //open serial port
        stream.Open();

        //delay
        //stream.ReadTimeout = 50;
    }

    // Update is called once per frame
    void Update()
    {
        //convert data
        string readOut = stream.ReadLine();
        string readOutTrimmed = readOut.Trim();
        string[] readOutInfo = readOutTrimmed.Split(',');
        int valueBinary01 = int.Parse(readOutInfo[0]);
        //float valueAnalogBrightness01 = float.Parse(readOutInfo[1]);

        //retrevied photo transistor data from serial array
        float[] phototransArr = new float[readOutInfo.Length - 1];

        //convert string into floats
        for(int i = 0; i < phototransArr.Length; i ++)
        {
            phototransArr[i] = float.Parse(readOutInfo[i + 1]);
        }

        //SERIAL READING

            //bianry input into states
        inputState01 = binaryToState(valueBinary01, previousBiValue01);
        previousBiValue01 = valueBinary01;

            //analog input from photo trans array
        for(int i = 0; i < inputAnaStateArr.Length; i ++)
        {
            inputAnaStateArr[i] = analogToThreshold(phototransArr[i], brightnessTreshold);
            brightnessArr[i] = analogBrightness(inputAnaStateArr[i], phototransArr[i]);
        }


        //SERIAL WRITING

            //serial writing by keyboard (DEBUG)
        serialWriteByKey();

           //serial writing by collision
        //
    } 

    //map function
    public float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    public float analogToNumber(float serialValue, float previousValue)
    {
        float input = 0;
        float offset = 0;
        if (serialValue != previousValue)
        {
            offset = serialValue - previousValue;
        }

        input = map(offset, 0, 255, 0, 1000);

        return input;
    }

    public bool binaryToState(int serialValue, int previousValue)
    {
        bool inputState;
        
        if (serialValue - previousValue == 1)
        {
            inputState = true;
        }
        else
        {
            inputState = false;
        }

        return inputState;
    }

    public int analogToThreshold(float serialValue, float threshold)
    {
        int inputState;

        if (serialValue > threshold)
        {
            inputState = 1;
        }
        else
        {
            inputState = 0;
        }

        return inputState;
    }

    public float analogBrightness(int inputState, float serialValue)
    {
        float mybrightness = 0;

        if (inputState == 1)
        {
            mybrightness = serialValue;
        }
        return mybrightness;
    }

    public void serialWriteByKey()
    {
        bool outputState = false;

        if (Input.GetKey(KeyCode.Q))
        {
            outputState = true;
        }
        else
        {
            outputState = false;
        }

        if (outputState == true)
        {
            stream.Write("10");
        }
        else
        {
            stream.Write("0");
        }
    }

    public void serialWriteByCollision(int thisNum)
    {
        string numToString = "";

        if(thisNum != 0)
        {
            numToString = thisNum.ToString();
            stream.Write(numToString);
        }
        else
        {
            stream.Write("0");
        } 

        //Debug.Log(thisNum);
    }
}
