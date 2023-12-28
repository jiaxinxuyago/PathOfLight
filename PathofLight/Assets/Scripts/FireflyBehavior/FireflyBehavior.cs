using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int indexNumber;
    public bool lightOnRecieved;
    float lightIntensity;
    float lightRange;
    float haloSize;
    Component halo;

    void Start()
    {
        halo = GetComponent("Halo");
    }

    // Update is called once per frame
    void Update()
    {
        if (lightOnRecieved == true)
        {
            lightIntensity = gameObject.GetComponentInParent<SerialInput>().mappedIntensity[indexNumber];
            lightRange = GameObject.Find("SerialComPort").GetComponent<SerialCommunication>().map(lightIntensity, 0, 15, 10, 100);
            haloSize = GameObject.Find("SerialComPort").GetComponent<SerialCommunication>().map(lightIntensity, 0, 15, 0, 5);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Light>().enabled = true;
            //gameObject.GetComponent<Halo>().enabled = true;
            halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
            gameObject.GetComponent<Light>().intensity = lightIntensity;
            gameObject.GetComponent<Light>().range = lightRange;
            //gameObject.GetComponent<Halo>().size = haloSize;
            //halo.GetType().GetProperty("size").SetValue(haloSize);
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Light>().enabled = false;
            halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
        }
    }
}
