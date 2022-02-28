using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static Resources _rss;
    private static SceneManager _scene;
    private List<GameObject> _enemy;

    private FreeMatrix.Utility.Time _spawnTime;

    private int _maxEnemy;
    private int _currentEnemy;
    private int _incrementEnemyCounter;
    private int _lastRand;

    // Start is called before the first frame update
    void Start()
    {
        _rss = FindObjectOfType<Resources>();
        _scene = FindObjectOfType<SceneManager>();
        _enemy = new List<GameObject>();

        _spawnTime = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 3);

        _maxEnemy = 10;
        _currentEnemy = 3;
        _incrementEnemyCounter = 0;
        _lastRand = -1;

        for (int i = 0; i < _currentEnemy; i++)
        {
            _enemy = Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnTime.Update())
        {

            for (int i = 0; i < _currentEnemy - _enemy.Count; i++)
            {
                _enemy = Spawn();
            }

            _incrementEnemyCounter++;

            if (_incrementEnemyCounter >= 3)
            {
                if (_currentEnemy < _maxEnemy)
                {
                    _incrementEnemyCounter = 0;
                    _currentEnemy++;
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

        cameraScale = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale, cameraScale);
        cameraPos = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale, cameraPos);

        bool generate = true;
        int side = 0;

        while (generate)
        {
            UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
            side = UnityEngine.Random.Range(0, 4);

            if (side != _lastRand)
            {
                _lastRand = side;
                generate = false;
            }
        }

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
        enemy.transform.SetParent(_rss.displayCanvas.transform, false);
        enemy.GetComponent<HeroManager>().isPlayer = false;
        enemy.AddComponent<EnemyController>();


        Vector3 newEnemyPos = Vector3.zero;

        newEnemyPos = GenerateSpawn(newEnemyPos);

        enemy.transform.localPosition = newEnemyPos;
        enemy.GetComponent<EnemyController>().target = _rss.player;
        enemy.GetComponent<EnemyController>().origin = newEnemyPos;
        enemy.transform.tag = "Enemy";

        return enemy;
    }

    private List<GameObject> Spawn()
    {
        _rss.heroesMisc.SetActive(true);
        _enemy.Add(Instantiate(_rss.heroesMisc.transform.Find("Aphelios").gameObject));
        _enemy[_enemy.Count - 1] = InitEnemy(_enemy[_enemy.Count - 1]);

        Vector3 cameraScale = Vector3.zero;
        cameraScale.y = (Camera.main.orthographicSize * 2) * 100;
        cameraScale.x = (Camera.main.aspect * cameraScale.y);
        Vector3 cameraPos = Camera.main.transform.localPosition * 100;

        cameraScale = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale, cameraScale);
        cameraPos = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale, cameraPos);

        Vector3 newEnemyPos = Vector3.zero;


        Vector2 destination;
        destination.x = UnityEngine.Random.Range(cameraPos.x + -(cameraScale.x / 2), cameraPos.x + (cameraScale.x / 2));
        destination.y = UnityEngine.Random.Range(cameraPos.y + -(cameraScale.y / 2), cameraPos.y + (cameraScale.y / 2));
        _enemy[_enemy.Count - 1].GetComponent<EnemyController>().destination = destination;

        _rss.heroesMisc.SetActive(false);

        return _enemy;
    }

    public List<GameObject> enemy { get { return _enemy; } set { _enemy = value; } }
}
