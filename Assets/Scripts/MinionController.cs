using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    MinionManager _minionManager;

    // Start is called before the first frame update
    void Start()
    {
        _minionManager = this.gameObject.GetComponent<MinionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.Find("Range").GetComponent<AutoAttack>().target != null)
        {
            if (_minionManager.allowMove)
            {
                this.gameObject.transform.localPosition = Vector2.MoveTowards(this.gameObject.transform.localPosition,
                this.gameObject.transform.Find("Range").GetComponent<AutoAttack>().target.transform.localPosition,
                _minionManager.minion.moveSpeed * Time.deltaTime);

                if (Vector2.Distance(this.gameObject.transform.localPosition, this.gameObject.transform.Find("Range").GetComponent<AutoAttack>().target.transform.localPosition) <=
                FreeMatrix.Utility.Convert2D.PixelToLocal(this.gameObject.transform.parent.transform.localScale.x, _minionManager.minion.range))
                {
                    _minionManager.allowMove = false;
                }
            }
        }

        else
        {
            _minionManager.allowMove = true;

            if (_minionManager.allowMove)
            {
                this.gameObject.transform.localPosition = Vector2.MoveTowards(this.gameObject.transform.localPosition, _minionManager.target.transform.localPosition,
                _minionManager.minion.moveSpeed * Time.deltaTime);
            }
        }
    }
}
