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

    void OnTriggerExit2D(Collider2D col)
    {
        _collideTag = col.transform.tag;
    }

    public string collideTag { get { return _collideTag; } set { _collideTag = value; } }
}
