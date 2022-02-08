using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private static Resources rss;
    private string currentScene;

    private int _oldScore;
    private int _score;

    void Start()
    {
        rss = GameObject.Find("Game Script").GetComponent<Resources>();

        _oldScore = 0;
        _score = 0;

        if (rss.sceneManager.currentScene == rss.sceneManager.gameOver)
        {
            _score = PlayerPrefs.GetInt("score");
        }
    }

    void Update()
    {

        if (_score > _oldScore)
        {
            rss.scoreText.GetComponent<TextMeshProUGUI>().text = _score.ToString();
            _oldScore = _score;
        }
    }

    public int score { get { return _score; } set { _score = value; } }
}

