// MinionManager handles the spawn cycle of minions and sets their initial values

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    // mageMinionRef
    // meleeMionionRef

    // spawntimer
    // intervaltimer

    // counter

    private GameObject _type;
    private GameObject _mageMinionRef;
    private GameObject _meleeMinionRef;

    private FreeMatrix.Utility.Time _spawnTimer;
    private FreeMatrix.Utility.Time _intervalTimer;

    private int _counter;

    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// Spawns the minions and initializes their starting values
    /// </summary>
    /// <param name="minionList">Minion List</param>
    void Spawn(List<GameObject> minionList)
    {

    }


    /// <summary>
    /// Checks whether a minion is null, if it is then remove it from the list
    /// </summary>
    /// <param name="minionList">Minion List</param>
    void Remove(List<GameObject> minionList)
    {

    }
}
