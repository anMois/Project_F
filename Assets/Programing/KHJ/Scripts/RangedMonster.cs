using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : Monster
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform muzzlePoint;

    private int friendlyLayer;

    private new void Awake()
    {
        base.Awake();

        friendlyLayer = LayerMask.GetMask("Monster");
    }

    protected override void Attack()
    {
        base.Attack();

        Vector3 offset = new Vector3(0, 2f, 0);

        Projectile newProjectile = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
        newProjectile.Launch(friendlyLayer, target.transform.position + offset, attackDamage);

        animator.SetInteger("Attack Motion", 0);
        animator.SetTrigger("Draw");
    }
}
