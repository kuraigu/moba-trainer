using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mage", menuName = "Minion/Mage")]
public class Mage : Minion
{
    public override void Start()
    {
        minionName = "Mage";

        health = 100;
        range = 700;
        attackDamage = 25;
        attackSpeed = 2;
        goldWorth = 20;
    }
}
