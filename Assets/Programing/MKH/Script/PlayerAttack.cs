using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject attackPos;
    [SerializeField] Animator animator;
    [SerializeField] float attackSpeed;
    [SerializeField] float lineSize;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject lazer;
    [SerializeField] GameObject bombshell;


    private static int lazerAttackHash = Animator.StringToHash("LazerAttack");
    private static int idleHash = Animator.StringToHash("Idle03");

    public int curAniHash { get; private set; }


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {

        Bullet();

        Lazer();
    }

    private void Bullet()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject obj = Instantiate(bulletPrefab, attackPos.transform.position, attackPos.transform.rotation.normalized);
            
        }


    }

    private void Lazer()
    {
        if (Input.GetKey(KeyCode.F2))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                animator.Play(lazerAttackHash);

                GameObject obj = Instantiate(lazer, attackPos.transform.position, attackPos.transform.rotation);


                Destroy(obj, 5f);
            }
        }

    }
}
