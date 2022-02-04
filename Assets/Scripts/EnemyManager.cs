using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static Resources rss;
    private List<GameObject> _enemy;

    private FreeMatrix.Utility.Time spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        rss = FindObjectOfType<Resources>();
        _enemy = new List<GameObject>();

        spawnTime = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 5);

        rss.heroesMisc.SetActive(true);
        _enemy.Add(Instantiate(rss.heroesMisc.transform.Find("Aphelios").gameObject));
        _enemy[_enemy.Count - 1].transform.SetParent(rss.displayCanvas.transform, false);
        _enemy[_enemy.Count - 1].GetComponent<HeroManager>().isPlayer = false;
        _enemy[_enemy.Count - 1].transform.localPosition = new Vector3(500, 500, 0);
        rss.heroesMisc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime.Update())
        {
            rss.heroesMisc.SetActive(true);
            _enemy.Add(Instantiate(rss.heroesMisc.transform.Find("Aphelios").gameObject));
            _enemy[_enemy.Count - 1].transform.SetParent(rss.displayCanvas.transform, false);
            _enemy[_enemy.Count - 1].GetComponent<HeroManager>().isPlayer = false;
            _enemy[_enemy.Count - 1].transform.localPosition = new Vector3(500, 500, 0);
            rss.heroesMisc.SetActive(false);

            spawnTime.max -= 0.01f;
        }
    }

    public List<GameObject> enemy { get { return _enemy; } set { _enemy = value; } }
}
