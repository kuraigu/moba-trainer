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
        ability1 = (UnityEngine.KeyCode)PlayerPrefs.GetInt("ability1", System.Convert.ToInt32(KeyCode.Q));
        ability2 = (UnityEngine.KeyCode)PlayerPrefs.GetInt("ability2", System.Convert.ToInt32(KeyCode.W));
        ability3 = (UnityEngine.KeyCode)PlayerPrefs.GetInt("ability3", System.Convert.ToInt32(KeyCode.E));
        ability4 = (UnityEngine.KeyCode)PlayerPrefs.GetInt("ability4", System.Convert.ToInt32(KeyCode.R));

        holdPosition = (UnityEngine.KeyCode)PlayerPrefs.GetInt("holdPosition", System.Convert.ToInt32(KeyCode.S));

        pause = (UnityEngine.KeyCode)PlayerPrefs.GetInt("pause", System.Convert.ToInt32(KeyCode.P));
        settings = (UnityEngine.KeyCode)PlayerPrefs.GetInt("settings", System.Convert.ToInt32(KeyCode.Escape));
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
