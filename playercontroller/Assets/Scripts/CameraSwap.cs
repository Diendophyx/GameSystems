using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    public Transform[] lookObjects; //collection of things to look at
    public GameObject objectHolder;
    public bool smooth = true; //whether or not the lerping is smooth
    public float damping = 6f; //smoothness value
    [Header("GUI")]
    public float screenWidth;
    public float screenHeight;

    public int objectIndex;
    public int objectMax;
    private Transform target;

    // Use this for initialization
    void Start()
    {
        objectHolder = GameObject.Find("ObjectHolder");
        lookObjects = objectHolder.GetComponentsInChildren<Transform>();

        //Last index of array
        objectMax = lookObjects.Length - 1;
        objectIndex = 1; //ignores the parent
    }

    // Update is called once per frame
    void LateUpdate()
    {
        target = lookObjects[objectIndex];
        if (target)
        {
            if (smooth)
            {
                Vector3 lookDirection = target.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(lookDirection);

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, damping * Time.deltaTime);
            }
            else
            {
                transform.LookAt(target);
            }
        }
        else
        {
            CamSwap();
        }
        
    }

    void CamSwap()
    {
        objectIndex += 1;

        if (objectIndex > objectMax)
        {
            objectIndex = 1;
        }
    }

    private void OnGUI()
    {
        if (screenWidth != Screen.width / 16)
        {
            screenWidth = Screen.width / 16;
        }
        if (screenHeight != Screen.height / 16)
        {
            screenHeight = Screen.height / 16;
        }
        if (GUI.Button(new Rect(0.5f * screenWidth, 0.25f * screenHeight, 1.5f * screenWidth, 0.5f * screenHeight), "Swap"))
        {
            CamSwap();
        }
    }
}
