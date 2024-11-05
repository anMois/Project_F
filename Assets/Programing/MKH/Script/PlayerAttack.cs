using System.Collections;
using UnityEngine;

public enum WeaponType
{
    Flame,
    Ice,
    Electricity,
    Earth
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform attackPos;
    [SerializeField] Transform lazerPos;
    [SerializeField] GameObject[] bulletPrefab;
    private GameObject curBullet;
    [SerializeField] GameObject[] bulletProperty;
    private GameObject curProperty;

    [SerializeField] Transform target;
    [SerializeField] Transform shellTarget;

    [SerializeField] string naming;
    [SerializeField] public int attackDmg;
    [SerializeField] public float speed;

    Coroutine lazers;

    private Coroutine delayAttackCoroutine;

    private static int idleHash = Animator.StringToHash("Idle03");
    public static int lazerHash = Animator.StringToHash("Lazer");
    private static int attackHash = Animator.StringToHash("Attack");
    public int curAniHash { get; private set; }

    PlayerMover mover;
    Player player;
    Animator ani;


    private void Awake()
    {
        curBullet = bulletPrefab[0];
        ani = GetComponentInChildren<Animator>();
        mover = GetComponent<PlayerMover>();
        player = GetComponent<Player>();
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
        BulletChange();
        AddState();
    }

    public void SwapBullet(int index)
    {
        curBullet = bulletPrefab[index];
    }

    public void SwapPreperty(int index)
    {
        curProperty = bulletProperty[index];
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
            bulletPrefab[1].GetComponent<Lazer>().Damage(naming, 10 + attackDmg);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                lazers = StartCoroutine(AttackLazer());

            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                StopCoroutine(lazers);
                bulletPrefab[1].SetActive(false);
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                mover.GetComponent<PlayerMover>().enabled = false;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
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
            obj.GetComponent<Bomb>().Fire(naming, 12 + attackDmg, 30f * (1 + (speed / 100)));
        }
        else if (curBullet == bulletPrefab[2])
        {
            GameObject obj = Instantiate(curBullet, attackPos.position, attackPos.rotation);
            obj.GetComponent<Bullet>().Launch(6, target, 50 + attackDmg, 10f * (1 + (speed / 100)));
        }
    }
    private void Delay()
    {
        StartCoroutine(DelayAttack());
    }

    IEnumerator AttackLazer()
    {
        bulletPrefab[1].SetActive(true);

        yield return new WaitForSeconds(5f);

        bulletPrefab[1].SetActive(false);
    }

    private void BulletChange()
    {
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

    public void WeaponTypes(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Flame:
                bulletPrefab[0] = bulletProperty[0];
                bulletProperty[4].SetActive(true);
                bulletProperty[5].SetActive(false);
                bulletProperty[6].SetActive(false);
                bulletProperty[7].SetActive(false);
                bulletPrefab[2] = bulletProperty[8];
                break;

            case WeaponType.Ice:
                bulletPrefab[0] = bulletProperty[1];
                bulletProperty[4].SetActive(false);
                bulletProperty[5].SetActive(true);
                bulletProperty[6].SetActive(false);
                bulletProperty[7].SetActive(false);
                bulletPrefab[2] = bulletProperty[9];
                break;

            case WeaponType.Electricity:
                bulletPrefab[0] = bulletProperty[2];
                bulletProperty[4].SetActive(false);
                bulletProperty[5].SetActive(false);
                bulletProperty[6].SetActive(true);
                bulletProperty[7].SetActive(false);
                bulletPrefab[2] = bulletProperty[10];
                break;

            case WeaponType.Earth:
                bulletPrefab[0] = bulletProperty[3];
                bulletProperty[4].SetActive(false);
                bulletProperty[5].SetActive(false);
                bulletProperty[6].SetActive(false);
                bulletProperty[7].SetActive(true);
                bulletPrefab[2] = bulletProperty[11];
                break;
        }
    }

    private void AddState()
    {
        naming = player.naming;
        attackDmg = player.dmg;
        speed = player.attackSpeed;
    }
}
