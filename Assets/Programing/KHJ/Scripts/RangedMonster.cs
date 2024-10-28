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
        Projectile newProjectile = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
        newProjectile.Launch(friendlyLayer, target.transform);
    }
}
