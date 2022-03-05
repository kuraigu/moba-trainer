using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    // 3 melee, 3 mages, 1 siege
    // allyMinion list
    // enemyMinion list

    // spawnTimer;
    // intervalTimer;

    // spawnPosition;
    private bool _isSpawning;

    private int _counter;
    private GameObject _mageMinionRef;

    private List<GameObject> _allyMinion;
    private List<GameObject> _enemyMinion;

    private Transform _allySpawner;
    private Transform _enemySpawner;

    private FreeMatrix.Utility.Time _spawnTimer;
    private FreeMatrix.Utility.Time _intervalTimer;

    // Start is called before the first frame update
    void Start()
    {
        _isSpawning = true;

        _counter = 0;

        _mageMinionRef = GameObject.Find("Minions").transform.Find("Mage").gameObject;
        _mageMinionRef.SetActive(false);

        _allySpawner = GameObject.Find("Minions").transform.Find("Ally Spawner").gameObject.transform;
        _enemySpawner = GameObject.Find("Minions").transform.Find("Enemy Spawner").gameObject.transform;

        _allyMinion = new List<GameObject>();
        _enemyMinion = new List<GameObject>();

        _spawnTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 30);
        _intervalTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnTimer.Update())
        {
            _isSpawning = true;
        }

        if (_isSpawning)
        {
            if (_intervalTimer.Update())
            {
                _allyMinion.Add(Instantiate(_mageMinionRef));
                _allyMinion[_allyMinion.Count - 1].SetActive(true);
                _allyMinion[_allyMinion.Count - 1].tag = "Ally Minion";
                _allyMinion[_allyMinion.Count - 1].transform.localPosition = _allySpawner.localPosition;
                _allyMinion[_allyMinion.Count - 1].transform.SetParent(GameObject.Find("Minions").transform, false);

                _enemyMinion.Add(Instantiate(_mageMinionRef));
                _enemyMinion[_enemyMinion.Count - 1].SetActive(true);
                _enemyMinion[_enemyMinion.Count - 1].tag = "Enemy Minion";
                _enemyMinion[_enemyMinion.Count - 1].transform.localPosition = _enemySpawner.localPosition;
                _enemyMinion[_enemyMinion.Count - 1].transform.SetParent(GameObject.Find("Minions").transform, false);
                _counter++;
            }

            if (_counter >= 1)
            {
                _counter = 0;

                _isSpawning = false;
            }
        }
    }
}
