using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDragon : MonoBehaviour, IDamageable
{
    public enum BossAttackType
    {
        Land1, Land2, Land3, Land4, LandPattern1, LandPattern2, Fly1, Fly2, FlyPattern1, FlyPattern2, SIZE
    }

    [SerializeField] Transform target;
    [SerializeField] BossCheck behindCheck;
    [SerializeField] BossAttack[] attacks;
    [SerializeField] int[] attackRates;
    [SerializeField] int landAttackDelay;
    [SerializeField] int flyAttackDelay;
    [SerializeField] int attackCount;

    [SerializeField] bool isFlying = false;
    [SerializeField] int maxHp;
    [SerializeField] int curHp;
    [SerializeField] float moveSpeed;

    private const int PatternAttackPeriod = 3;
    private float attackJudgeTime = 0.1f;    // time of attack range collider remains set as true
    private bool isTargetBehind;

    public bool IsTargetBehind { set { isTargetBehind = value; } }

    private void Awake()
    {
        curHp = maxHp;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        LandAttackCoroutine = StartCoroutine(LandAttackRoutine());
    }

    Coroutine LandAttackCoroutine;
    IEnumerator LandAttackRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(landAttackDelay);

        while (!isFlying)
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
            BossAttackType type = NextAttackType(BossAttackType.LandPattern1, BossAttackType.LandPattern2);

            // Attack
            StartCoroutine(AttackRoutine(attacks[(int)type]));
        }
        // Attack
        else
        {
            CheckBehind();
            
            if (isTargetBehind)
            {
                StartCoroutine(AttackRoutine(attacks[(int)BossAttackType.Land3]));
            }
            else
            {
                BossAttackType type = NextAttackType(BossAttackType.Land1, BossAttackType.Land4);

                // Move & Attack
                StartCoroutine(MoveRoutine());
                StartCoroutine(AttackRoutine(attacks[(int)type]));
            }
        }
    }

    IEnumerator MoveRoutine()
    {
        float rotationSpeed = 5f;
        float weight = 0.7f;

        while (Vector3.Distance(transform.position, target.position) > 5f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, target.position, weight * Time.deltaTime), moveSpeed);
            yield return null;
        }
    }

    Coroutine FlyAttackCoroutine;
    IEnumerator FlyAttackRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(flyAttackDelay);

        while (isFlying)
        {
            FlyAttack();
            yield return delay;
        }
    }

    private void FlyAttack()
    {
        // Pattern Attack
        if (++attackCount % PatternAttackPeriod == 0)
        {
            //BossAttackType type = NextAttackType(BossAttackType.FlyPattern1, BossAttackType.FlyPattern2);

            //// Attack
            StartCoroutine(AttackRoutine(attacks[(int)BossAttackType.FlyPattern2]));
        }
        // Attack
        else
        {
            BossAttackType type = NextAttackType(BossAttackType.Fly1, BossAttackType.Fly2);

            // Attack
            StartCoroutine(AttackRoutine(attacks[(int)type]));
        }
    }

    private BossAttackType NextAttackType(BossAttackType startType, BossAttackType endType)
    {
        int n = Random.Range(1, 101);

        // Decide attack type
        for (int i = (int)startType; i <= (int)endType; i++)
        {
            if (n <= attackRates[i])
                return (BossAttackType)i;
        }

        Debug.LogError("Error: Boss attack selection");
        return endType;
    }

    private void CheckBehind()
    {
        behindCheck.gameObject.SetActive(true);
    }

    IEnumerator AttackRoutine(BossAttack bossAttack)
    {
        Debug.Log($"[Dragon] {bossAttack.gameObject.name}!");
        bossAttack.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackJudgeTime);
        bossAttack.gameObject.SetActive(false);
    }

    public void TakeHit(int dmg)
    {
        curHp -= dmg;
        if (!isFlying && curHp <= maxHp / 2)
        {
            Fly();
        }    
    }

    IEnumerator FlyRoutine()
    {
        float height = 3f;

        while (transform.position.y < height)
        {
            transform.position += moveSpeed * Time.deltaTime * Vector3.up;
            yield return null;
        }
    }

    private void Fly()
    {
        isFlying = true;
        attackCount = 0;
        if (LandAttackCoroutine != null)
        {
            StopCoroutine(LandAttackCoroutine);
            LandAttackCoroutine = null;
        }

        StartCoroutine(FlyRoutine());
        StartCoroutine(FlyAttackRoutine());
    }
}