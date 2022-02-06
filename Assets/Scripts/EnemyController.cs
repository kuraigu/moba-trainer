using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    private static Resources rss;
    private static ColorScheme colorScheme;
    private GameObject _target;

    private bool _isDestroy;
    // Start is called before the first frame update
    void Start()
    {
        rss = FindObjectOfType<Resources>();
        colorScheme = FindObjectOfType<ColorScheme>();

        _isDestroy = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = colorScheme.enemy;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.gameObject.transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween2D.PointTo(_target.transform.localPosition, this.gameObject.transform.localPosition, this.gameObject.transform.localRotation));
        Vector3 newEnemyPos = Vector2.MoveTowards(this.gameObject.transform.localPosition, _target.transform.localPosition, 200 * Time.deltaTime);
        newEnemyPos = FreeMatrix.Utility.Convert2D.LocalToPixel(rss.displayCanvas.transform.localScale, newEnemyPos);
        newEnemyPos = FreeMatrix.Utility.Convert2D.PixelToWorld(newEnemyPos);

        this.gameObject.GetComponent<Rigidbody2D>().position = newEnemyPos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            Destroy(this.gameObject);
            rss.sceneManager.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player Projectile")
        {
            int score = System.Convert.ToInt32(rss.score.GetComponent<TextMeshProUGUI>().text);
            score += 100;
            rss.score.GetComponent<TextMeshProUGUI>().text = score.ToString();

            Destroy(this.gameObject);
        }
    }

    public GameObject target { get { return _target; } set { _target = value; } }

    public bool isDestroy { get { return _isDestroy; } set { _isDestroy = value; } }
}
