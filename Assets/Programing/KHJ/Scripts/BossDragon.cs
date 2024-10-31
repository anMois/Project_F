using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDragon : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] BossCheck behindCheck;
    [SerializeField] BossAttack[] attacks;
    [SerializeField] int[] attackRates;
    [SerializeField] int attackDelay;
    [SerializeField] int attackCount;

    [SerializeField] float moveSpeed;

    private const int PatternAttackPeriod = 3;
    private bool isTargetBehind;

    public bool IsTargetBehind { set { isTargetBehind = value; } }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        LandAttackCoroutine = StartCoroutine(LandAttackRoutine());
    }

    Coroutine LandAttackCoroutine;
    IEnumerator LandAttackRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(attackDelay);

        while (true)
        {
            isTargetBehind = false;
            LandAttack();
            yield return delay;
        }
    }

    private void LandAttack()
    {
        // Pattern Attack
        if (++attackCount % PatternAttackPeriod == 0)
        {
            Debug.Log("[Dragon] Pattern Attack!");
        }
        // Attack
        else
        {
            CheckBehind();
            
            if (isTargetBehind)
            {
                Attack(attacks[2]);
            }
            else
            {
                int n = Random.Range(1, 101);
                int type = -1;

                // Decide attack type
                for (int i = 0; i < attackRates.Length; i++)
                {
                    if (n <= attackRates[i])
                    {
                        type = i;
                        break;
                    }
                }

                // Attack
                Attack(attacks[type]);
            }
        }
    }

    private void CheckBehind()
    {
        behindCheck.gameObject.SetActive(true);
    }

    private void Attack(BossAttack bossAttack)
    {
        bossAttack.gameObject.SetActive(true);
        bossAttack.gameObject.SetActive(false);
    }
}