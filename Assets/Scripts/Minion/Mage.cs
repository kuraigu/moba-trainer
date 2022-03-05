using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mage", menuName = "Minion/Mage")]
public class Mage : Minion
{
    void OnEnable()
    {
        minionName = "Mage";

        health = 100;
        range = 150;
        moveSpeed = 150;
    }

    public override void Attack()
    {

    }
}
