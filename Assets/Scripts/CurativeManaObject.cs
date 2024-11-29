using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurativeManaObject : MonoBehaviour
{
    [SerializeField] float manaToGain;
    private SpriteRenderer spriteRenderer;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //cuando el objeto toca al player le sumamos 20 de vida al player
            GameManager.instance.gameData.Mana += manaToGain;

            //se "apaga" el objeto visualmente
            spriteRenderer.color = Color.grey;
            levelManager.UpdateLife();
        }
    }

}
