using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] List<BossAttackRange> ranges = new();

    [SerializeField] protected int damage;

    protected void Start()
    {
        // Pull ranges
        for (int i = 0; i < transform.childCount; i++)
        {
            BossAttackRange range = transform.GetChild(i).GetComponent<BossAttackRange>();
            range.OnDetected += Attack;
            ranges.Add(range);
        }

        gameObject.SetActive(false);
    }

    private void Attack(IDamageable damageable)
    {
        damageable.TakeHit(damage);
    }
}
