using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private string _collideTag;

    // Start is called before the first frame update
    void Start()
    {
        _collideTag = null;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.gameObject.tag == "Player Projectile")
        {
            if (col.transform.tag == "Enemy")
            {
                Destroy(this.gameObject);
            }
        }

        if (this.gameObject.tag == "Enemy Projectile")
        {
            if (col.transform.tag == "Player")
            {
                Destroy(this.gameObject);
            }
        }
    }

    public string collideTag { get { return _collideTag; } set { _collideTag = value; } }
}
