using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    private Resources _rss;

    private GameObject _type;
    private GameObject _mageMinionRef;
    private GameObject _meleeMinionRef;

    private FreeMatrix.Utility.Time _spawnTimer;
    private FreeMatrix.Utility.Time _intervalTimer;

    private bool _isSpawning;

    private int _counter;

    private List<GameObject> _allyMinionList;
    private List<GameObject> _enemyMinionList;

    void Start()
    {
        _rss = FindObjectOfType<Resources>();

        _type = null;

        _mageMinionRef = _rss.minionMisc.transform.Find("Mage").gameObject;

        _spawnTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 30);
        _intervalTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 1);

        _isSpawning = true;
        _counter = 0;

        _allyMinionList = new List<GameObject>();
        _enemyMinionList = new List<GameObject>();

        //_meleeMinionRef = _rss.minionMisc.transform.Find("Melee").gameOBject;
    }

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
                if (_counter < 7)
                {
                    _type = _mageMinionRef;
                    _allyMinionList = Spawn(_allyMinionList, "Ally Minion");
                    _enemyMinionList = Spawn(_enemyMinionList, "Enemy Minion");

                    _counter++;
                }

                else
                {
                    _counter = 0;
                    _isSpawning = false;
                }
            }
        }
    }

    /// <summary>
    /// Spawns the minions and initializes their starting values
    /// </summary>
    /// <param name="minionList">Minion List</param>
    List<GameObject> Spawn(List<GameObject> minionList, string type)
    {
        minionList.Add(Instantiate(_type));

        int count = minionList.Count - 1;
        MinionController minionCtrl = minionList[count].GetComponent<MinionController>();

        // naturalDestination
        // destination

        // seekTagList

        switch (type)
        {
            default:
                break;

            case "Ally Minion":
                minionCtrl.seekTagList.Add("Enemy Minion");

                break;

            case "Enemy Minion":
                minionCtrl.seekTagList.Add("Ally Minion");

                break;
        }

        return minionList;
    }


    /// <summary>
    /// Checks whether a minion is null, if it is then remove it from the list
    /// </summary>
    /// <param name="minionList">Minion List</param>
    List<GameObject> Remove(List<GameObject> minionList, string type)
    {
        return minionList;
    }
}
