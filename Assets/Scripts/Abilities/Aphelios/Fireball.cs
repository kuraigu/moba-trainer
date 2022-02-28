using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Ability/Fireball")]
public class Fireball : Ability
{
    private static Resources _rss;
    private static SceneManager _scene;

    private GameObject _projectileReference;
    private List<GameObject> _projectile;
    private List<Vector3> _oldPositionList;

    private float _thrust;

    private Vector2 _direction;

    public override void Start()
    {
        _rss = GameObject.Find("Game Script").GetComponent<Resources>();
        _projectileReference = _rss.fireBall;
        range = 500;

        _projectile = new List<GameObject>();
        _oldPositionList = new List<Vector3>();

        abilityName = "Fireball";
        isPassive = false;

        isActive = true;

        attackSpeed = 1000;

        coolDownTime = 1;
        coolDownTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, coolDownTime);
        coolDownTimeRun = false;

        _thrust = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale.x, attackSpeed);
        _thrust = FreeMatrix.Utility.Convert2D.LocalToWorld(_rss.displayCanvas.transform.localScale.x, attackSpeed);
    }

    public override void Activate()
    {
        if (isActive)
        {
            Debug.Log("TOGGLED");
            parent.GetComponent<HeroManager>().allowMove = false;

            _projectile.Add(Instantiate(_projectileReference));
            int tempIndex = _projectile.Count - 1;

            _projectile[tempIndex].transform.SetParent(_rss.displayCanvas.transform, false);
            _projectile[tempIndex].transform.localPosition = parent.transform.localPosition;

            if (parent.GetComponent<HeroManager>().isPlayer) _projectile[tempIndex].tag = "Player Projectile";
            else if (!parent.GetComponent<HeroManager>().isPlayer) _projectile[tempIndex].tag = "Enemy Projectile";


            Vector2 direction = onMousePosition - _projectile[tempIndex].transform.localPosition;

            Vector3 angle = FreeMatrix.Utility.Tween2D.PointTo(onMousePosition, _projectile[tempIndex].transform.localPosition, _projectile[tempIndex].transform.localRotation);

            _projectile[tempIndex].transform.localRotation = Quaternion.Euler(angle);
            parent.transform.localRotation = Quaternion.Euler(angle);

            _oldPositionList.Add(parent.transform.localPosition);

            isActive = false;
            coolDownTimeRun = true;
        }
    }

    public override void Update()
    {
        if (coolDownTimeRun)
        {
            if (coolDownTimer.Update())
            {
                isActive = true;
                coolDownTimeRun = false;
            }
        }
    }

    public override void FixedUpdate()
    {
        if (_projectile.Count > 0)
        {
            for (int i = 0; i < _projectile.Count; i++)
            {
                if (_projectile[i] == null)
                {
                    _projectile.RemoveAt(i);
                    _oldPositionList.RemoveAt(i);
                }
            }

            for (int i = 0; i < _projectile.Count; i++)
            {

                _projectile[i].GetComponent<Rigidbody2D>().velocity = _projectile[i].transform.right * new Vector2(_thrust, _thrust);


                if (Vector2.Distance(_oldPositionList[i], _projectile[i].transform.localPosition) >= FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale.x, range))
                {
                    Destroy(_projectile[i]);
                    _projectile.RemoveAt(i);
                    _oldPositionList.RemoveAt(i);
                }
            }


        }
    }
}
