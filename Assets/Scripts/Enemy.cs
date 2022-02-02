using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{

    [SerializeField] protected GameObject _parent;
    [SerializeField] protected GameObject _target;

    [SerializeField] protected string _enemyName;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected TYPE _type;
    [SerializeField] protected STATE _state;

    [SerializeField] protected bool _isActive;

    public enum TYPE
    {
        MINION
    }

    public enum STATE
    {
        ATTACK,
        FOLLOW,
        PATROL
    }

    public GameObject target { get { return _target; } set { _target = value; } }
    public GameObject parent { get { return _parent; } set { _parent = value; } }

    // Properties
    public string enemyName { get { return _enemyName; } protected set { _enemyName = value; } }
    public float moveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    public bool isActive { get { return _isActive; } set { _isActive = value; } }

    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
