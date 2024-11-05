using UnityEngine;

public partial class PlayerMover : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] public float addSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float dashSpeed;
    [SerializeField] float mouserotateSpeed;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigid;
    [SerializeField] public int maxJump;
    [SerializeField] public bool isGround = false;
    [SerializeField] bool isJump = false;
    public int jumpCount;
    Vector3 moveDir;

    Player player;
    PlayerAttack attack;

    private static int idleHash = Animator.StringToHash("Idle03");
    private static int walkForwardHash = Animator.StringToHash("BattleWalkForward");
    private static int walkBackHash = Animator.StringToHash("BattleWalkBack");
    private static int walkRightHash = Animator.StringToHash("BattleWalkRight");
    private static int walkLeftHash = Animator.StringToHash("BattleWalkLeft");
    private static int runForwardHash = Animator.StringToHash("BattleRunForward");
    private static int dieHash = Animator.StringToHash("Die");
    private static int jumpUpHash = Animator.StringToHash("JumpUP");
    private static int jumpDownHash = Animator.StringToHash("JumpDown");
    private static int StopHash = Animator.StringToHash("Stop");



    public int curAniHash { get; private set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();

        jumpCount = maxJump;
        isJump = false;
    }

    private void Update()
    {
        Move();
        AnimaitorPlay();
        State();
    }

    private void FixedUpdate()
    {
        Vector3 moveOffset = moveDir * (moveSpeed * (1 + addSpeed / 100) * Time.fixedDeltaTime);
        Vector3 runOffset = moveDir * (runSpeed * (1 + addSpeed / 100) * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            rigid.MovePosition(rigid.position + runOffset);
        }
        else
        {
            rigid.MovePosition(rigid.position + moveOffset);
        }


        if (isJump && jumpCount >= 0)
        {
            rigid.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.VelocityChange);
            isJump = false;
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDir = transform.forward * z + transform.right * x;

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount >= 0)
        {
            isJump = true;
            jumpCount--;
            if (jumpCount < 0)
            {
                isJump = false;
            }
        }

    }

    #region 대쉬 (현재 사용 X)
    /*
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rigid.position = rigid.position + new Vector3(0f, 0f, dashSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rigid.position = rigid.position + new Vector3(0f, 0f, -dashSpeed);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rigid.position = rigid.position + new Vector3(-dashSpeed, 0f, 0f); ;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rigid.position = rigid.position + new Vector3(dashSpeed, 0f, 0f);
            }
        }
    }
    */
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGround = true;
            jumpCount = maxJump;
        }

        if (collision.transform.tag == "Monster")
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }


    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.tag == "Ground")
        {
            isGround = false;
        }

        if (collision.transform.tag == "Monster")
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }

    }

    #region 애니메이션
    public void AnimaitorPlay()
    {
        int checkAniHash = 0;


        if (Input.GetKey(KeyCode.W))
        {
            checkAniHash = walkForwardHash;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                checkAniHash = runForwardHash;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            checkAniHash = walkBackHash;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                checkAniHash = runForwardHash;
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            checkAniHash = walkLeftHash;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                checkAniHash = runForwardHash;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            checkAniHash = walkRightHash;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                checkAniHash = runForwardHash;
            }
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            checkAniHash = GetComponent<PlayerAttack>().curAniHash;
        }
        else
        {
            checkAniHash = idleHash;
        }

        if (rigid.velocity.y > 2f)
        {
            checkAniHash = jumpUpHash;
        }
        else if (rigid.velocity.y < -2f)
        {
            checkAniHash = jumpDownHash;
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }
    #endregion

    private void State()
    {
        addSpeed = moveSpeed;
    }
}
