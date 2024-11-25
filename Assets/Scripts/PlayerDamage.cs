using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    [SerializeField] private float damage;

    //[SerializeField] private bool proyectil;

    //[SerializeField] private bool doingFall=false;
   // private Rigidbody2D rb;

    private void Start()
    {
        /*if (doingFall)
        {
            rb = GetComponent<Rigidbody2D>();
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }

        /*if (proyectil == true)
        {
            Destroy(gameObject);
        }*/

        if (collision.tag == "ParedColision" || collision.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }

    /*private void Update()
    {
        if (doingFall)
        {
            Vector3 dirToLook = rb.velocity;
            Quaternion rot = Quaternion.LookRotation(dirToLook);
            transform.rotation = rot;
            transform.Rotate(new Vector3(0,90,0),Space.Self);
        }
    }*/
}
