using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] string runeName;

    private void Start()
    {
        switch (runeName)
        {
            case "FireRune":
                if(GameManager.instance.gameData.FireRune == true)
                {
                    Destroy(gameObject);
                }
                break;

            case "AirRune":
                if (GameManager.instance.gameData.AirRune > 2)
                {
                    Destroy(gameObject);
                }
                break;

            case "EarthRune":
                if (GameManager.instance.gameData.EarthRune == true)
                {
                    Destroy(gameObject);
                }

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (runeName)
            {
                case "FireRune":
                    GameManager.instance.gameData.FireRune = true;
                    Destroy(gameObject);
                    break;

                case "AirRune":
                    GameManager.instance.gameData.AirRune = 1;
                    Destroy (gameObject);
                    break;

                case "EarthRune":
                    GameManager.instance.gameData.EarthRune = true;
                    Destroy(gameObject);
                    break;
            }

        }
    }
}
