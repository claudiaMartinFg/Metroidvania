using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject partidaPanel;

    public void PlayButton()
    {
        partidaPanel.SetActive(true);
        partidaPanel.transform.GetChild(1).GetComponent<Button>().Select();
        RevisarPartidas();
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        partidaPanel.SetActive(false);
        GameObject.Find("Play").GetComponent<Button>().Select();
    }

    public void RevisarPartidas()
    {
        for(int i=0; i < 3; i++){

            if (PlayerPrefs.HasKey("gameData" + i.ToString()))
            {
                GameManager.instance.LoadData(i);

                partidaPanel.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    "Partida" + i + "\n Vida" + GameManager.instance.gameData.Life;
            }
            else
            {
                partidaPanel.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Partida Vacía";

            }
        }

    }

    void StartGame()
    {


    }
}
