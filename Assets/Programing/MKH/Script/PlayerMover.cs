using UnityEngine;

public partial class PlayerMover : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] public float moveSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float dashSpeed;
    [SerializeField] float yAngle;
    [SerializeField] float mouserotateSpeed;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigid;
    [SerializeField] public int maxJump;
    [SerializeField] public bool isGround = false;
    [SerializeField] bool isMove = false;
    [SerializeField] bool isJump = false;
    public int jumpCount;
    Vector3 moveDir;

    Player players;
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
        player = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        players = GetComponent<Player>();

        jumpCount = maxJump;
        Cursor.lockState = CursorLockMode.Locked;
        isMove = true;
        isJump = false;
    }

    private void Update()
    {
        if (players.curHp > 0)
        {
            ViewRotate();
            Dash();
            Jump();
        }
        AnimaitorPlay();
    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            Vector3 moveOffset = moveDir * (moveSpeed * Time.fixedDeltaTime);
            Vector3 runOffset = moveDir * (runSpeed * Time.fixedDeltaTime);

            if (Input.GetKey(KeyCode.LeftControl))
            {
                rigid.MovePosition(rigid.position + runOffset);
            }
            else
            {
                rigid.MovePosition(rigid.position + moveOffset);
            }
        }

        if (isJump && jumpCount >= 0)
        {
            rigid.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.VelocityChange);
            isJump = false;
        }
    }

    private void Move()
    {
        if (isMove)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            if (isMove)
            {
                moveDir = transform.forward * z + transform.right * x;
                moveDir.Normalize();
            }

            if (Input.GetKeyDown(KeyCode.Space) && jumpCount >= 0)
            {
                isJump = true;
                jumpCount--;
                if (jumpCount < 0)
                {
                    isJump = false;
                }
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                isMove = false;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isMove = true;
            }
        }


    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount >= 0)
        {
            isJump = true;
            jumpCount--;
            if (jumpCount < 0)
            {
                isJump = false;
            }

            if (isJump && jumpCount >= 0)
            {
                rigid.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.VelocityChange);
                isJump = false;
            }
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGround && isMove)
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

        //if (collision.transform.tag == "Wall")
        //{
        //    rigid.velocity = Vector3.zero;
        //    rigid.angularVelocity = Vector3.zero;
        //}

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

    private void ViewRotate()
    {

        yAngle += Input.GetAxis("Mouse X") * mouserotateSpeed;

        player.transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }







    #region 애니메이션
    public void AnimaitorPlay()
    {
        int checkAniHash = 0;


        if (Input.GetKey(KeyCode.W) && isMove)
        {
            checkAniHash = walkForwardHash;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                checkAniHash = runForwardHash;
            }
        }
        else if (Input.GetKey(KeyCode.S) && isMove)
        {
            checkAniHash = walkBackHash;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                checkAniHash = runForwardHash;
            }
        }
        else if (Input.GetKey(KeyCode.A) && isMove)
        {
            checkAniHash = walkLeftHash;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                checkAniHash = runForwardHash;
            }
        }
        else if (Input.GetKey(KeyCode.D) && isMove)
        {
            checkAniHash = walkRightHash;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                checkAniHash = runForwardHash;
            }
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            //attack.AnimatorPlay();
        }
        else
        {
            checkAniHash = idleHash;
        }

        if (rigid.velocity.y > 0.1f)
        {
            checkAniHash = jumpUpHash;
        }
        else if (rigid.velocity.y < -0.1f)
        {
            checkAniHash = jumpDownHash;
        }


        if (players.curHp <= 0)
        {
            checkAniHash = dieHash;
            isMove = false;
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }
    #endregion
}
