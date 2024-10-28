using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float moveSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float dashSpeed;
    [SerializeField] float yAngle;
    [SerializeField] float mouserotateSpeed;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigid;
    [SerializeField] float hp;
    [SerializeField] int maxJump;
    [SerializeField] bool isGround = false;
    int jumpCount;


    private static int idleHash = Animator.StringToHash("Idle03");
    private static int walkForwardHash = Animator.StringToHash("BattleWalkForward");
    private static int walkBackHash = Animator.StringToHash("BattleWalkBack");
    private static int walkRightHash = Animator.StringToHash("BattleWalkRight");
    private static int walkLeftHash = Animator.StringToHash("BattleWalkLeft");
    private static int runForwardHash = Animator.StringToHash("BattleRunForward");
    private static int dieHash = Animator.StringToHash("Die");
    private static int jumpUpHash = Animator.StringToHash("JumpUP");
    private static int jumpDownHash = Animator.StringToHash("JumpDown");


    public int curAniHash { get; private set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();

        jumpCount = maxJump;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        PlayerCamera();
        AnimaitorPlay();
        Jump();
        Dash();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, 0, z);
        if (moveDir == Vector3.zero)
            return;

        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        transform.Rotate(moveDir.normalized * moveSpeed * Time.deltaTime);

        if(Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(moveDir.normalized * runSpeed * Time.deltaTime);
            transform.Rotate(moveDir.normalized * runSpeed * Time.deltaTime);
        }

        if(hp == 0)             
        {
            moveSpeed = 0;
        }
    }

    private void PlayerCamera()
    {
        yAngle += Input.GetAxis("Mouse X") * mouserotateSpeed;

        player.transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }

    private void Jump()
    {
      
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != null)
        {
            isGround = true;
            jumpCount = maxJump;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag != null)
        {
            isGround = false;
        }
    }


    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * dashSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * dashSpeed);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * dashSpeed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * dashSpeed);
            }
        }
    }


    #region 애니메이션
    private void AnimaitorPlay()
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
        else
        {
            checkAniHash = idleHash;
        }

        if (rigid.velocity.y > 0.01f)
        {
            checkAniHash = jumpUpHash;
        }
        else if (rigid.velocity.y < -0.01f)
        {
            checkAniHash = jumpDownHash;
        }

        if (hp <= 0)
        {
            checkAniHash = dieHash;
        }



        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }
    #endregion
}
