using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStone : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("SaveGlow",true);
        }
        
    }
}
