using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject Bullet;
    public KeyCode fireButton;
    public GameObject SpawnPoint;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(fireButton))
        {
            GameObject clone = Instantiate(Bullet, transform.position, transform.rotation);
            Bullet NewBullet = clone.GetComponent<Bullet>();
            NewBullet.Fire(transform.forward);
        }
    }
}
