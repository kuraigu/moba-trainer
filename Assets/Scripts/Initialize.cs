using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    private Preferences _preferences;

    // Start is called before the first frame update
    void Start()
    {
        _preferences = Preferences.GetInstance();

        PlayerPrefs.GetInt(_preferences.revive, 0);
        PlayerPrefs.GetInt(_preferences.score, 0);

        PlayerPrefs.SetInt(_preferences.revive, 0);
        PlayerPrefs.SetInt(_preferences.score, 0);
    }
}
