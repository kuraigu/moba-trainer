using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static Resources rss;
    private static SceneManager scene;
    private List<GameObject> _enemy;

    private FreeMatrix.Utility.Time spawnTime;

    private int maxEnemy;
    private int currentEnemy;
    private int incrementEnemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        rss = FindObjectOfType<Resources>();
        scene = FindObjectOfType<SceneManager>();
        _enemy = new List<GameObject>();

        spawnTime = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 3);

        maxEnemy = 10;
        currentEnemy = 3;
        incrementEnemyCounter = 0;

        for (int i = 0; i < currentEnemy; i++)
        {
            _enemy = Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime.Update())
        {

            for (int i = 0; i < currentEnemy - _enemy.Count; i++)
            {
                _enemy = Spawn();
            }

            incrementEnemyCounter++;

            if (incrementEnemyCounter >= 2)
            {
                if (currentEnemy < maxEnemy)
                {
                    incrementEnemyCounter = 0;
                    currentEnemy++;
                }
            }
        }

        if (enemy.Count > 0)
        {
            for (int i = 0; i <= 0; i++)
            {
                if (enemy[i] == null)
                {
                    enemy.RemoveAt(i);
                }
            }
        }
    }

    private Vector2 GenerateSpawn(Vector2 newEnemyPos)
    {
        Vector3 cameraScale = Vector3.zero;
        cameraScale.y = (Camera.main.orthographicSize * 2) * 100;
        cameraScale.x = (Camera.main.aspect * cameraScale.y);
        Vector3 cameraPos = Camera.main.transform.localPosition * 100;

        cameraScale = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale, cameraScale);
        cameraPos = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale, cameraPos);

        int side = Random.Range(0, 4);
        side = Random.Range(0, 4);
        side = Random.Range(0, 4);
        side = Random.Range(0, 4);

        // Top
        if (side == 0)
        {
            newEnemyPos.y = cameraPos.y + (cameraScale.y / 2) + 50;
            newEnemyPos.x = UnityEngine.Random.Range(cameraPos.x + -(cameraScale.x / 2), cameraPos.x + (cameraScale.x / 2));
        }

        if (side == 1)
        {
            newEnemyPos.y = cameraPos.y - (cameraScale.y / 2) + -50;
            newEnemyPos.x = UnityEngine.Random.Range(cameraPos.x + -(cameraScale.x / 2), cameraPos.x + (cameraScale.x / 2));
        }


        // Left
        if (side == 2)
        {
            newEnemyPos.x = cameraPos.x + (cameraScale.x / 2) + 50;
            newEnemyPos.y = UnityEngine.Random.Range(cameraPos.y + -(cameraScale.y / 2), cameraPos.y + (cameraScale.y / 2));
        }

        // Right
        if (side == 3)
        {
            newEnemyPos.x = cameraPos.x + -(cameraScale.x / 2) + -50;
            newEnemyPos.y = UnityEngine.Random.Range(cameraPos.y + -(cameraScale.y / 2), cameraPos.y + (cameraScale.y / 2));
        }

        return newEnemyPos;
    }

    private GameObject InitEnemy(GameObject enemy)
    {
        enemy.transform.SetParent(rss.displayCanvas.transform, false);
        enemy.GetComponent<HeroManager>().isPlayer = false;
        enemy.AddComponent<EnemyController>();


        Vector3 newEnemyPos = Vector3.zero;

        newEnemyPos = GenerateSpawn(newEnemyPos);

        enemy.transform.localPosition = newEnemyPos;
        enemy.GetComponent<EnemyController>().target = rss.player;
        enemy.GetComponent<EnemyController>().origin = newEnemyPos;
        enemy.transform.tag = "Enemy";

        return enemy;
    }

    private List<GameObject> Spawn()
    {
        rss.heroesMisc.SetActive(true);
        _enemy.Add(Instantiate(rss.heroesMisc.transform.Find("Aphelios").gameObject));
        _enemy[_enemy.Count - 1] = InitEnemy(_enemy[_enemy.Count - 1]);

        Vector3 cameraScale = Vector3.zero;
        cameraScale.y = (Camera.main.orthographicSize * 2) * 100;
        cameraScale.x = (Camera.main.aspect * cameraScale.y);
        Vector3 cameraPos = Camera.main.transform.localPosition * 100;

        cameraScale = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale, cameraScale);
        cameraPos = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale, cameraPos);

        Vector3 newEnemyPos = Vector3.zero;


        Vector2 destination;
        destination.x = UnityEngine.Random.Range(cameraPos.x + -(cameraScale.x / 2), cameraPos.x + (cameraScale.x / 2));
        destination.y = UnityEngine.Random.Range(cameraPos.y + -(cameraScale.y / 2), cameraPos.y + (cameraScale.y / 2));
        _enemy[_enemy.Count - 1].GetComponent<EnemyController>().destination = destination;

        rss.heroesMisc.SetActive(false);

        return _enemy;
    }

    public List<GameObject> enemy { get { return _enemy; } set { _enemy = value; } }
}
