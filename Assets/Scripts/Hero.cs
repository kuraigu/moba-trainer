using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class Hero : ScriptableObject
{
    [SerializeField] protected string _heroName;
    [SerializeField] protected float _moveSpeed;

    public string heroName { get { return _heroName; } protected set { _heroName = value; } }
    public float moveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
}
