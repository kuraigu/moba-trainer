using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static Resources rss;

    [SerializeField] private GameObject _parent;
    [SerializeField] private Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        rss = GameObject.Find("Game Script").GetComponent<Resources>();
        enemy.target = rss.player;
        enemy.parent = _parent;
        enemy.Start();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.Update();
    }

    void FixedUpdate()
    {
        enemy.FixedUpdate();
    }
}
