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
        Vector3 offset = new Vector3(0, 3f, 0);

        Projectile newProjectile = Instantiate(projectilePrefab, muzzlePoint.position, Quaternion.identity);
        newProjectile.Launch(friendlyLayer, target.position + offset, damage);
    }
}
