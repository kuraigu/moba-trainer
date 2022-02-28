using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private Preferences _preferences;
    // Start is called before the first frame update
    public string currentScene;

    public string currentSubScene;

    public string mainMenu;
    public string skillShot;
    public string dodging;
    public string farming;
    public string gameOver;

    public string normal;
    public string settings;
    public string paused;

    void Start()
    {
        _preferences = Preferences.GetInstance();

        UnityEngine.PlayerPrefs.GetString(_preferences.lastScene, "None");

        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        mainMenu = "Main Menu";
        skillShot = "Skillshot";
        dodging = "Dodging";
        farming = "Farming";
        gameOver = "Game Over";

        normal = "Normal";
    }

    public void MainMenu()
    {
        UnityEngine.PlayerPrefs.SetString(_preferences.lastScene, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(mainMenu);
    }

    public void SkillShot()
    {
        UnityEngine.PlayerPrefs.SetString(_preferences.lastScene, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(skillShot);
    }

    public void Dodging()
    {
        UnityEngine.PlayerPrefs.SetString(_preferences.lastScene, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(dodging);
    }

    public void Farming()
    {
        UnityEngine.PlayerPrefs.SetString(_preferences.lastScene, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(farming);
    }

    public void GameOver()
    {
        UnityEngine.PlayerPrefs.SetString(_preferences.lastScene, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        PlayerPrefs.SetInt("score", GameObject.Find("Score").GetComponent<Score>().score);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(gameOver);
    }

    public void Revive()
    {
        UnityEngine.PlayerPrefs.SetInt(_preferences.revive, 1);

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.PlayerPrefs.GetString(_preferences.lastScene));
    }

    public void ExitGame()
    {
        UnityEngine.Application.Quit();
    }
}
