using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion", menuName = "Enemy/Minion")]
public class Minion : Enemy
{
    public override void Start()
    {
        enemyName = "Minion";
        _moveSpeed = 200;
        _type = TYPE.MINION;
        _state = STATE.FOLLOW;

        isActive = false;
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {
        _parent.transform.localPosition = Vector3.MoveTowards(parent.transform.localPosition, target.transform.localPosition, _moveSpeed * Time.deltaTime);

        _parent.transform.localRotation = Quaternion.Euler(FreeMatrix.Utility.Tween2D.PointTo(_target.transform.localPosition, _parent.transform.localPosition, _parent.transform.localRotation));
    }
}
