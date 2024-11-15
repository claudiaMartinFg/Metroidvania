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
                break;

            case "EarthRune":
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
                    if (GameManager.instance.gameData.FireRune != true)
                    {
                        GameManager.instance.gameData.FireRune = true;
                    }
                    break;

                case "AirRune":
                    break;

                case "EarthRune":
                    break;
            }

        }
    }
}
