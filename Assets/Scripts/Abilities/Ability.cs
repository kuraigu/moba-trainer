using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    // Parent
    protected GameObject _parent;

    // Properties
    protected string _abilityName;
    protected bool _isPassive;

    protected Vector3 _onMousePosition;
    protected Vector3 _destination;
    protected Vector3 _oldPosition;
    protected float _range;

    protected bool _isActive;
    protected bool _isDestroy;

    protected float _attackSpeed;
    protected float _moveSpeed;

    protected float _coolDownTime;
    protected float _activeTime;

    protected bool _activeTimeRun;
    protected bool _coolDownTimeRun;
    protected FreeMatrix.Utility.Time _coolDownTimer;
    protected FreeMatrix.Utility.Time _activeTimer;


    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    public virtual void Activate() { }
    public virtual bool WhileActive() { return false; }

    // Property Fields
    public GameObject parent { get { return _parent; } set { _parent = value; } }
    public string abilityName { get { return _abilityName; } set { _abilityName = value; } }
    public bool isPassive { get { return _isPassive; } set { _isPassive = value; } }

    public Vector3 onMousePosition { get { return _onMousePosition; } set { _onMousePosition = value; } }
    public Vector3 destination { get { return _destination; } set { _destination = value; } }
    public Vector3 oldPosition { get { return _oldPosition; } set { _oldPosition = value; } }
    public float range { get { return _range; } set { _range = value; } }

    public bool isActive { get { return _isActive; } set { _isActive = value; } }
    public bool isDestroy { get { return _isDestroy; } set { _isDestroy = value; } }

    public float attackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public float moveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    public float coolDownTime { get { return _coolDownTime; } set { _coolDownTime = value; } }
    public float activeTime { get { return _activeTime; } set { _activeTime = value; } }

    public bool activeTimeRun { get { return _activeTimeRun; } set { _activeTimeRun = value; } }
    public bool coolDownTimeRun { get { return _coolDownTimeRun; } set { _coolDownTimeRun = value; } }
    public FreeMatrix.Utility.Time coolDownTimer { get { return _coolDownTimer; } set { _coolDownTimer = value; } }
    public FreeMatrix.Utility.Time activeTimer { get { return _activeTimer; } set { _activeTimer = value; } }
}
