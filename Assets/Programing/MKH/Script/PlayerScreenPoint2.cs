using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScreenPoint2 : MonoBehaviour
{
    [SerializeField] float xAngle;
    [SerializeField] float yAngle;
    [SerializeField] float speed;
    [SerializeField] GameObject player;

    [SerializeField] Animator animator;
    [SerializeField] Transform playerSpine;
    [SerializeField] GameObject cameraHolder;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerSpine = animator.GetBoneTransform(HumanBodyBones.Spine);
    }

    private void LateUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        xAngle -= Input.GetAxis("Mouse Y") * speed;
        yAngle += Input.GetAxis("Mouse X") * speed;

        transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);

        player.transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }

    private void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * speed);

        xAngle += Input.GetAxisRaw("Mouse Y") * speed;
        xAngle = Mathf.Clamp(xAngle, -40f, 80f);

        cameraHolder.transform.localEulerAngles = Vector3.left * xAngle;
    }
}
