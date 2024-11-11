using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameData gameData;

    public int nextSpawnPoint;

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

        LoadData(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameData.PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            SaveData(0);
            Debug.Log("He guardado");
        }


        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Se han borrado los datos guardados.");
        }
    }

    public void SaveData(int saveSlot)
    {
        string data = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("gameData" + saveSlot, data);
        PlayerPrefs.Save();
    }

    public void LoadData(int saveSlot)
    {
        if (PlayerPrefs.HasKey("gameData" + saveSlot))
        {
            string data = PlayerPrefs.GetString("gameData" + saveSlot);
            gameData = JsonUtility.FromJson<GameData>(data);
        }
        else
        {
            gameData = new GameData();
            gameData.Life = 100;
            gameData.MaxLife = 100;
            gameData.PlayerPos = new Vector3(-4.18f, -1.98f, 0);
            gameData.SaveSlot = saveSlot;
        }
    }
}
