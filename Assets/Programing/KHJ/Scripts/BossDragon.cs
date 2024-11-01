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
    private float attackJudgeTime = 0.1f;    // time of attack range collider remains set as true
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
                StartCoroutine(AttackRoutine(attacks[2]));
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
                StartCoroutine(AttackRoutine(attacks[type]));
            }
        }
    }

    private void CheckBehind()
    {
        behindCheck.gameObject.SetActive(true);
    }

    IEnumerator AttackRoutine(BossAttack bossAttack)
    {
        bossAttack.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackJudgeTime);
        bossAttack.gameObject.SetActive(false);
    }
}