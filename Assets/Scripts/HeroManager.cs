using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    private static Resources rss;

    // Properties
    private GameObject _parent;
    [SerializeField] private Hero _hero;

    private bool _allowMove;
    private bool _isPlayer;
    private float _moveSpeed;

    // Abilities
    [SerializeField] Ability ability1Ref;
    [SerializeField] Ability ability2Ref;
    [SerializeField] Ability ability3Ref;
    [SerializeField] Ability ability4Ref;

    Ability ability1;
    Ability ability2;
    Ability ability4;

    Vector3 coolDownIndicatorOriginalScale;
    Vector3 coolDownIndicatorOriginalPos;

    // KeyCodes
    private bool _abilityToggle1;
    private bool _abilityToggle2;
    private bool _abilityToggle3;
    private bool _abilityToggle4;
    private bool onToggle;

    // Start is called before the first frame update
    void Start()
    {
        rss = GameObject.Find("Game Script").GetComponent<Resources>();
        _allowMove = false;
        _moveSpeed = _hero.moveSpeed;

        ability1 = Instantiate(ability1Ref);
        ability2 = Instantiate(ability2Ref);
        ability4 = Instantiate(ability4Ref);

        ability1.Start();
        ability2.Start();
        ability4.Start();

        _abilityToggle1 = false;
        _abilityToggle2 = false;
        _abilityToggle3 = false;
        _abilityToggle4 = false;
        onToggle = false;

        if (isPlayer)
        {
            coolDownIndicatorOriginalScale = rss.coolDownIndicator1.transform.localScale;
            coolDownIndicatorOriginalPos = rss.coolDownIndicator1.transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ability1.parent = gameObject;
        ability2.parent = gameObject;
        ability4.parent = gameObject;

        if (isPlayer)
        {
            setCoolDownIndicator(ability1, rss.abilityHolder1, rss.coolDownIndicator1);
            setCoolDownIndicator(ability2, rss.abilityHolder2, rss.coolDownIndicator2);

            setCoolDownIndicator(ability4, rss.abilityHolder4, rss.coolDownIndicator4);
        }

        if (_abilityToggle1)
        {
            ability1.Activate();
            _abilityToggle1 = false;
            onToggle = true;
        }

        if (_abilityToggle2)
        {
            ability2.Activate();
            _abilityToggle2 = false;
            onToggle = true;
        }

        if (_abilityToggle4)
        {
            ability4.Activate();


            _abilityToggle4 = false;
            onToggle = true;

        }

        ability1.Update();
        ability2.Update();
        ability4.Update();

    }

    void FixedUpdate()
    {
        ability1.FixedUpdate();
        ability2.FixedUpdate();
        ability4.FixedUpdate();
    }

    private void setCoolDownIndicator(Ability ability, GameObject abilityHolder, GameObject coolDownIndicator)
    {
        if (ability.isActive)
        {
            coolDownIndicator.transform.localScale = coolDownIndicatorOriginalScale;
            coolDownIndicator.transform.localPosition = rss.abilityHolder1.transform.localPosition;
            coolDownIndicator.SetActive(false);
        }

        if (ability.coolDownTimeRun)
        {
            coolDownIndicator.SetActive(true);

            Vector3 newScale = coolDownIndicator.transform.localScale;
            newScale.y = FreeMatrix.Utility.Tween2D.ScaleDown(coolDownIndicator.transform.localScale.y, (coolDownIndicatorOriginalScale.y) / ability.coolDownTime);
            coolDownIndicator.transform.localScale = newScale;
            Vector3 newPos = abilityHolder.transform.localPosition;
            newPos.y = (abilityHolder.transform.localPosition.y + (abilityHolder.transform.localScale.y / 2)) - (coolDownIndicator.transform.localScale.y / 2);
            coolDownIndicator.transform.localPosition = newPos;
        }

    }

    // Properties
    public GameObject parent { get { return _parent; } set { _parent = value; } }
    public Hero hero { get { return _hero; } }
    public bool allowMove { get { return _allowMove; } set { _allowMove = value; } }
    public bool isPlayer { get { return _isPlayer; } set { _isPlayer = value; } }
    public float moveSpeed { get { return _moveSpeed; } }


    public bool abilityToggle1 { get { return _abilityToggle1; } set { _abilityToggle1 = value; } }
    public bool abilityToggle2 { get { return _abilityToggle2; } set { _abilityToggle2 = value; } }
    public bool abilityToggle3 { get { return _abilityToggle3; } set { _abilityToggle3 = value; } }
    public bool abilityToggle4 { get { return _abilityToggle4; } set { _abilityToggle4 = value; } }
}
