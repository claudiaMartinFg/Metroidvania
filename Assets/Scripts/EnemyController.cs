using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float health;

    [SerializeField] private float speed;

    public bool playerDetection;

    [SerializeField] private float attackRate;

    [SerializeField] private float damagingRate;

    [SerializeField] private float damage;


    [SerializeField] private Animator animator;

    private Transform player;

    [SerializeField] private float stopDistance;
    [SerializeField] private Rigidbody2D rb;

    private bool isAttacking;
    private bool isHit;


    private float timePass;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (playerDetection && GameManager.instance.gameData.Life > 0)
        {
            Vector3 vectorDistancia = player.transform.position - transform.position;
            Vector3 direccion = vectorDistancia.normalized;
            float moduloDistancia = vectorDistancia.magnitude;

            if (vectorDistancia.x > 0)
            {
                transform.eulerAngles = new Vector3(0f,180f,0f);

            }
            else{

                transform.eulerAngles = Vector3.zero;
            }
            if (stopDistance <= moduloDistancia)
            {

                rb.velocity = new Vector2(speed * direccion.x, rb.velocity.y);
                animator.SetBool("walk", true);
            }
            else
            {
                if (isHit == false){

                    rb.velocity = new Vector3(0, rb.velocity.y);
                    animator.SetBool("walk", false);
                    Attack();
                }
            }
        }

        timePass += Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.transform;
            playerDetection = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isAttacking==false)
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
            playerDetection = false;
            animator.SetBool("walk", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            gameObject.layer = 8;
            Invoke("RestoreCollision", 1.5f);
        }
    }

    private void RestoreCollision()
    {
        gameObject.layer = 0;
    }

private void Attack()
    {
        if (timePass>=attackRate)
        {
            timePass = 0;
            animator.SetTrigger("attack");
            isAttacking = true;
        }
    }


    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void SetIsHitFalse()
    {
        isHit = false;
    }

    public void IsNotAttacking()
    {
        isAttacking = false;
    }

    private void Die()
    {
        animator.SetTrigger("dead");
        Invoke("DestroyEnemy", 0.7f);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }
}
