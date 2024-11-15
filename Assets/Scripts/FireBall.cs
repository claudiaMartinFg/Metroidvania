using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    [SerializeField] float damageFireBall;
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemigo")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damageFireBall); 
        }

        if(collision.tag != "Player")
        {
            anim.SetTrigger("hit");
            Invoke("DestroyFireball", 0.33f);
        }

    }

    private void DestroyFireball()
    {
        Destroy(gameObject);
    }

}
