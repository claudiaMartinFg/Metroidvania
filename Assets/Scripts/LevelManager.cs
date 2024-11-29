using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private Image lifeBar;

    [SerializeField] private Image manaBar;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private AudioClip levelMusic;

    [SerializeField] private GameObject panelTactil;

    [SerializeField] private GameObject panelPausa;
    private bool isPanelPausa;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnPoints[GameManager.instance.nextSpawnPoint].position;
        GameObject.FindGameObjectWithTag("Player").transform.rotation = spawnPoints[GameManager.instance.nextSpawnPoint].rotation;

        UpdateLife();

        if(levelMusic != null)
        {

        }
    }
#if UNITY_ANDROID
    panelTactil.SetActive(true);
#endif
    public void UpdateLife()
    {
        lifeBar.fillAmount = GameManager.instance.gameData.Life / GameManager.instance.gameData.MaxLife;
        manaBar.fillAmount = GameManager.instance.gameData.Mana / GameManager.instance.gameData.MaxMana;

    }


    //Menu de pausa
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPanelPausa==false)
        {
            Time.timeScale = 0;
            isPanelPausa = true;
            panelPausa.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isPanelPausa == true)
        {
            Time.timeScale = 1;
            isPanelPausa = false;
            panelPausa.SetActive(false);
        }
    }

    public void SaveButton()
    {

    }
    public void SaveExitButton()
    {
       // funcion guardar  + 
        Application.Quit();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
