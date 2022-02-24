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

    private static Resources rss;
    private static SceneManager scene;

    private FreeMatrix.Utility.Time waitTime;

    private static ColorScheme colorScheme;
    private GameObject _target;
    private Vector2 _origin;
    private Vector2 _destination;

    private bool _isDestroy;

    private STATE _state;

    void Start()
    {
        rss = FindObjectOfType<Resources>();
        scene = FindObjectOfType<SceneManager>();
        colorScheme = FindObjectOfType<ColorScheme>();

        _isDestroy = false;

        this.gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", colorScheme.enemy);
        this.gameObject.GetComponent<HeroManager>().allowMove = true;

        if (scene.currentScene == scene.skillShot)
        {
            _destination = GenerateDestination();
            _state = STATE.PATROL;
        }

        if (scene.currentScene == scene.dodging)
        {
            waitTime = new FreeMatrix.Utility.Time(FreeMatrix.Utility.Time.TYPE.COUNTDOWN, 1);
            _state = STATE.FOLLOW;
        }

        else
        {
            waitTime = null;
        }
    }

    void FixedUpdate()
    {
        if (!isDestroy)
        {
            if (_state == STATE.PATROL)
            {


                if (scene.currentScene == scene.skillShot)
                {
                    if (Vector2.Distance(this.gameObject.transform.localPosition, _destination) < 0.1)
                    {
                        _state = STATE.FOLLOW;
                    }

                }
            }

            if (_state == STATE.FOLLOW)
            {
                if (scene.currentScene == scene.skillShot)
                {
                    destination = _target.transform.localPosition;
                }

                if (scene.currentScene == scene.dodging)
                {
                    if (destination != origin)
                    {
                        _destination = _target.transform.localPosition;

                        if (Vector2.Distance(this.gameObject.transform.localPosition, _destination) < 500)
                        {
                            _state = STATE.ATTACK;
                        }
                    }

                    if (Vector2.Distance(this.gameObject.transform.localPosition, _destination) < 0.1 && _destination == _origin)
                    {
                        isDestroy = true;
                        this.gameObject.GetComponent<HeroManager>().allowMove = false;
                    }
                }
            }

            if (_state == STATE.ATTACK)
            {
                if (scene.currentScene == scene.dodging)
                {
                    if (Vector2.Distance(this.gameObject.transform.localPosition, _destination) <= 500 && _destination != _origin)
                    {
                        this.gameObject.GetComponent<HeroManager>().allowMove = false;
                        this.gameObject.GetComponent<HeroManager>().target = _target.transform.localPosition;
                        this.gameObject.GetComponent<HeroManager>().abilityToggle1 = true;
                    }

                    if (!this.gameObject.GetComponent<HeroManager>().allowMove)
                    {
                        _destination = _origin;

                        if (waitTime.Update())
                        {
                            this.gameObject.GetComponent<HeroManager>().allowMove = true;
                            _state = STATE.PATROL;
                        }
                    }
                }
            }

            if (this.gameObject.GetComponent<HeroManager>().allowMove)
            {
                this.gameObject.transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween2D.PointTo(_destination, this.gameObject.transform.localPosition, this.gameObject.transform.localRotation));
                Vector3 newEnemyPos = Vector2.MoveTowards(this.gameObject.transform.localPosition, _destination, (this.gameObject.GetComponent<HeroManager>().moveSpeed - 100) * Time.deltaTime);
                newEnemyPos = FreeMatrix.Utility.Convert2D.LocalToPixel(rss.displayCanvas.transform.localScale, newEnemyPos);
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

        cameraScale = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale, cameraScale);
        cameraPos = FreeMatrix.Utility.Convert2D.PixelToLocal(rss.displayCanvas.transform.localScale, cameraPos);

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
                rss.sceneManager.GameOver();
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
                rss.scoreGameObject.GetComponent<Score>().score += 100;

                isDestroy = true;
                this.gameObject.GetComponent<HeroManager>().allowMove = false;
            }
        }
    }

    void OnMouseEnter()
    {
        this.gameObject.GetComponent<SpriteRenderer>().material.SetInt("_IsOutlineVisible", 1);
        this.gameObject.GetComponent<SpriteRenderer>().material.SetColor("_OutlineColor", colorScheme.enemyHighLight);
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
