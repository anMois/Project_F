using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : Monster
{
    protected override void Attack()
    {
        Debug.Log($"Monster Attack!");
        target.GetComponent<Test_Player>().TakeHit(attackDamage);
    }
}
