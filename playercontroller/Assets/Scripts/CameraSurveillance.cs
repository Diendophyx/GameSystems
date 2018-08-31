using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSurveillance : MonoBehaviour
{

    public Camera[] cameras;
    public KeyCode prevkey = KeyCode.Q; // filter to previous cam
    public KeyCode nextkey = KeyCode.E; // Filter to Next camera 
    private int camIndex; // current cam index from array 
    private int camMax; // max amount of cams in array 
    private Camera current; // current camera selected 
	void Start ()
    {
        // Get all camera children and store into array 
        cameras = GetComponentsInChildren<Camera>();
        // last index of array = array.length -1
        camMax = cameras.Length - 1;
        // actvate the default camera 
        
	}

    private void Update()
    {
        if(Input.GetKeyDown(nextkey))
        {
            camIndex++;

            if(camIndex >= camMax)
            {
                camIndex = 0;
            }
            ActivateCamera(camIndex);
        }

        if(Input.GetKeyDown(prevkey))
        {
            camIndex--;

            if(camIndex < 0)
            {
                camIndex = camMax;
            }
            ActivateCamera(camIndex);
        }
    }

    void ActivateCamera(int camIndex)
    {
        for (int i = 0; i < cameras.Length; i++)
        {


            // loop through all surveliance cameras 
            Camera cam = cameras[i];
            // if the current indes matches the argument camIndex
            if (i == camIndex)
            {
                // enable this camera 
                cam.gameObject.SetActive(true);
            }
            else
            {
                cam.gameObject.SetActive(false);
            }
        }
	}
}
