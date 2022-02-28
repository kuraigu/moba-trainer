using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Firebust", menuName = "Ability/Firebust")]
public class Fireburst : Ability
{
    private static Resources _rss;

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
        isDestroy = false;

        attackSpeed = 700;

        coolDownTimeRun = false;
        coolDownTime = 5;
        coolDownTimer = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, coolDownTime);

        _thrust = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale.x, attackSpeed);
        _thrust = FreeMatrix.Utility.Convert2D.LocalToWorld(_rss.displayCanvas.transform.localScale.x, attackSpeed);
    }

    public override void Activate()
    {
        if (isActive)
        {
            parent.GetComponent<HeroManager>().allowMove = false;

            Vector2 direction = new Vector3();
            Vector3 angle = new Vector3();

            _projectile.Add(Instantiate(_projectileReference));
            _projectile.Add(Instantiate(_projectileReference));
            _projectile.Add(Instantiate(_projectileReference));

            for (int i = _projectile.Count - 3; i < _projectile.Count; i++)
            {
                _projectile[i].transform.SetParent(_rss.displayCanvas.transform, false);
                _projectile[i].transform.localPosition = parent.transform.localPosition;

                if (i == _projectile.Count - 3)
                {
                    direction = onMousePosition - _projectile[i].transform.localPosition;
                    angle = FreeMatrix.Utility.Tween2D.PointTo(onMousePosition, _projectile[i].transform.localPosition, _projectile[i].transform.localRotation);

                    _projectile[i].transform.localRotation = Quaternion.Euler(angle);
                    parent.transform.localRotation = Quaternion.Euler(angle);
                }

                else if (i > _projectile.Count - 3 && i % 2 == 0)
                {
                    float angleTemp = angle.z - 15;
                    _projectile[i].transform.localRotation = Quaternion.Euler(new Vector3(angle.x, angle.y, angleTemp));
                }

                else if (i > _projectile.Count - 3 && i % 2 == 1)
                {
                    float angleTemp = angle.z + 15;
                    _projectile[i].transform.localRotation = Quaternion.Euler(new Vector3(angle.x, angle.y, angleTemp));
                }

                if (parent.GetComponent<HeroManager>().isPlayer) _projectile[i].tag = "Player Projectile";
                else _projectile[i].tag = "Enemy Projectile";
            }

            _oldPositionList.Add(parent.transform.localPosition);
            _oldPositionList.Add(parent.transform.localPosition);
            _oldPositionList.Add(parent.transform.localPosition);

            isActive = false;
            coolDownTimeRun = true;

            Debug.Log(onMousePosition);
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
