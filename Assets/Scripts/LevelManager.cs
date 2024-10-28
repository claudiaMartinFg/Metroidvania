using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private Image lifeBar;

    public void UpdateLife()
    {
        lifeBar.fillAmount = GameManager.instance.life / GameManager.instance.maxLife;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLife();
    }

}
