using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{
    [SerializeField]
    private string path, fileName;
    [SerializeField]
    private bool endOfArea = false;

    private void Start()
    {
        UIManager.Instance.highScore = GameDataHandler.GetLevelData(path, fileName).highScore;
    }

    public void CompleteLevel()
    {
        GameManager.Instance.gameState = GameManager.GameState.GAME_WIN;
        UIManager.Instance.hideTimer();
        GameManager.Instance.GameFrozen = true;
        UIManager.Instance.CompleteLevel(path, fileName, endOfArea);
    }
}
