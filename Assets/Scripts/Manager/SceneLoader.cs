using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 0.5f;

    public void StartGame()
    {
        if (!GameManager.Instance.IsTransitioning())
            StartCoroutine(LoadLevel("Tutorial 1", GameManager.GameState.PLAYING));
    }
    public void LoadNextLevel()
    {
        if (!GameManager.Instance.IsTransitioning())
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1, GameManager.GameState.PLAYING));
    }

    public void ReturnToMenu()
    {
        if (!GameManager.Instance.IsTransitioning())StartCoroutine(LoadLevel("Menu", GameManager.GameState.MAIN_MENU));
    }    

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex, GameManager.GameState nextState)
    {
        transition.SetTrigger("Next Scene");
        GameManager.Instance.gameState = GameManager.GameState.TRANSITION;

        bool sceneLoaded = false;
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == levelIndex)
            {
                sceneLoaded = true;
            }
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

        yield return new WaitForSecondsRealtime(transitionTime);

        UIManager.Instance.HideUI();
        GameManager.Instance.UnfreezeGame();
        transition.SetTrigger("Load Scene");
        SceneManager.LoadScene(levelIndex);

        while (!sceneLoaded)
        {
            yield return null;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;

        UIManager.Instance.showTimer();
        if (nextState != GameManager.GameState.MAIN_MENU)
            UIManager.Instance.resetTimer();


        GameManager.Instance.gameState = nextState;
    }

    IEnumerator LoadLevel(string levelName, GameManager.GameState nextState)
    {
        transition.SetTrigger("Next Scene");
        GameManager.Instance.gameState = GameManager.GameState.TRANSITION;

        bool sceneLoaded = false;
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == levelName)
            {
                sceneLoaded = true;
            }
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

        yield return new WaitForSecondsRealtime(transitionTime);

        UIManager.Instance.HideUI();
        GameManager.Instance.UnfreezeGame();
        transition.SetTrigger("Load Scene");
        SceneManager.LoadScene(levelName);

        while (!sceneLoaded)
        {
            yield return null;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;

        
        UIManager.Instance.resetTimer();
        if (nextState != GameManager.GameState.MAIN_MENU)
            UIManager.Instance.showTimer();

        GameManager.Instance.gameState = nextState;
    }
}
