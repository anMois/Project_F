using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossDragon : MonoBehaviour, IDamageable
{
    public UnityEvent<int> OnHpChanged;

    public enum BossAttackType
    {
        Land1, Land2, Land3, Land4, LandPattern1, LandPattern2, Fly1, Fly2, FlyPattern1, FlyPattern2, SIZE
    }

    [SerializeField] Transform target;
    [SerializeField] Transform origin;

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

    private Animator animator;

    private const int PatternAttackPeriod = 3;
    private const float attackJudgeTime = 0.1f;    // time of attack range collider remains set as true
    private bool isTargetBehind;

    public bool IsTargetBehind { set { isTargetBehind = value; } }
    public int CurHp { get { return curHp; } set { curHp = value; OnHpChanged?.Invoke(curHp); } }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        CurHp = maxHp;
    }

    private void OnEnable()
    {
        PlayIntroSound();
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
            animator.SetInteger("Attack", (int)type + 1);
            PlayAttackSound(type);
            StartCoroutine(AttackRoutine(attacks[(int)type]));
        }
        // Attack
        else
        {
            CheckBehind();
            
            if (isTargetBehind)
            {
                animator.SetInteger("Attack", (int)BossAttackType.Land3 + 1);
                PlayAttackSound(BossAttackType.Land3);
                StartCoroutine(AttackRoutine(attacks[(int)BossAttackType.Land3]));
            }
            else
            {
                BossAttackType type = NextAttackType(BossAttackType.Land1, BossAttackType.Land4);

                // Move & Attack
                animator.SetInteger("Attack", (int)type + 1);
                PlayAttackSound(type);
                StartCoroutine(MoveRoutine());
                switch (type)
                {
                    case BossAttackType.Land1:
                    case BossAttackType.Land2:
                    case BossAttackType.Land3:
                        StartCoroutine(AttackRoutine(attacks[(int)type]));
                        break;
                    case BossAttackType.Land4:
                        StartCoroutine(AttackRoutine(attacks[(int)type], 1f));
                        break;
                    default:
                        break;
                }
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
            BossAttackType type = NextAttackType(BossAttackType.FlyPattern1, BossAttackType.FlyPattern2);

            // Attack
            animator.SetInteger("Attack", (int)type + 1);
            PlayAttackSound(type);
            switch (type)
            {
                case BossAttackType.FlyPattern1:
                    StartCoroutine(AttackRoutine(attacks[(int)type], 2f));
                    break;
                case BossAttackType.FlyPattern2:
                    StartCoroutine(AttackRoutine(attacks[(int)type]));
                    break;
                default:
                    break;
            }
        }
        // Attack
        else
        {
            BossAttackType type = NextAttackType(BossAttackType.Fly1, BossAttackType.Fly2);

            // Attack
            animator.SetInteger("Attack", (int)type + 1);
            PlayAttackSound(type);
            StartCoroutine(AttackRoutine(attacks[(int)type]));
        }
    }

    private BossAttackType NextAttackType(BossAttackType startType, BossAttackType endType)
    {
        int n = UnityEngine.Random.Range(1, 101);

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

    IEnumerator AttackRoutine(BossAttack bossAttack, float judgeTime = attackJudgeTime)
    {
        Debug.Log($"[Dragon] {bossAttack.gameObject.name}!");
        bossAttack.gameObject.SetActive(true);

        yield return new WaitForSeconds(judgeTime);

        animator.SetInteger("Attack", 0);
        bossAttack.gameObject.SetActive(false);
    }

    public void TakeHit(int dmg)
    {
        CurHp -= dmg;
        if (!isFlying && CurHp <= maxHp / 2)
        {
            Fly();
        }

        if(CurHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        PlayDieSound();
        Destroy(gameObject, 5f);
        UIManager.Instance.ShowUI("Game Clear Canvas");
    }

    IEnumerator FlyRoutine()
    {
        float height = 1f;
        Vector3 offset = new Vector3(-19 * 5 / 2, height, 22 * 5 / 2);
        Vector3 center = origin.position + offset;
        
        // Fly to center
        transform.rotation = Quaternion.LookRotation(center - transform.position);
        while (Vector3.Distance(transform.position, center) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, center, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Move as radius
        float radius = 40f;
        while (Vector3.Distance(transform.position, center) < radius)
        {
            transform.position += moveSpeed * Time.deltaTime * transform.forward;
            yield return null;
        }

        // Glide circle
        float angle = moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
        while (CurHp > 0)
        {
            transform.RotateAround(center, transform.up, angle);
            yield return null;
        }
    }

    private void Fly()
    {
        isFlying = true;
        attackCount = 0;
        StopAllCoroutines();

        animator.SetTrigger("Fly");
        StartCoroutine(FlyRoutine());
        StartCoroutine(FlyAttackRoutine());
    }

    private void PlayIntroSound() => SoundManager.Instance.BossAppearSound();
    private void PlayDieSound() => SoundManager.Instance.BossDieSound();
    private void PlayAttackSound(BossAttackType attackType)
    {
        switch (attackType)
        {
            case BossAttackType.Land1:
                SoundManager.Instance.BossAttackSound1();
                break;
            case BossAttackType.Land2:
                SoundManager.Instance.BossAttackSound2();
                break;
            case BossAttackType.Land3:
                SoundManager.Instance.BossAttackSound3();
                break;
            case BossAttackType.Land4:
                SoundManager.Instance.BossLandPatternSound1();
                break;
            case BossAttackType.LandPattern1:
            case BossAttackType.LandPattern2:
            case BossAttackType.Fly1:
            case BossAttackType.Fly2:
            case BossAttackType.FlyPattern1:
            case BossAttackType.FlyPattern2:
                SoundManager.Instance.BossLandPatternSound2();
                break;
            default:
                break;
        }
    }
}