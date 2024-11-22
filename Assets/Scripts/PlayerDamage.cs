using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    [SerializeField] private float damage;

    [SerializeField] private bool proyectil;
       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        if (proyectil == true)
        {
            Destroy(gameObject);
        }

        if (collision.tag == "ParedColision")
        {
            Destroy(gameObject);
        }

    }
}
