using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialOutput : MonoBehaviour
{
    Component[] walls;
    public int numToSend;
    
    // Start is called before the first frame update
    void Start()
    {
        walls = gameObject.GetComponentsInChildren<WallIndex>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(walls.Length);
        for(int i = 0; i < walls.Length; i ++)
        {
            if (walls[i].GetComponent<WallIndex>().sendThisIndex == true)
            {
                numToSend = walls[i].GetComponent<WallIndex>().indexNumber;
                GameObject.Find("SerialComPort").GetComponent<SerialCommunication>().serialWriteByCollision(numToSend);
            }
            else
            {
                numToSend = 0;
            }
        }
    }
}
