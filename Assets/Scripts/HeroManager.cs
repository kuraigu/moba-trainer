using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    private static Resources rss;

    // Properties
    [SerializeField] private GameObject _parent;
    [SerializeField] private Hero _hero;
    private bool _allowMove;
    private float _moveSpeed;


    // Abilities
    [SerializeField] Ability ability1;
    [SerializeField] Ability ability2;
    [SerializeField] Ability ability3;
    [SerializeField] Ability ability4;

    Vector3 coolDownIndicatorOriginalScale;
    Vector3 coolDownIndicatorOriginalPos;

    // KeyCodes
    [SerializeField] KeyCode abilityKey1;
    [SerializeField] KeyCode abilityKey2;
    [SerializeField] KeyCode abilityKey3;
    [SerializeField] KeyCode abilityKey4;

    // Start is called before the first frame update
    void Start()
    {
        rss = GameObject.Find("Game Script").GetComponent<Resources>();
        _allowMove = false;
        _moveSpeed = _hero.moveSpeed;

        ability1.Start();
        ability2.Start();
        ability4.Start();

        ability1.parent = _parent;
        ability2.parent = _parent;
        ability4.parent = _parent;

        coolDownIndicatorOriginalScale = rss.coolDownIndicator1.transform.localScale;
        coolDownIndicatorOriginalPos = rss.coolDownIndicator1.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(abilityKey1))
        {
            ability1.Activate();
        }

        if (Input.GetKeyDown(abilityKey2))
        {
            ability2.Activate();
        }

        if (Input.GetKeyDown(abilityKey4))
        {
            ability4.Activate();
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

        setCoolDownIndicator(ability1, rss.abilityHolder1, rss.coolDownIndicator1);
        setCoolDownIndicator(ability2, rss.abilityHolder2, rss.coolDownIndicator2);

        setCoolDownIndicator(ability4, rss.abilityHolder4, rss.coolDownIndicator4);
    }

    private void setCoolDownIndicator(Ability ability, GameObject abilityHolder, GameObject coolDownIndicator)
    {
        if (ability.isActive)
        {
            coolDownIndicator.transform.localScale = coolDownIndicatorOriginalScale;
            coolDownIndicator.transform.localScale = coolDownIndicatorOriginalScale;
            coolDownIndicator.transform.localPosition = rss.abilityHolder1.transform.localPosition;
            coolDownIndicator.SetActive(false);
        }

        if (ability.coolDownTimeRun)
        {
            coolDownIndicator.SetActive(true);

            Vector3 rate = new Vector3(0, coolDownIndicatorOriginalScale.y / ability.coolDownTimer.max, 0);
            coolDownIndicator.transform.localScale -= rate * Time.deltaTime;
            Vector3 newPos = abilityHolder.transform.localPosition;
            newPos.y = (abilityHolder.transform.localPosition.y + (abilityHolder.transform.localScale.y / 2)) - (coolDownIndicator.transform.localScale.y / 2);
            coolDownIndicator.transform.localPosition = newPos;
        }
    }

    // Properties
    public GameObject parent { get { return _parent; } set { _parent = value; } }
    public Hero hero { get { return _hero; } }
    public bool allowMove { get { return _allowMove; } set { _allowMove = value; } }
    public float moveSpeed { get { return _moveSpeed; } }
}
