using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScreenPoint : MonoBehaviour
{
    [SerializeField] float yAngle;
    [SerializeField] public float mouserotateSpeed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        ViewRotate();
    }
    private void ViewRotate()
    {

        yAngle += Input.GetAxis("Mouse X") * mouserotateSpeed;

        transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }
}
