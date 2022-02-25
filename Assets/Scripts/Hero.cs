using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class Hero : ScriptableObject
{
    [SerializeField] protected string _heroName;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _bonusMoveSpeed;

    void OnEnable()
    {
        _bonusMoveSpeed = 0;
    }

    public string heroName { get { return _heroName; } protected set { _heroName = value; } }
    public float moveSpeed { get { return _moveSpeed; } protected set { _moveSpeed = value; } }
    public float bonusMoveSpeed { get { return _bonusMoveSpeed; } set { _bonusMoveSpeed = value; } }
}
