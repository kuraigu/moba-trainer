using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences
{
    // Scene
    public string lastScene;

    public string revive;

    public string score;

    // Keybinds
    public string ability1;
    public string ability2;
    public string ability3;
    public string ability4;

    public string holdPosition;

    public string pause;
    public string settings;

    // Start is called before the first frame update
    private Preferences()
    {

        lastScene = "lastScene";

        revive = "revive";

        score = "score";

        ability1 = "ability1";
        ability2 = "ability2";
        ability3 = "ability3";
        ability4 = "ability4";

        holdPosition = "holdPosition";

        pause = "pause";
        settings = "settings";
    }

    private static Preferences _instance;

    public static Preferences GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Preferences();
        }

        return _instance;
    }
}
