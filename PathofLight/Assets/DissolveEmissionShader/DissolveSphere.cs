using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveSphere : MonoBehaviour {

    Material mat;
    float dissolveValue = 0.0f;

    private void Start() {
        mat = GetComponent<Renderer>().material;
    }

    private void Update() {
        //mat.SetFloat("_DissolveAmount", Mathf.Sin(Time.time) / 2 + 0.5f);
        mat.SetFloat("_DissolveAmount", dissolveValue);
        if (Input.GetKeyDown(KeyCode.Return))
        {
             if(dissolveValue != 1.0f)
            {
                dissolveValue += 0.01f;
            }
        }

    }
}