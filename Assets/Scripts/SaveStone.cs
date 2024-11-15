using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStone : MonoBehaviour
{

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("SaveGlow",true);
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("SaveGlow", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetAxis("Vertical") >= 0.1f)
        {
            GameManager.instance.SaveData(GameManager.instance.gameData.SaveSlot);
            animator.SetBool("SaveGlow",false);
            Destroy(this);
        }
    }

}
