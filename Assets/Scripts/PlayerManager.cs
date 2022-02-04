using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static Resources rss;

    // Start is called before the first frame update
    void Start()
    {
        rss = FindObjectOfType<Resources>();
        rss.heroesMisc.SetActive(true);
        rss.player = Instantiate(rss.heroesMisc.transform.Find("Aphelios").gameObject);
        rss.player.transform.SetParent(rss.displayCanvas.transform, false);
        rss.player.transform.localPosition = Vector3.zero;
        rss.player.GetComponent<HeroManager>().isPlayer = true;
        rss.heroesMisc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
