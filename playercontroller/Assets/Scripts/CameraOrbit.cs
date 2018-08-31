using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // target to orbit around 
    public bool hideCursor = true; // is the cursor hidden? 
    [Header("Orbit")]
    public Vector3 offset = new Vector3(0, 5f, 0); // position offset
    public float xSpeed = 120f; // x orbit speed
    public float ySpeed = 120f; // y orbit speed 
    public float yMinLimit = -20f; // y clamp min 
    public float yMaxLimit = 80f; // y clamp max
    public float distanceMin = 0.5f;  // min distance to target 
    public float distanceMax = 15f;

    [Header("Collision")]
    public bool cameraCollision; // is cam Collision enabled 
    public float camRadius = 0.3f; // radius of cam Collision cast 
    public LayerMask ignorelayers; // layers ignored by Collision 

    private Vector3 originalOffset;
    private float distance; // current distance
    private float rayDistance = 1000f; //max distance ray can check of collisions 
    private float x = 0f; // x degrees of rotation
    private float y = 0f; // y degrees of rotation

    // Use this for initialization
    void Start()
    {
        // detach camera from parent 
        transform.SetParent(null);
        //set target 
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //is the cursor supposed to be hidden? 
        if (hideCursor) ;   // if cursor is hidden 
        {
            // lock
            Cursor.lockState = CursorLockMode.Locked;
            // hide the cursor 
            Cursor.visible = false;
        }
        // calculate original offset from target
        originalOffset = transform.position - target.position;
        // set rayy distance to current distance magnitude of camera 
        rayDistance = originalOffset.magnitude;
        // get camera rotation 
        Vector3 angles = transform.eulerAngles;
        // set x and y degrees to current camera rotatio 
        x = angles.y;
        y = angles.x;


    }

    // Update is called once per frame
    // transform.setparent(Parents name) this is to automatically parent something to something else
    void Update()
    {
        // if target 
        if (target)
        {
            // rotate the camera based on mouse x and mouse y inputs 
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            // clamp the angle using the custom "clampAngle" function 
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            //  Rotate the transform using euler angles 
            transform.rotation = Quaternion.Euler(y, x, 0);
        }
    }

    void FixedUpdate()
    {
        // if a target has been set 
        if (target)
        {
            // is camera collision enabled? 
            if (cameraCollision)
            {
                // create a ray starting from the targets position and point backwards from the camera 
                Ray camRay = new Ray(target.position, -transform.forward);
                RaycastHit hit;
                // shoot a sphere in a defined ray direction 
                if (Physics.SphereCast(camRay, camRadius, out hit, rayDistance, ~ignorelayers, QueryTriggerInteraction.Ignore))
                {
                    // set current camera distance to hit objects 
                    distance = hit.distance;
                    // exit function 
                    return;
                }
            }
            // set distance to original distance 
            distance = originalOffset.magnitude;
        }
    }


    void LateUpdate()
    {
        // if targe thas been set 
        if (target)
        {
            // calculate our local offset from offset 
            Vector3 LocalOffset = transform.TransformDirection(offset);
            // reposition camera to new position based off distance and offset
            transform.position = (target.position - LocalOffset + -transform.forward * distance);
        }
    }


    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360;
        }
        if (angle > 360f)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
