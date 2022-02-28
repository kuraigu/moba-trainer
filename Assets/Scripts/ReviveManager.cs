using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveManager : MonoBehaviour
{
    private Preferences _preferences;
    private GameObject _reviveButton;

    private int _reviveValue;

    // Start is called before the first frame update
    void Start()
    {
        _preferences = Preferences.GetInstance();

        _reviveButton = GameObject.Find("Revive Button");

        _reviveValue = PlayerPrefs.GetInt(_preferences.revive);

        if (_reviveValue == 1)
        {
            _reviveButton.SetActive(false);
        }

        else _reviveButton.SetActive(true);
    }
}
