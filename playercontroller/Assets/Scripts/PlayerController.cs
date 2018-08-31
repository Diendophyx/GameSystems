using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool rotateToMainCamera = false;
    public Transform weapon;
    public float moveSpeed = 10f;
    public float jumpheight = 10f;
    public Rigidbody rigid;
    private bool isGrounded = true;
    public float RayDistance = 1f;

    private void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);

        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * RayDistance);
    }

    bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(groundRay, out hit, RayDistance)) // cast a line beneath th player
        {
            return true; // is grounded so return true
        }
        return false; // is not grounded so return false
    }


    // Use this for initialization 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal") * moveSpeed;
        float inputV = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 moveDir = new Vector3(inputH, 0f, inputV);
        Vector3 camEuler = Camera.main.transform.eulerAngles;
        if (rotateToMainCamera)
        {
            moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;
        }


        Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

        Quaternion PlayerRotation = Quaternion.AngleAxis(camEuler.y, Vector3.up);

        if (Input.GetButton("Jump") && IsGrounded())
        {
            force.y = jumpheight;
        }

        rigid.velocity = force;

        //  if (moveDir.magnitude > 0)
        // {
        //   transform.rotation = Quaternion.LookRotation(moveDir);
        // }

        
        Quaternion playerRotation = Quaternion.AngleAxis(camEuler.y, Vector3.up);
        transform.rotation = PlayerRotation;
        Quaternion weaponrotation = Quaternion.AngleAxis(camEuler.x, Vector3.right);
       // weapon.localRotation = weaponrotation;
        transform.rotation = playerRotation;
        
    } 

}

/*
 *     {
        // check if W key is pressed
        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(Vector3.forward * movespeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(Vector3.back * movespeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(Vector3.left * movespeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(Vector3.right * movespeed);
        }


        //if spacebar is pressed, jump up
        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rigid.AddForce(Vector3.up * jumpheight, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    */
