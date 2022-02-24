using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string currentScene;
    public string mainMenu;
    public string skillShot;
    public string dodging;
    public string farming;
    public string gameOver;

    void Start()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        mainMenu = "Main Menu";
        skillShot = "Skillshot";
        dodging = "Dodging";
        farming = "Farming";
        gameOver = "Game Over";
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(mainMenu);
    }

    public void SkillShot()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(skillShot);
    }

    public void Dodging()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(dodging);
    }

    public void Farming()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(farming);
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("score", GameObject.Find("Score").GetComponent<Score>().score);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(gameOver);
    }

    public void ExitGame()
    {
        UnityEngine.Application.Quit();
    }
}
