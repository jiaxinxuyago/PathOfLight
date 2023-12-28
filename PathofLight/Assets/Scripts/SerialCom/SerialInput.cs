using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialInput: MonoBehaviour
{
    // Start is called before the first frame update
    bool serialInputStateLeft = false;
    float serialInputDisplacement = 0;
    int[] serialInputLightOn;
    float[] lightIntensity;
    

    //firefly array
    Component[] fireFlies;
    int thisFirefly;
    public float[] mappedIntensity;

    //define input type
    public bool binaryInput;
    public bool analogInput;
    public bool analogToggle;


    void Start()
    {
        fireFlies = gameObject.GetComponentsInChildren<FireflyBehavior>();
    }

    // Update is called once per frame
    void Update()

    {
        //input state from serial
        serialInputStateLeft = GameObject.Find("SerialComPort").GetComponent<SerialCommunication>().inputState01;

        //input analog from serial array
        int arrLength = GameObject.Find("SerialComPort").GetComponent<SerialCommunication>().inputAnaStateArr.Length;
        serialInputLightOn = GameObject.Find("SerialComPort").GetComponent<SerialCommunication>().inputAnaStateArr;

        //get brightness from array
        lightIntensity = GameObject.Find("SerialComPort").GetComponent<SerialCommunication>().brightnessArr;

        //keyboard OR Arduino Digital Input
        if (binaryInput == true) {
            if (Input.GetKeyDown(KeyCode.J) || serialInputStateLeft == true)
            {
                Vector3 position = this.transform.position;
                position.x++;
                this.transform.position = position;
            }
        }
        

        //Arduino Analog Input
        if (analogInput == true) {
            Debug.Log("No analog read from potentiometer avaliable!!!!!!");
        }

        //Arduino Analog Toggle
        //Lights On/Off
        if (analogToggle == true ) {

            for (int i = 0; i < arrLength; i ++)
            {
                thisFirefly = fireFlies[i].GetComponent<FireflyBehavior>().indexNumber;
                mappedIntensity[thisFirefly] = GameObject.Find("SerialComPort").GetComponent<SerialCommunication>().map(lightIntensity[thisFirefly], 80, 300, 0, 15);

                if (serialInputLightOn[thisFirefly] == 1)
                {
                    //Debug.Log("Yes");
                    fireFlies[thisFirefly].GetComponent<FireflyBehavior>().lightOnRecieved = true;
                    
                }
                else
                {
                    //Debug.Log("No");
                    fireFlies[thisFirefly].GetComponent<FireflyBehavior>().lightOnRecieved = false;
                }
            }
        }

    }
}
