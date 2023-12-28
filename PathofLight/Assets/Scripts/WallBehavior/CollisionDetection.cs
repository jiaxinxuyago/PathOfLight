using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Transform myParent;
    Renderer myMesh;
    Renderer parentMesh;
    Material parentMeshMat;
    Light spotLight;

    public bool isShowingUp = false;
    public float dissolveValue = 1.0f;
    void Start()
    {
        myMesh = GetComponent<MeshRenderer>();
        myParent = gameObject.transform.parent;
        parentMesh = gameObject.transform.parent.GetComponent<MeshRenderer>();
        parentMeshMat = gameObject.transform.parent.GetComponent<MeshRenderer>().material;
        spotLight = myParent.GetChild(2).GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player");

        spotLight.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        //disolving test
        //parentMeshMat.SetFloat("_DissolveAmount", Mathf.Sin(Time.time) / 2 + 0.5f);

        if (isShowingUp == true)
        {
            //show mesh
            parentMesh.enabled = true;
            spotLight.enabled = true;

            if (dissolveValue > 0.0f)
            {
                dissolveValue -= 0.01f;
            }
            parentMeshMat.SetFloat("_DissolveAmount", dissolveValue);
        }
        else
        {
            if (dissolveValue < 1.0f)
            {
                dissolveValue += 0.01f;
            }

            parentMeshMat.SetFloat("_DissolveAmount", dissolveValue);

            if (parentMeshMat.GetFloat("_DissolveAmount") == 1.0f)
            {
                //turn off mesh
                parentMesh.enabled = false;
                spotLight.enabled = false;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("detected!");
            //if (parentMesh.enabled == false)
            //{
                //parentMesh.enabled = true;
            //}

            
            isShowingUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isShowingUp = false;
        }
    }
}
