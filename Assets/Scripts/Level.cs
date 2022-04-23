using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private float gameOverDelay = 2.0f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");

        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            gameSession.ResetGame();
        }
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadGameOverDelayed()
    {
        StartCoroutine(WaitAndLoadGameOver(gameOverDelay));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoadGameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadGameOver();
    }
}
