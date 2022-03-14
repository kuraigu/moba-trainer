using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    [Header("Edit")]
    [SerializeField] private Minion _minion;
    [SerializeField] private Seeker _seeker;
    [Space(10)]

    [Header("Read Only")]
    [SerializeField] private int _currentHealth;

    [SerializeField] private bool _allowMove;

    [SerializeField] private bool _isAttacking;

    [SerializeField] private Transform _naturalDestination;
    [SerializeField] private Transform _destination;                               // current destination of the gameobject

    [SerializeField] private bool _isDestroy;                                      // flag to destroy the gameobject
    [Space(1)]

    [SerializeField] private List<string> _seekTagList;                            // _seekTag list allows the Seeker to create a list of gameobjects allowed to be targeted

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _minion.health;

        _allowMove = true;

        _isAttacking = false;

        _seekTagList = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (_seeker.target != null) _destination = _seeker.target.transform;
        else _destination = _naturalDestination;

        if (_destination != _naturalDestination)
        {

        }

        else
        {
        }

        if (_allowMove)
        {

        }
    }

    public Minion minion { get { return _minion; } set { _minion = value; } }

    public int currentHealth { get { return _currentHealth; } set { _currentHealth = value; } }

    public bool allowMove { get { return _allowMove; } set { _allowMove = value; } }

    public bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }

    public Transform naturalDestination { get { return _naturalDestination; } set { _naturalDestination = value; } }
    public Transform destination { get; set; }

    public bool isDestroy { get { return _isDestroy; } set { _isDestroy = value; } }

    public List<string> seekTagList { get { return _seekTagList; } set { _seekTagList = value; } }
}
