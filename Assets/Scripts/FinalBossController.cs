using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    public enum bossStates { Idle, Rugido, Roll, Spines, Jump, Walk, Hit, Death }
    [SerializeField] private bossStates state;

    private Animator anim;
    public bool isWaiting;
    [SerializeField] Transform player;
    [SerializeField] float stopDistance;

    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed;
    [SerializeField] private int knockback;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] private float damage;

    [Header("Roar")]
    [SerializeField] private Transform roarSpawn;
    [SerializeField] private GameObject roarProyectil;
    [SerializeField] float projectileSpeed;
    [SerializeField] float secondsToWaitToShootRoarAgain;

    [Header ("Roll")]
    [SerializeField] float rollSpeed;
    [SerializeField] private float stopRollTime;
    [SerializeField] private bool isColisionado;

    [Header("Spikes")]
    [SerializeField] private GameObject spikeProjectile;
    [SerializeField] private Transform spikeSpawnPoint;
    [SerializeField] private float spikeSpeed;
    [SerializeField] private float tiredCooldown;
    private float tiredTime;
    private bool isHit;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpSpeed;
    private bool isJumping = false;

    [Header("Knockback")]
    [SerializeField] private int knockbackForce;

    void Start()
    {
        state = bossStates.Idle;
        anim = GetComponent<Animator>();
        isWaiting = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        ChangeState(bossStates.Idle);

    }
    public void ChangeState(bossStates _state)
    {
        state = _state;
        switch (state)
        {
            case bossStates.Idle:
                StartCoroutine(Idle());
                break;

            case bossStates.Rugido:
                StartCoroutine(Roar());
                break;

            case bossStates.Roll:
                StartCoroutine(Roll());
                break;

            case bossStates.Spines:

                StartCoroutine(Spines());

                break;

            case bossStates.Jump:
                //anim
                //desplazar al enemigo a otro sitio
                //termina el estado cuando toque el suelo

                break;

            case bossStates.Walk:
                StartCoroutine(Walk());

                break;


            default:

                break;
                    
            }
    }

    IEnumerator Idle()
    {
        while (isWaiting) {

            yield return null;
        }

        ChangeState(bossStates.Walk);
    }

    IEnumerator Walk()
    {
        anim.SetBool("isWalking", true);

        Vector2 distanceVector = player.position - transform.position;
        Vector2 direccionPlayer = distanceVector.normalized;
        float distancia = distanceVector.magnitude;
        
        while (distancia > stopDistance) {

            transform.Translate(new Vector2(walkSpeed * direccionPlayer.x, 0)*Time.deltaTime,Space.World);
            if (direccionPlayer.x > 0)
            {
                transform.eulerAngles = new Vector3(0,180,0);
            }
            else 
            {
                transform.eulerAngles = Vector3.zero;
            }

            distanceVector = player.position - transform.position;
            direccionPlayer = distanceVector.normalized;
            distancia = distanceVector.magnitude;
            yield return null;
        }

       rb.velocity = Vector2.zero;
       anim.SetBool("isWalking", false);

       int numRandState = Random.Range(1, 4);
        // ChangeState((bossStates)numRandState);
        ChangeState((bossStates.Roll));
    }

    IEnumerator Roar()
    {
        anim.SetTrigger("Roar");
        yield return new WaitForSeconds(secondsToWaitToShootRoarAgain);
    }
    IEnumerator Roll()
    {
        isColisionado = false;
        anim.SetTrigger("Roll");
        yield return new WaitForSeconds(1.2f);
        while (!isColisionado)
        {
            transform.Translate(transform.right * -1 * rollSpeed * Time.deltaTime,Space.World);
            //rb.velocity = transform.right * -1 * rollSpeed;    
            yield return null;
        }

        yield return new WaitForSeconds(stopRollTime);

        int numRandState = Random.Range(1, 6);
        ChangeState((bossStates)numRandState);
    }

    public void StartRoll()
    {
        transform.Translate(transform.right * -1 * rollSpeed, Space.World);
       // rb.velocity = transform.right * -1 * rollSpeed;
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.95f, 0.95f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ParedColision")
        {
            isColisionado = true;
            rb.velocity = Vector2.zero;
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1.37f, 0.95f);
            anim.SetTrigger("Colisionado");
        }

         if(collision.gameObject.tag == "Player")
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

   /* pruebas  
    * private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        }
    }*/

    public void ShootRoarProyectile()
    {
        GameObject clone = Instantiate(roarProyectil, roarSpawn.position, roarSpawn.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * -1 * projectileSpeed);
    }

    void AddKnockBackForceToPlayer()
    {
        player.GetComponent<Rigidbody2D>().AddForce(transform.right * -1 * knockbackForce);
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;

        if (health <= 0)
        {
            anim.SetTrigger("Dead");
            StopAllCoroutines();
            GetComponent<CapsuleCollider2D>().enabled = false;
            rb.isKinematic = true;
        }
        else
        {
            StartCoroutine(ChangeColorHit());

        }
    }


    IEnumerator ChangeColorHit()
    {
        Color colorInicial = Color.white;  
        Color colorFinal = Color.red;
        float t = 0;
        SpriteRenderer bossSprite = GetComponent<SpriteRenderer>();

        while (t < 1)
        {
            bossSprite.color = Color.Lerp(colorInicial, colorFinal, t);
            t += Time.deltaTime*3f;
            yield return null;
        }

        while (t > 0)
        {
            bossSprite.color = Color.Lerp(colorInicial, colorFinal, t);
            t -= Time.deltaTime * 3f;
            yield return null;
        }
        yield return null;
    }

    IEnumerator Spines()
    {
        tiredTime = 0;
        isHit = false;
        anim.SetTrigger("Spike");
        while(tiredCooldown > tiredTime && !isHit)
        {
            tiredTime += Time.deltaTime;
            yield return null;  
        }
    }

    public void LaunchSpikes()
    {
        int numberOfSpikes = 10;
        float angleStep = 180f / numberOfSpikes;
        float currentAngle = 0f;

        for (int i = 0; i < numberOfSpikes; i++)
        {
            GameObject spikeClone = Instantiate(spikeProjectile, spikeSpawnPoint.position, spikeSpawnPoint.rotation);

            float angleInRadians = currentAngle * Mathf.Deg2Rad;
            Vector2 launchDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            Rigidbody2D rb = spikeClone.GetComponent<Rigidbody2D>();
            rb.velocity = launchDirection * spikeSpeed;

            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

            //codigo profe en el PlayerDamage para que las spines sigan la direccion de su velocidad. Aqui cambiado 
            spikeClone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

            currentAngle += angleStep;
        }

        Invoke("GotTired", 0.5f);
    }


    private void GotTired()
    {
        anim.SetTrigger("Tired");
    }

}
