using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybind : MonoBehaviour
{
    public int keyBindInit;
    public KeyCode ability1;
    public KeyCode ability2;
    public KeyCode ability3;
    public KeyCode ability4;

    public KeyCode holdPosition;

    public KeyCode pause;
    public KeyCode settings;

    // Start is called before the first frame update
    void Start()
    {
        keyBindInit = PlayerPrefs.GetInt("keyBindInit", 0);

        if (keyBindInit == 0)
        {
            Reset();
        }

        ability1 = (UnityEngine.KeyCode)PlayerPrefs.GetInt("ability1");
        ability2 = (UnityEngine.KeyCode)PlayerPrefs.GetInt("ability2");
        ability3 = (UnityEngine.KeyCode)PlayerPrefs.GetInt("ability3");
        ability4 = (UnityEngine.KeyCode)PlayerPrefs.GetInt("ability4");

        holdPosition = (UnityEngine.KeyCode)PlayerPrefs.GetInt("holdPosition");

        pause = (UnityEngine.KeyCode)PlayerPrefs.GetInt("pause");
        settings = (UnityEngine.KeyCode)PlayerPrefs.GetInt("settings");
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("ability1", System.Convert.ToInt32(KeyCode.Q));
        PlayerPrefs.SetInt("ability2", System.Convert.ToInt32(KeyCode.W));
        PlayerPrefs.SetInt("ability3", System.Convert.ToInt32(KeyCode.E));
        PlayerPrefs.SetInt("ability4", System.Convert.ToInt32(KeyCode.R));

        PlayerPrefs.SetInt("holdPosition", System.Convert.ToInt32(KeyCode.S));

        PlayerPrefs.SetInt("pause", System.Convert.ToInt32(KeyCode.P));
        PlayerPrefs.SetInt("settings", System.Convert.ToInt32(KeyCode.Escape));
    }
}
