using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static Resources rss;
    private static EnemyManager enemyManager;
    private List<GameObject> enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = new List<GameObject>();
        rss = FindObjectOfType<Resources>();
        enemyManager = FindObjectOfType<EnemyManager>();

        enemy = enemyManager.enemy;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemy.Count > 0)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween2D.PointTo(rss.player.transform.localPosition, enemy[i].transform.localPosition, enemy[i].transform.localRotation));
                enemy[i].transform.localPosition = Vector2.MoveTowards(enemy[i].transform.localPosition, rss.player.transform.localPosition, (enemy[i].GetComponent<HeroManager>().moveSpeed - 150) * Time.deltaTime);

                if (Vector2.Distance(enemy[i].transform.localPosition, rss.player.transform.localPosition) < 0.1 * Time.deltaTime)
                {
                    Destroy(enemy[i]);
                    enemy.RemoveAt(i);
                }
            }
        }
    }
}
