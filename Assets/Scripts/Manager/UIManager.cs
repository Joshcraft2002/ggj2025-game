using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Singleton instance of the ui manager
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI gameClearTime;
    [SerializeField]
    private TextMeshProUGUI bestClearTime;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    public float timer;
    [SerializeField]
    public float highScore = float.MaxValue;

    public SceneLoader sceneLoader;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject gameWinMenu;
    public GameObject timerObject;
    public GameObject newBestObject;

    private void Awake()
    {
        // Ensure only one instance of the GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGame();
        HideUI();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        //only run in scenes with timer object
        if (timerObject)
            timerText.text = ConvertToTimeFormat(timer);
    }

    public void hideTimer()
    {
        timerObject.SetActive(false);
    }

    public void showTimer()
    {
        timerObject.SetActive(true);
    }

    public void resetTimer()
    {
        timer = 0f;
    }

    public void HideUI()
    {
        timerObject.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameWinMenu.SetActive(false);
        newBestObject.SetActive(false);
    }

    public void GameWin()
    {
        Debug.Log("Level Completed!");
        gameWinMenu.SetActive(true);
        if (timer < highScore)
        {           
            highScore = timer;
            SaveGame();
            newBestObject.SetActive(true);
        }
        gameClearTime.text = ConvertToTimeFormat(timer);
        bestClearTime.text = ConvertToTimeFormat(highScore);
    }

    public string ConvertToTimeFormat(float timer)
    {
        return Mathf.FloorToInt(timer / 60).ToString() + ":" + Mathf.FloorToInt(timer % 60).ToString("D2") + "." + Mathf.FloorToInt((timer * 1000) % 1000).ToString();
    }

    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveData data = new SaveData();
        data.highScore = highScore;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    void LoadGame()
    {
        if (!File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            Debug.Log("No save data found.");
            return;
        }

        BinaryFormatter bf = new();
        FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();
        highScore = data.highScore;
        Debug.Log("Game data loaded!");

    }

    void ResetData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveData data = new SaveData();
        data.highScore = float.MaxValue;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data reset!");
    }
}

[Serializable]
class SaveData
{
    public float highScore;
}
