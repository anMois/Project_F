using UnityEngine;
using UnityEngine.Events;
using static AttackBullet;

public class Player : MonoBehaviour, IDamageable
{
    //public UnityAction OnAlarm;

    [Header("Attributes")]
    [SerializeField] int maxHp;
    [SerializeField] public int curHp;
    [SerializeField] public float moveSpeed;
    [SerializeField] public int dmg;
    [SerializeField] public float time;
    [SerializeField] public float attackSpeed;
    [SerializeField] public string naming;



    Animator ani;
    PlayerMover mover;
    PlayerAttack attack;



    private static int idleHash = Animator.StringToHash("Idle03");
    private static int DieHash = Animator.StringToHash("Die");

    public int curAniHash { get; private set; }

    private void Awake()
    {
        ani = GetComponent<Animator>();
        mover = GetComponent<PlayerMover>();
        attack = GetComponent<PlayerAttack>();

        curHp = maxHp;

    }

    private void Start()
    {
        StatusWindowController.ItemChanged += PlayerStatusChange;
    }

    private void Update()
    {
        AnimationPlay();
        State();

    }

    public void TakeHit(int dmg)
    {
        //OnAlarm?.Invoke();

        curHp -= dmg;
        GameManager.Instance.TakeDamage(dmg);
        if (curHp <= 0)
        {
            ani.Play("Die");
            mover.GetComponent<PlayerMover>().enabled = false;
            attack.GetComponent<PlayerAttack>().enabled = false;
        }
        else if (curHp > 0)
        {
            mover.GetComponent<PlayerMover>().enabled = true;
            attack.GetComponent<PlayerAttack>().enabled = true;
        }
    }


    private void AnimationPlay()
    {
        int checkAniHash = 0;

        if (curHp <= 0)
        {
            checkAniHash = DieHash;
            gameObject.GetComponent<PlayerMover>().enabled = false;
            gameObject.GetComponent<PlayerAttack>().enabled = false;
        }
        else if (curHp > 0)
        {
            checkAniHash = idleHash;
            gameObject.GetComponent<PlayerMover>().enabled = true;
            gameObject.GetComponent<PlayerAttack>().enabled = true;
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            ani.Play(curAniHash);
        }
    }


    private void State()
    {
        attack.attackDmg = dmg;
        mover.addSpeed = moveSpeed;
        attack.speed = attackSpeed;
    }

    /// <summary>
    /// 공격력 증가
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseDamage(int value)
    {
        Debug.Log("공격력 증가");
        dmg += value;
    }

    /// <summary>
    /// 공격속도 증가
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseAttackSpeed(float percent)
    {
        Debug.Log("공격속도 증가");
        attackSpeed += attackSpeed * (percent / 100);
    }

    /// <summary>
    /// 이동속도 증가
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseMoveSpeed(float percent)
    {
        Debug.Log("이동속도 증가");
        moveSpeed += moveSpeed * (percent / 100);
    }

    /// <summary>
    /// 아이템에 따른 스탯을 변경하는 메서드
    /// </summary>
    /// <param name="itemName">아이템 이름</param>
    /// <param name="atkIncrease">공격력</param>
    /// <param name="atkSpeedIncreasePercent">공격속도</param>
    /// <param name="moveSpeedIncreasePercent">이동속도</param>
    private void ChangeStats(string itemName, int atkIncrease, float atkSpeedIncreasePercent, float moveSpeedIncreasePercent)
    {
        //UI 스테이터스 업데이트
        StatusWindowController.Instance.ChangeStat("ATK", atkIncrease);
        StatusWindowController.Instance.ChangeStat("ATS", (int)atkSpeedIncreasePercent);
        StatusWindowController.Instance.ChangeStat("SPD", (int)moveSpeedIncreasePercent);

        //실제 스테이터스 증가
        IncreaseDamage(atkIncrease);
        IncreaseAttackSpeed(atkSpeedIncreasePercent);
        IncreaseMoveSpeed(moveSpeedIncreasePercent);
    }

    private void PlayerStatusChange(string itemName)
    {
        switch (itemName)
        {
            case "불같은 분노":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "오우거의 심장":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "증폭의 고서":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "파괴의 룬":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "빗발치는 불꽃":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "한줄기 태양":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "차가운 시선":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "날선 추위":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "몰아치는 눈폭풍":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "한파의 고서":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "얼어붙는 동파":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "얼음거울":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "천둥의 룬":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "전기 전도":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "무거운 벼락":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "플라즈마 방전":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "치명의 고서":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "올곶은 흐름":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "대지의 룬":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "가이아의 축복":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "굳은 심지":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "유연한 사고":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "철의 마음":
                ChangeStats(itemName, 10, 15, 5);
                break;
            case "지진의 고서":
                ChangeStats(itemName, 10, 15, 5);
                break;
        }
    }
}
