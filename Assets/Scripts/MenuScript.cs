using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private Button startGame;
    [SerializeField]
    private Button quitGame;

    void Start()
    {
        startGame.onClick.AddListener(UIManager.Instance.sceneLoader.StartGame);
        quitGame.onClick.AddListener(UIManager.Instance.sceneLoader.QuitGame);
    }
}
