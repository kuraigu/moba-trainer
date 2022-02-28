using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    public enum STATE
    {
        PATROL,
        FOLLOW,
        ATTACK,
    }

    private static Resources _rss;
    private static SceneManager _scene;

    private FreeMatrix.Utility.Time _waitTime;

    private static ColorScheme _colorScheme;
    private GameObject _target;
    private Vector2 _origin;
    private Vector2 _destination;

    private bool _reachedDestination;

    private bool _isDestroy;

    private STATE _state;

    void Start()
    {
        _rss = FindObjectOfType<Resources>();
        _scene = FindObjectOfType<SceneManager>();
        _colorScheme = FindObjectOfType<ColorScheme>();

        this.gameObject.GetComponent<HeroManager>().hero.bonusMoveSpeed = -(float)(0.25 * this.gameObject.GetComponent<HeroManager>().hero.moveSpeed);

        _reachedDestination = false;

        isDestroy = false;

        this.gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", _colorScheme.enemy);
        this.gameObject.GetComponent<HeroManager>().allowMove = true;

        if (_scene.currentScene == _scene.skillShot)
        {
            int type = UnityEngine.Random.Range(0, 2);

            if (type == 0)
            {
                _destination = GenerateDestination();
                _state = STATE.PATROL;
            }

            if (type == 1)
            {
                _state = STATE.FOLLOW;
            }
        }

        if (_scene.currentScene == _scene.dodging)
        {
            _waitTime = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 0.5f);
            _state = STATE.FOLLOW;
        }

        else
        {
            _waitTime = null;
        }
    }

    void FixedUpdate()
    {
        if (!isDestroy)
        {
            if (_state == STATE.PATROL)
            {
                if (_scene.currentScene == _scene.skillShot)
                {
                    if (Vector2.Distance(this.gameObject.transform.localPosition, _destination) < 0.1)
                    {
                        _state = STATE.FOLLOW;
                    }
                }

                if (_scene.currentScene == _scene.dodging)
                {
                    _destination = _origin;

                    this.gameObject.GetComponent<Collider2D>().enabled = false;
                    this.gameObject.GetComponent<HeroManager>().hero.bonusMoveSpeed = 0;

                    if (Vector2.Distance(this.gameObject.transform.localPosition, _destination) < 0.1 && _destination == _origin)
                    {
                        isDestroy = true;
                        this.gameObject.GetComponent<HeroManager>().allowMove = false;
                    }
                }
            }

            if (_state == STATE.FOLLOW)
            {
                if (_scene.currentScene == _scene.skillShot)
                {
                    destination = _target.transform.localPosition;
                }

                if (_scene.currentScene == _scene.dodging)
                {
                    if (destination != origin)
                    {
                        _destination = _target.transform.localPosition;

                        if (Vector2.Distance(this.gameObject.transform.localPosition, _destination) < 450)
                        {
                            _state = STATE.ATTACK;
                        }
                    }
                }
            }

            if (_state == STATE.ATTACK)
            {
                if (_scene.currentScene == _scene.dodging)
                {
                    if (Vector2.Distance(this.gameObject.transform.localPosition, _destination) <= 450 && _destination != _origin)
                    {
                        this.gameObject.GetComponent<HeroManager>().allowMove = false;
                        this.gameObject.GetComponent<HeroManager>().target = _target.transform.localPosition;
                        _reachedDestination = true;
                    }

                    if (_reachedDestination)
                    {
                        if (_waitTime.Update())
                        {
                            this.gameObject.GetComponent<HeroManager>().abilityToggle1 = true;
                        }

                        if (this.gameObject.GetComponent<HeroManager>().ability1.coolDownTimeRun)
                        {
                            this.gameObject.GetComponent<HeroManager>().allowMove = true;
                            state = STATE.PATROL;
                        }
                    }


                }
            }

            if (this.gameObject.GetComponent<HeroManager>().allowMove)
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween2D.PointTo(_destination, this.gameObject.transform.localPosition, this.gameObject.transform.localRotation));
                Vector3 newEnemyPos = Vector2.MoveTowards(this.gameObject.transform.localPosition, _destination, (this.gameObject.GetComponent<HeroManager>().hero.moveSpeed +
                this.gameObject.GetComponent<HeroManager>().hero.bonusMoveSpeed) * Time.deltaTime);
                newEnemyPos = FreeMatrix.Utility.Convert2D.LocalToPixel(_rss.displayCanvas.transform.localScale, newEnemyPos);
                newEnemyPos = FreeMatrix.Utility.Convert2D.PixelToWorld(newEnemyPos);

                this.gameObject.GetComponent<Rigidbody2D>().position = newEnemyPos;
            }
        }

        if (isDestroy)
        {
            Despawn();
        }
    }

    void Despawn()
    {
        GetComponent<Collider2D>().enabled = false;
        float fade = GetComponent<SpriteRenderer>().material.GetFloat("_Fade");

        fade -= Time.deltaTime;

        GetComponent<SpriteRenderer>().material.SetFloat("_Fade", fade);

        if (fade <= 0f)
        {
            fade = 1f;
            Destroy(this.gameObject);
        }
    }

    Vector2 GenerateDestination()
    {
        Vector3 cameraScale = Vector3.zero;
        cameraScale.y = (Camera.main.orthographicSize * 2) * 100;
        cameraScale.x = (Camera.main.aspect * cameraScale.y);
        Vector3 cameraPos = Camera.main.transform.localPosition * 100;

        cameraScale = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale, cameraScale);
        cameraPos = FreeMatrix.Utility.Convert2D.PixelToLocal(_rss.displayCanvas.transform.localScale, cameraPos);

        Vector2 newPos = Vector2.zero;
        newPos.x = UnityEngine.Random.Range(cameraPos.x + -(cameraScale.x / 2), cameraPos.x + (cameraScale.x / 2));
        newPos.y = UnityEngine.Random.Range(cameraPos.y + -(cameraScale.y / 2), cameraPos.y + (cameraScale.y / 2));
        return newPos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!isDestroy)
        {
            if (col.transform.tag == "Player")
            {
                Destroy(this.gameObject);
                _rss.sceneManager.GameOver();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isDestroy)
        {
            if (col.transform.tag == "Player Projectile")
            {
                this.gameObject.GetComponent<SpriteRenderer>().material.SetInt("_IsOutlineVisible", 0);
                _rss.scoreGameObject.GetComponent<Score>().score += 100;

                isDestroy = true;
                this.gameObject.GetComponent<HeroManager>().allowMove = false;
            }
        }
    }

    void OnMouseEnter()
    {
        this.gameObject.GetComponent<SpriteRenderer>().material.SetInt("_IsOutlineVisible", 1);
        this.gameObject.GetComponent<SpriteRenderer>().material.SetColor("_OutlineColor", _colorScheme.enemyHighLight);
    }

    void OnMouseExit()
    {
        this.gameObject.GetComponent<SpriteRenderer>().material.SetInt("_IsOutlineVisible", 0);
    }

    public GameObject target { get { return _target; } set { _target = value; } }
    public Vector2 origin { get { return _origin; } set { _origin = value; } }
    public Vector2 destination { get { return _destination; } set { _destination = value; } }

    public STATE state { get { return _state; } set { _state = value; } }

    public bool isDestroy { get { return _isDestroy; } set { _isDestroy = value; } }
}
