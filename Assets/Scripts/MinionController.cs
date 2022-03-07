using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    // minion

    // currentHealth;

    // allowMove;

    // isAttacking

    // naturalDestination
    // destination 

    // currentTarget

    // isDestroy

    private Minion _minion;

    private int _currentHealth;

    private bool _allowMove;

    private bool _isAttacking;

    private Transform _naturalDestination;
    private Transform _destination;

    public bool _isDestroy;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _minion.health;

        _allowMove = true;

        _isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Move()
    {
        if (_destination != _naturalDestination)
        {
            // move to target
        }

        else
        {
            if (_allowMove)
            {
                // move to natural destination
            }
        }
    }
}
