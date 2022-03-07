using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : ScriptableObject
{
    [SerializeField] private GameObject _parent;

    [SerializeField] private string _minionName;

    [SerializeField] private int _health;
    [SerializeField] private float _range;
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _goldWorth;

    public virtual void Start() { }

    public string minionName { get { return _minionName; } protected set { _minionName = value; } }

    public int health { get { return _health; } protected set { _health = value; } }
    public float range { get { return _range; } protected set { _range = value; } }
    public int attackDamage { get { return _attackDamage; } protected set { _attackDamage = value; } }
    public float attackSpeed { get { return _attackSpeed; } protected set { _attackSpeed = value; } }
    public int goldWorth { get { return _goldWorth; } protected set { _goldWorth = value; } }
}
