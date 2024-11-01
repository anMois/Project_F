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

    Lazer lazer;
    Player player;
    

    private Coroutine delayAttackCoroutine;

    private static int idleHash = Animator.StringToHash("Idle03");
    private static int lazerHash = Animator.StringToHash("Lazer");
    private static int attackHash = Animator.StringToHash("Attack");
    public int curAniHash { get; private set; }

    PlayerMover mover;


    private void Awake()
    {
        curBullet = bulletPrefab[0];
        ani = GetComponentInChildren<Animator>();
        mover = GetComponent<PlayerMover>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (mover.isGround && player.curHp > 0)
        {
            AnimatorPlay();

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Bullet();
                Shell();

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Lazer();
                }
                if(Input.GetKeyUp(KeyCode.Mouse0))
                {
                    //Destroy(obj);
                }
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
            obj.GetComponent<Bullet>().Launch(6, target, 1);
        }

    }

    private void Lazer()
    {
        if (curBullet == bulletPrefab[1])
        {
            GameObject obj = Instantiate(curBullet, lazerPos.position, lazerPos.rotation);

            obj.transform.parent = transform;
            obj.GetComponent<Lazer>().Damage(6, 1);
        }
    }

    private void Shell()
    {
        if (curBullet == bulletPrefab[2])
        {
            GameObject obj = Instantiate(curBullet, attackPos.position, attackPos.rotation);
            obj.GetComponent<Shell>().Launch(6, shellTarget, 1);
        }
    }



}
