using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Transform roarSpawn;
    [SerializeField] private GameObject roarProyectil;

    [SerializeField] float projectileSpeed;
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
                //anim quieto

                StartCoroutine(Idle());
                break;

            case bossStates.Rugido:
                //anim
                //lance algo
                //acabar estado cuando acabe la animacion
                break;

            case bossStates.Roll:
                //anim
                //moverlo mientras rueda
                //cuando impacte contra la pared, cambiar de estado
                break;

            case bossStates.Spines:
                //anim
                //instanciamos las espinas
                //que se quede unos segundos anim tired
                //despues de ese tiempo termina el estado
                break;

            case bossStates.Jump:
                //anim
                //desplazar al enemigo a otro sitio
                //termina el estado cuando toque el suelo

                break;

            case bossStates.Walk:
                //anim
                //mover hacia el jugador
                //termina el estado a x distancia del player
                StartCoroutine(Walk());

                break;


            case bossStates.Death:
                //anim
                // desactivamos que no pueda hacer nada mas y se quede muerto
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
            
            rb.velocity = new Vector2(walkSpeed * direccionPlayer.x, rb.velocity.y);
            distanceVector = player.position - transform.position;
            direccionPlayer = distanceVector.normalized;
            distancia = distanceVector.magnitude;
            yield return null;

        }

        rb.velocity = Vector2.zero;
       int numRandState = Random.Range(1, 4);
       ChangeState((bossStates)numRandState);

    }

    IEnumerator Roar()
    {
        anim.SetTrigger("Roar");

        yield return null;
    }

    public void ShootRoarProyectile()
    {

        GameObject clone = Instantiate(roarProyectil, roarSpawn.position, roarSpawn.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * -1 * projectileSpeed);
            
    }

}
