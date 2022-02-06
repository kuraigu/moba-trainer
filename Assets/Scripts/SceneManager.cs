using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static string gameOver;


    void Start()
    {
        gameOver = "Game Over";
    }

    public void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(gameOver);
    }
}
