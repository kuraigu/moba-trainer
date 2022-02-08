using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string currentScene;
    public string gameOver;

    void Start()
    {
        gameOver = "Game Over";
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("score", GameObject.Find("Score").GetComponent<Score>().score);
        currentScene = gameOver;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(gameOver);
    }
}
