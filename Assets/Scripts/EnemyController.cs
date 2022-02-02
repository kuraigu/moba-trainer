using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static Resources rss;

    private List<GameObject> enemy;
    private GameObject enemyCanvas;

    private FreeMatrix.Utility.Time enemySpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        rss = FindObjectOfType<Resources>();

        enemySpawnTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 6);
        enemy = new List<GameObject>();
        enemy.Add(Instantiate(GameObject.Find("Enemies").transform.Find("Enemy Minion").gameObject));
        enemy[enemy.Count - 1].transform.SetParent(rss.displayCanvas.transform, false);
        enemy[enemy.Count - 1].transform.localPosition = new Vector3(500, 500, 0);

        enemyCanvas = GameObject.Find("Enemies");
        enemyCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawnTimer.Update())
        {
            enemySpawnTimer.scale += 0.001f;

            enemyCanvas.SetActive(true);
            enemy.Add(Instantiate(enemyCanvas.transform.Find("Enemy Minion").gameObject));
            enemy[enemy.Count - 1].transform.SetParent(rss.displayCanvas.transform, false);
            enemy[enemy.Count - 1].transform.localPosition = new Vector3(500, 500, 0);

            enemyCanvas.SetActive(false);

        }
    }
}
