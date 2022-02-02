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
        rss.player = Instantiate(GameObject.Find("Heroes").transform.Find("Aphelios").gameObject);
        rss.player.transform.SetParent(rss.displayCanvas.transform, false);
        rss.player.transform.localPosition = Vector3.zero;
        GameObject.Find("Heroes").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
