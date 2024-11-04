using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    public UnityAction OnAlarm;

    [Header("Attributes")]
    [SerializeField] int maxHp;
    [SerializeField] public int curHp;
    [SerializeField] float dmg;
    [SerializeField] float time;
    

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            curHp--;

        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            curHp++;
        }

        AnimationPlay();
       
    }

    public void TakeHit(int dmg)
    {
        OnAlarm?.Invoke();

        curHp -= dmg;
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

}
