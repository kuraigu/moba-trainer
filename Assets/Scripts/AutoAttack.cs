using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    private GameObject _target;

    void OnTriggerEnter2D(Collider2D col)
    {
        _target = null;

        if (this.gameObject.transform.parent.tag == "Enemy Minion")
        {
            if (col.tag == "Ally Minion")
            {
                _target = col.gameObject;
            }
        }

        if (this.gameObject.transform.parent.tag == "Ally Minion")
        {
            if (col.tag == "Enemy Minion")
            {
                _target = col.gameObject;
            }
        }
    }

    void OnTriggerExit2D()
    {
        _target = null;
    }

    public GameObject target { get { return _target; } }
}
