using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    // parent - serialized
    // target - readonly
    // hasTarget  ;;  
    // seekTag    ;; 

    private GameObject _parent;

    private GameObject _target;
    private bool _hasTarget;
    private List<string> _seekTagList;


    // Start is called before the first frame update
    void Start()
    {
        _target = null;
        _hasTarget = false;
        _seekTagList = new List<string>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!_hasTarget)
        {
            if (_seekTagList.Count > 0)
            {
                for (int i = 0; i < _seekTagList.Count; i++)
                {
                    if (col.tag == _seekTagList[i])
                    {
                        _hasTarget = true;
                        _target = col.gameObject;
                    }
                }
            }
        }

        if (_target == null)
        {
            _hasTarget = false;
        }
    }

    public int parent { get; set; }

    public GameObject target { get; set; }
    public int hasTarget { get; set; }
    public int seekTag { get; set; }
}
