using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScheme : MonoBehaviour
{
    public Color32 player;
    public Color32 ally;
    public Color32 enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = new Color32(150, 184, 204, 100);
        enemy = new Color32(219, 124, 112, 100);
    }
}
