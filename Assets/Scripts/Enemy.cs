using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ScriptableObject
{
    protected string _enemyName;
    protected float _moveSpeed;
    protected TYPE _type;
    protected STATE _state;

    public enum TYPE
    {
        MINION
    }

    public enum STATE
    {

    }

    // Properties
    public string enemyName { get { return _enemyName; } protected set { _enemyName = value; } }
    public float moveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
}
