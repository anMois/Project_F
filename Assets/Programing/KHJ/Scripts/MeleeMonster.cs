using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : Monster
{
    protected override void Attack()
    {
        base.Attack();

        target.GetComponent<IDamageable>().TakeHit(attackDamage);
    }
}
