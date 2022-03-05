using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    [SerializeField] Minion _minion;
    [SerializeField] GameObject _range;

    GameObject _target;

    int _currentHealth;

    bool _allowMove;

    // Start is called before the first frame update
    void Start()
    {
        _allowMove = true;

        if (this.gameObject.tag == "Ally Minion") _target = GameObject.Find("Ally Destination");
        if (this.gameObject.tag == "Enemy Minion") _target = GameObject.Find("Enemy Destination");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Minion minion { get { return _minion; } }

    public GameObject target { get { return _target; } set { _target = value; } }

    public int currentHealth { get { return _currentHealth; } }

    public bool allowMove { get { return _allowMove; } set { _allowMove = value; } }
}
