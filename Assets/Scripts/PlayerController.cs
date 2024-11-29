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
    //private bool jump;
    private int jumpCount;

    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator;

    private bool isGrounded;
    public bool isAttacking;
    [SerializeField] private float damage;

    private LevelManager levelManager;

    [Header("Fireball")]
    [SerializeField] GameObject fireball;
    [SerializeField] Transform spawnFire;
    [SerializeField] float freqFire;
    [SerializeField] float manaCost;
    [SerializeField] float speedFire;
    [SerializeField] float fireBallTimePass;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        fireBallTimePass += Time.deltaTime;
        if (!isAttacking)
        {
#if UNITY_ANDROID == false
            horizontal = Input.GetAxis("Horizontal");
#endif
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
       

        if (Input.GetButtonDown("Jump") && jumpCount < GameManager.instance.gameData.AirRune)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            jumpCount++;
        }

      /*  if (Input.GetButtonDown("Fire1")==true && rb.velocity.y==0)
        {

            animator.SetTrigger("isAttacking");
            isAttacking = true;
        }*/

        if (Input.GetButtonDown("Fire2") == true && GameManager.instance.gameData.FireRune==true)
        {
            Fireball();
        }
    }

    //Inputs Movil
    public void JumpButton()
    {
        if (jumpCount < GameManager.instance.gameData.AirRune)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            jumpCount++;
            //audio manager
            //AudioManager.instance.PlayerSFX(jumpSfx, 1f);
        }
    }
    public void MoveButtonDown(int _horizontal)
    {
        horizontal = _horizontal;
    }
    public void MoveButtonUp()
    {
        horizontal = 0;
    }


    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
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
            if (collision.GetContact(collision.contactCount - 1).normal.y >= 0.5f)

                animator.SetBool("isJumping", false);
                jumpCount = 0;

            isGrounded = true;
            //animator.SetBool("inWall",false);
            animator.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.GetContact(0).normal.y == 0){

                //animator.SetBool("inWall", true);
                //rb.velocity = new Vector2(rb.velocity);
            }
        }
    }


    void WallJump()
    {
        //cosas comentadas?
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
                try
                {
                    collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
                }
                catch 
                {
                    collision.gameObject.GetComponent<FinalBossController>().TakeDamage(damage);
                }

                break;
        }
    }

    public void IsHit()
    {
        //programar que cuando el boss le ataque se tenga que quedar quieto

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
            levelManager.UpdateLife();
            float direction = transform.localScale.y > 0 ? 1 : -1;

            fireballClone.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * speedFire, 0f);
        }
    }


}

