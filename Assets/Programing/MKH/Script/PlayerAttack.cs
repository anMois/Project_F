using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPos;
    [SerializeField] Transform lazerPos;
    [SerializeField] GameObject[] bulletPrefab;
    [SerializeField] string naming;
    private GameObject curBullet;
    [SerializeField] Animator ani;
    [SerializeField] Transform target;
    [SerializeField] Transform shellTarget;
    [SerializeField] GameObject lazer;
    [SerializeField] float time;
    [SerializeField] GameObject bombFactory;
    [SerializeField] float attackDmg;
    Player player;

    Coroutine lazers;


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

        lazer.GetComponent<Lazer>().Damage(naming, (int)attackDmg);
    }

    private void Update()
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

    private void Lazer()
    {
        if (curBullet == bulletPrefab[1])
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                lazers = StartCoroutine(AttackLazer());

                mover.GetComponent<PlayerMover>().enabled = false;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                StopCoroutine(lazers);
                lazer.SetActive(false);
                mover.GetComponent<PlayerMover>().enabled = true;
            }
        }
    }
    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.2f);

        if (curBullet == bulletPrefab[0])
        {
            GameObject obj = Instantiate(curBullet, attackPos.position, attackPos.rotation);
            obj.GetComponent<Bullet>().Launch(6, target, (int)attackDmg);
        }
        else if (curBullet == bulletPrefab[2])
        {
            GameObject obj = Instantiate(curBullet, attackPos.position, attackPos.rotation);
            obj.GetComponent<Bomb>().Fire(naming, (int)1.5f * (int)attackDmg);


        }
    }
    private void Delay()
    {
        StartCoroutine(DelayAttack());
    }

    IEnumerator AttackLazer()
    {
        lazer.SetActive(true);

        yield return new WaitForSeconds(5f);

        lazer.SetActive(false);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Monster"))
    //    {
    //        Destroy(hitEffect, 4f);
    //    }
    //}

}
