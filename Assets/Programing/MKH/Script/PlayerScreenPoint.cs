using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScreenPoint : MonoBehaviour
{
    [SerializeField] float yAngle;
    [SerializeField] float mouserotateSpeed;

    private void Update()
    {
        ViewRotate();
    }
    private void ViewRotate()
    {

        yAngle += Input.GetAxis("Mouse X") * mouserotateSpeed;

        transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }
}
