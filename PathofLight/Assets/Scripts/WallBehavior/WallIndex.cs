using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallIndex : MonoBehaviour
{
    // Start is called before the first frame update
    public int indexNumber;
    public bool sendThisIndex = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        sendThisIndex = gameObject.GetComponentInChildren<CollisionDetection>().isShowingUp;

        if (sendThisIndex == true)
        {
            //Debug.Log(sendThisIndex);
            //Debug.Log(indexNumber);
        }
        else
        {
            //Debug.Log("N/A");
        }
    }
}
