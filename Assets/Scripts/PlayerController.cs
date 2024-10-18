using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private float horizontal;
    private bool jump;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        // transform.Translate(Vector3.right * horizontal * speed* Time.deltaTime);

        if (horizontal > 0) 
        {
            transform.eulerAngles = Vector3.zero;
            animator.SetBool("isRunning",true);
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

        if (Input.GetButtonDown("Jump"))
        {
           jump = true;
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);

        if (jump == true && rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * jumpForce);
            animator.SetBool("isJumping", true);

        }
        else if(rb.velocity.y == 0)
        {
            jump = false;
            animator.SetBool("isJumping", false);
        }

    }
}
