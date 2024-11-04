using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPos;
    [SerializeField] Transform lazerPos;
    [SerializeField] GameObject[] bulletPrefab;
    private GameObject curBullet;
    [SerializeField] Animator ani;
    [SerializeField] Transform target;
    [SerializeField] Transform shellTarget;
    [SerializeField] GameObject lazer;
    [SerializeField] int maxtime;
    [SerializeField] float time;
    [SerializeField] GameObject bombFactory;
    [SerializeField] int attackDmg;
    Player player;


    private Coroutine delayAttackCoroutine;

    private static int idleHash = Animator.StringToHash("Idle03");
    public static int lazerHash = Animator.StringToHash("Lazer");
    private static int attackHash = Animator.StringToHash("Attack");
    public int curAniHash { get; private set; }

    PlayerMover mover;


    private void Awake()
    {
        curBullet = bulletPrefab[0];
        ani = GetComponentInChildren<Animator>();
        mover = GetComponent<PlayerMover>();
        player = GetComponent<Player>();

        lazer.GetComponent<Lazer>().Damage("Monster", 1);
    }

    private void Update()
    {
        if (mover.isGround && player.curHp > 0)
        {
            if (mover.isGround)
            {
                AnimatorPlay();

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Delay();
                }
                Lazer();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapBullet(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapBullet(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapBullet(2);
        }
    }

    public void SwapBullet(int index)
    {
        curBullet = bulletPrefab[index];
    }


    public void AnimatorPlay()
    {
        int checkAniHash = 0;
        if (curBullet == bulletPrefab[0])
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                checkAniHash = attackHash;
            }
        }
        else if (curBullet == bulletPrefab[1])
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                checkAniHash = lazerHash;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                checkAniHash = idleHash;
            }
        }
        else if (curBullet == bulletPrefab[2])
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                checkAniHash = attackHash;
            }
        }
        

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            ani.Play(curAniHash);
        }
    }

    private void Bullet()
    {
        
        if (curBullet == bulletPrefab[0])
        {
            GameObject obj = Instantiate(curBullet, attackPos.position, attackPos.rotation);
            obj.GetComponent<Bullet>().Launch(6, target, attackDmg);
        }

    }

    private void Lazer()
    {
        if (curBullet == bulletPrefab[1])
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                StartCoroutine(AttackLazer());
                gameObject.GetComponent<PlayerMover>().enabled = false;
                
            }
            else if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                lazer.SetActive(false);
                gameObject.GetComponent<PlayerMover>().enabled = true;
            }
        }
    }

    private void Shell()
    {
        if (curBullet == bulletPrefab[2])
        {
            GameObject obj = Instantiate(curBullet, attackPos.position, attackPos.rotation);
        }
    }

    private void Delay()
    {
        StartCoroutine(DelayAttack());
    }

    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.2f);

        if(curBullet == bulletPrefab[0])
        {
            GameObject obj = Instantiate(curBullet, attackPos.position, attackPos.rotation);
            obj.GetComponent<Bullet>().Launch(6, target, attackDmg);
        }
        else if(curBullet == bulletPrefab[2])
        {
            GameObject obj = Instantiate(curBullet, attackPos.position, attackPos.rotation);
        }
    }

    IEnumerator AttackLazer()
    {
        lazer.SetActive(true);

        yield return new WaitForSeconds(time);

        lazer.SetActive(false);

        yield return null;
    }
    
}
