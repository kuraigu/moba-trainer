using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Minion/Melee")]
public class Melee : Minion
{
    public override void Start()
    {
        minionName = "Mage";

        health = 150;
        range = 100;
        attackDamage = 15;
        attackSpeed = 1;
        goldWorth = 25;
    }
}
