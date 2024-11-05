using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] List<BossAttackRange> ranges = new();

    [SerializeField] protected Transform target;
    [SerializeField] protected bool isTargeting = false;
    [SerializeField] protected int damage;
    [SerializeField] ParticleSystem[] particles;

    protected void Start()
    {
        // Pull ranges
        for (int i = 0; i < transform.childCount; i++)
        {
            BossAttackRange range = transform.GetChild(i).GetComponent<BossAttackRange>();
            if (range == null)
                continue;

            range.OnDetected += Attack;
            ranges.Add(range);
        }

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        if (isTargeting)
        {
            transform.position = target.position;
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        if (particles != null)
        {
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void Attack(IDamageable damageable)
    {
        damageable.TakeHit(damage);
    }
}
