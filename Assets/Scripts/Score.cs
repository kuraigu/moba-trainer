using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private static Resources _rss;
    private Preferences _preferences;
    private string _currentScene;

    private int _oldScore;
    private int _score;

    void Start()
    {
        _rss = GameObject.Find("Game Script").GetComponent<Resources>();
        _preferences = Preferences.GetInstance();

        _oldScore = 0;
        _score = 0;

        if (_rss.sceneManager.currentScene == _rss.sceneManager.gameOver)
        {
            _score = PlayerPrefs.GetInt("score");
            _rss.scoreText.GetComponent<TextMeshProUGUI>().text += _score.ToString();
        }
    }

    void Update()
    {
        if (_rss.sceneManager.currentScene != _rss.sceneManager.gameOver)
        {
            if (_score > _oldScore)
            {
                _rss.scoreText.GetComponent<TextMeshProUGUI>().text = _score.ToString();
                _oldScore = _score;
            }

        }
    }

    public int score { get { return _score; } set { _score = value; } }
}

