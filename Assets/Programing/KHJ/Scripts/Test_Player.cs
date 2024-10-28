using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Test_Player : MonoBehaviour, IDamageable
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] Transform aim;
    [SerializeField] int maxHp;
    [SerializeField] int curHp;

    private int friendlyLayer;

    private void Awake()
    {
        curHp = maxHp;
        friendlyLayer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    public void TakeHit(int dmg)
    {
        curHp -= dmg;
        if (curHp < 0)
        {
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        Projectile newProjectile = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);
        newProjectile.Launch(friendlyLayer, aim.transform);
    }
}
