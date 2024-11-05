using UnityEngine;
using UnityEngine.Events;

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
        StatusWindowController.ItemChanged += Test;
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

    public void IncreaseDamage(int value)
    {
        Debug.Log("공격력 증가");
        dmg += value;
    }

    private void Test(string itemName)
    {
        switch (itemName)
        {
            case "불같은 분노":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "오우거의 심장":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "증폭의 고서":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "파괴의 룬":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "빗발치는 불꽃":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "한줄기 태양":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "차가운 시선":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "날선 추위":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "몰아치는 눈폭풍":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "한파의 고서":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "얼어붙는 동파":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "얼음거울":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "천둥의 룬":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "전기 전도":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "무거운 벼락":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "플라즈마 방전":
                IncreaseDamage(5);
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                break;
            case "치명의 고서":
                IncreaseDamage(5);
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                break;
            case "올곶은 흐름":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "대지의 룬":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "가이아의 축복":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "굳은 심지":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "유연한 사고":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "철의 마음":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
            case "지진의 고서":
                StatusWindowController.Instance.ChangeStat("ATK", 5);
                IncreaseDamage(5);
                break;
        }
    }
}
