using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private float horizontal;
    private bool jump;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator;

    private bool isGrounded;
    public bool isAttacking;

    private LevelManager levelManager;

    [Header("Fireball")]
    [SerializeField] GameObject fireball;
    [SerializeField] Transform spawnFire;
    [SerializeField] float freqFire;
    [SerializeField] float manaCost;
    [SerializeField] float speedFire;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {

        if (!isAttacking)
        {
            horizontal = Input.GetAxis("Horizontal");

            if (horizontal > 0)
            {
                transform.eulerAngles = Vector3.zero;
                animator.SetBool("isRunning", true);
            }
            else if (horizontal < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            horizontal = 0;
        }
       

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1")==true && rb.velocity.y==0)
        {

            animator.SetTrigger("isAttacking");
            isAttacking = true;
        }

        if (Input.GetButtonDown("Fire2") == true)
        {
            Fireball();

        }
    }

    
    private void FixedUpdate()
    {

        if (!isAttacking)
        {
            rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);

            if (jump)
            {
                if (isGrounded)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    animator.SetBool("isJumping", true);
                    isGrounded = false;
                }
                jump = false;
            }
        }

        if (rb.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemigo":

                Debug.Log("he tocado al enemigo");

                break;
        }

    }

    public void TakeDamage(float _damage)
    {
        GameManager.instance.gameData.Life -= _damage;
        levelManager.UpdateLife();

        if (GameManager.instance.gameData.Life <= 0)
        {
            //muerte
            animator.SetTrigger("dead");
            this.enabled = false;
        }
        else
        {
            //hit
            animator.SetTrigger("hit");
        }
    }

    public void Fireball()
    {
        if (GameManager.instance.gameData.Mana >= manaCost)
        {
            GameObject fireballClone = Instantiate(fireball, spawnFire.position, Quaternion.identity);
            GameManager.instance.gameData.Mana -= manaCost;

            float direction = transform.localScale.y > 0 ? 1 : -1;

            fireballClone.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speedFire, 0f);
        }
    }


}

