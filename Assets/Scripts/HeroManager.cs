using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    private static Resources _rss;

    // Properties
    private GameObject _parent;
    [SerializeField] private Hero _heroRef;
    private Hero _hero;

    private bool _allowMove;
    private bool _isPlayer;

    // Abilities
    [SerializeField] Ability _ability1Ref;
    [SerializeField] Ability _ability2Ref;
    [SerializeField] Ability _ability3Ref;
    [SerializeField] Ability _ability4Ref;

    Ability _ability1;
    Ability _ability2;
    Ability _ability4;

    Vector3 _coolDownIndicatorOriginalScale;
    Vector3 _coolDownIndicatorOriginalPos;

    // KeyCodes
    private bool _abilityToggle1;
    private bool _abilityToggle2;
    private bool _abilityToggle3;
    private bool _abilityToggle4;

    private Vector2 _target;

    // Start is called before the first frame update
    void Start()
    {
        _rss = GameObject.Find("Game Script").GetComponent<Resources>();
        _allowMove = false;

        _hero = Instantiate(_heroRef);

        _ability1 = Instantiate(_ability1Ref);
        _ability2 = Instantiate(_ability2Ref);
        _ability4 = Instantiate(_ability4Ref);

        _ability1.Start();
        _ability2.Start();
        _ability4.Start();

        _abilityToggle1 = false;
        _abilityToggle2 = false;
        _abilityToggle3 = false;
        _abilityToggle4 = false;

        if (_isPlayer)
        {
            _coolDownIndicatorOriginalScale = _rss.coolDownIndicator1.transform.localScale;
            _coolDownIndicatorOriginalPos = _rss.coolDownIndicator1.transform.localPosition;
        }

        _target = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        _ability1.parent = this.gameObject;
        _ability2.parent = this.gameObject;
        _ability4.parent = this.gameObject;

        if (_isPlayer)
        {
            SetCoolDownIndicator(_ability1, _rss.abilityHolder1, _rss.coolDownIndicator1);
            SetCoolDownIndicator(_ability2, _rss.abilityHolder2, _rss.coolDownIndicator2);

            SetCoolDownIndicator(_ability4, _rss.abilityHolder4, _rss.coolDownIndicator4);
        }

        if (_abilityToggle1)
        {
            SetTarget(_ability1);

            _ability1.Activate();
            _abilityToggle1 = false;
        }

        if (_abilityToggle2)
        {
            SetTarget(_ability2);

            _ability2.Activate();
            _abilityToggle2 = false;
        }

        if (_abilityToggle4)
        {
            SetTarget(_ability4);

            _ability4.Activate();
            _abilityToggle4 = false;
        }

        _ability1.Update();
        _ability2.Update();
        _ability4.Update();

    }

    void FixedUpdate()
    {
        _ability1.FixedUpdate();
        _ability2.FixedUpdate();
        _ability4.FixedUpdate();
    }

    private void SetCoolDownIndicator(Ability ability, GameObject abilityHolder, GameObject coolDownIndicator)
    {
        if (ability.isActive)
        {
            coolDownIndicator.transform.localScale = _coolDownIndicatorOriginalScale;
            coolDownIndicator.SetActive(false);
        }

        if (ability.coolDownTimeRun)
        {
            coolDownIndicator.SetActive(true);

            Vector3 newScale = coolDownIndicator.transform.localScale;
            newScale.y = FreeMatrix.Utility.Tween2D.ScaleDown(coolDownIndicator.transform.localScale.y, (_coolDownIndicatorOriginalScale.y) / ability.coolDownTime);
            coolDownIndicator.transform.localScale = newScale;
        }

    }

    private void SetTarget(Ability ability)
    {
        if (_isPlayer)
        {
            ability.onMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ability.onMousePosition = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale, ability.onMousePosition * 100);
        }

        else if (!_isPlayer)
        {
            ability.onMousePosition = _target;
        }
    }

    // Properties
    public GameObject parent { get { return _parent; } set { _parent = value; } }
    public Hero hero { get { return _hero; } }
    public bool allowMove { get { return _allowMove; } set { _allowMove = value; } }
    public bool isPlayer { get { return _isPlayer; } set { _isPlayer = value; } }

    public bool abilityToggle1 { get { return _abilityToggle1; } set { _abilityToggle1 = value; } }
    public bool abilityToggle2 { get { return _abilityToggle2; } set { _abilityToggle2 = value; } }
    public bool abilityToggle3 { get { return _abilityToggle3; } set { _abilityToggle3 = value; } }
    public bool abilityToggle4 { get { return _abilityToggle4; } set { _abilityToggle4 = value; } }

    public Ability ability1 { get { return _ability1; } }
    public Ability ability2 { get { return _ability2; } }
    public Ability ability4 { get { return _ability4; } }

    public Vector2 target { get { return _target; } set { _target = value; } }
}
