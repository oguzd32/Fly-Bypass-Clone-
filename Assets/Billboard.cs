using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if(cam == null)
        {
            cam = Camera.main;
        }

        transform.LookAt(transform.position + cam.transform.forward);
    }
}
