using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrowAttack : BossAttack
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform muzzlePoint;

    private int friendlyLayer;

    private void Awake()
    {
        friendlyLayer = LayerMask.GetMask("Monster");
    }

    private void OnEnable()
    {
        if (target == null)
            return;

        Throw();
    }

    private void Throw()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, muzzlePoint.position, Quaternion.identity);
        newProjectile.Launch(friendlyLayer, target, damage);
    }
}
