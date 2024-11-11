using System.Collections;
using System.Collections.Generic;
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


    }

    void StartGame()
    {


    }
}
