using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion", menuName = "Minion")]
public class Minion : ScriptableObject
{
    [SerializeField] private string _controller;

    [SerializeField] private string _name;

    [SerializeField] private int _health;
    [SerializeField] private int _range;
    [SerializeField] private int _moveSpeed;

    public virtual void Attack() { }

    public string controller { get { return _controller; } protected set { _controller = value; } }

    public string minionName { get { return _name; } set { _name = value; } }

    public int health { get { return _health; } protected set { _moveSpeed = value; } }
    public int range { get { return _range; } protected set { _range = value; } }
    public int moveSpeed { get { return _moveSpeed; } protected set { _moveSpeed = value; } }
}