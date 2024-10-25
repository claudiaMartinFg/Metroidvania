using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float life;
    public float maxLife;
    public float playerLife;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerLife = maxLife; 
    }
}
