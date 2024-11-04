using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameData gameData;       
    //public float playerLife;

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

        //Temporal
        LoadData();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            gameData.PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            SaveData();
            Debug.Log("He guardado");
        }

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("gameData", data);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("gameData") == true)
        {
            string data = PlayerPrefs.GetString("gameData");
            gameData = JsonUtility.FromJson<GameData>(data);
        }
        else
        {
            gameData= new GameData();
            gameData.Life = 100;
            gameData.MaxLife = 100;
            gameData.PlayerPos = new Vector3(-4.18f, -1.98f, 0);
        }

    }
}
