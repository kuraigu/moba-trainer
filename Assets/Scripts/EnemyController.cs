using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    private static Resources rss;
    private static Materials materials;

    private static ColorScheme colorScheme;
    private GameObject _target;

    private bool _isDestroy;
    // Start is called before the first frame update
    void Start()
    {
        rss = FindObjectOfType<Resources>();
        materials = FindObjectOfType<Materials>();
        colorScheme = FindObjectOfType<ColorScheme>();

        _isDestroy = false;
        //this.gameObject.GetComponent<SpriteRenderer>().material.color = colorScheme.enemy;
    }

    void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDestroy)
        {
            this.gameObject.transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween2D.PointTo(_target.transform.localPosition, this.gameObject.transform.localPosition, this.gameObject.transform.localRotation));
            Vector3 newEnemyPos = Vector2.MoveTowards(this.gameObject.transform.localPosition, _target.transform.localPosition, 200 * Time.deltaTime);
            newEnemyPos = FreeMatrix.Utility.Convert2D.LocalToPixel(rss.displayCanvas.transform.localScale, newEnemyPos);
            newEnemyPos = FreeMatrix.Utility.Convert2D.PixelToWorld(newEnemyPos);

            this.gameObject.GetComponent<Rigidbody2D>().position = newEnemyPos;
        }

        if (isDestroy)
        {
            float fade = GetComponent<SpriteRenderer>().material.GetFloat("_Fade");

            fade -= Time.deltaTime;

            GetComponent<SpriteRenderer>().material.SetFloat("_Fade", fade);

            if (fade <= 0f)
            {
                fade = 1f;
                Destroy(this.gameObject);
            }

            Debug.Log(fade);
        }
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
                rss.scoreGameObject.GetComponent<Score>().score += 100;

                isDestroy = true;
                this.gameObject.GetComponent<HeroManager>().allowMove = false;
            }
        }
    }

    void onMouseOver()
    {
        this.gameObject.GetComponent<SpriteRenderer>().material.SetColor("_OutlineColor", colorScheme.enemy);
    }

    public GameObject target { get { return _target; } set { _target = value; } }

    public bool isDestroy { get { return _isDestroy; } set { _isDestroy = value; } }
}
