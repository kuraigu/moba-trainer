using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static Resources rss;
    private static ColorScheme colorScheme;

    // Start is called before the first frame update
    void Start()
    {
        rss = FindObjectOfType<Resources>();
        colorScheme = FindObjectOfType<ColorScheme>();
        rss.heroesMisc.SetActive(true);
        rss.player = Instantiate(rss.heroesMisc.transform.Find("Aphelios").gameObject);
        rss.player.transform.SetParent(rss.displayCanvas.transform, false);
        rss.player.transform.localPosition = Vector3.zero;
        rss.player.tag = "Player";
        rss.player.GetComponent<SpriteRenderer>().material.SetColor("_Color", colorScheme.player);
        rss.heroesMisc.SetActive(false);
        rss.player.GetComponent<HeroManager>().isPlayer = true;

    }

    // Update is called once per frame
    void Update()
    {
    }
}
