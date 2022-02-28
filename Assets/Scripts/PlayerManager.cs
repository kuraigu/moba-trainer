using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static Resources _rss;
    private static ColorScheme _colorScheme;

    // Start is called before the first frame update
    void Start()
    {
        _rss = FindObjectOfType<Resources>();
        _colorScheme = FindObjectOfType<ColorScheme>();
        _rss.heroesMisc.SetActive(true);
        _rss.player = Instantiate(_rss.heroesMisc.transform.Find("Aphelios").gameObject);
        _rss.player.transform.SetParent(_rss.displayCanvas.transform, false);
        _rss.player.transform.localPosition = Vector3.zero;
        _rss.player.tag = "Player";
        _rss.player.GetComponent<SpriteRenderer>().material.SetColor("_Color", _colorScheme.player);
        _rss.heroesMisc.SetActive(false);
        _rss.player.GetComponent<HeroManager>().isPlayer = true;

    }
}
