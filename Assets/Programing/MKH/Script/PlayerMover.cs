using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float moveSpeed;
    [SerializeField] float yAngle;
    [SerializeField] float mouserotateSpeed;
    [SerializeField] Animator animator;

    private static int idleHash = Animator.StringToHash("Idle03");
    private static int walkForwardHash = Animator.StringToHash("BattleWalkForward");
    private static int walkBackHash = Animator.StringToHash("BattleWalkBack");
    private static int walkRightHash = Animator.StringToHash("BattleWalkRight");
    private static int walkLeftHash = Animator.StringToHash("BattleWalkLeft");

    public int curAniHash { get; private set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        PlayerCamera();
        AnimaitorPlay();
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
    }

    private void PlayerCamera()
    {
        yAngle += Input.GetAxis("Mouse X") * mouserotateSpeed;

        player.transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }

    #region 애니메이션
    private void AnimaitorPlay()
    {
        int checkAniHash = 0;

        if (Input.GetKey(KeyCode.W))
        {
            animator.CrossFade(walkForwardHash, 0.2f);
            checkAniHash = walkForwardHash;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.CrossFade(walkBackHash, 0.2f);
            checkAniHash = walkBackHash;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.CrossFade(walkLeftHash, 0.2f);
            checkAniHash = walkLeftHash;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.CrossFade(walkRightHash, 0.2f);
            checkAniHash = walkRightHash;
        }
        //else if (Input.GetKey(KeyCode.ct)
        else
        {
            animator.CrossFade(idleHash, 0.2f);
            checkAniHash = idleHash;
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }
    #endregion
}
