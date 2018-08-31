using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 50;
    public float speed = 5f;
    public Rigidbody rigid;

    // Use this for initialization
    public void Fire(Vector3 direction)
    {
        rigid.AddForce(direction * speed, ForceMode.Impulse);
    }
    // Update is called once per frame
    void OnTrggerEnter (Collider other)
    {
        //Enemy enemy = other.GetComponent<Enemy>();

        //if(enemy)
        //{
        //    enemy.DealDamage(damage);

        //    Destroy(gameObject);
        //}
    }
}