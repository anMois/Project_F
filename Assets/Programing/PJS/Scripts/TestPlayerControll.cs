using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerControll : MonoBehaviour
{
    [SerializeField] Transform camTrans;
    [SerializeField] Rigidbody rigid;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    [SerializeField] float mouseX;
    [SerializeField] float mouseY;

    private void Start()
    {
        camTrans = Camera.main.transform;
    }

    private void Update()
    {
        MoveTest();
        MouseRotate();
    }

    private void MoveTest()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, z);

        rigid.velocity = dir.normalized * speed;
    }

    private void MouseRotate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, rotateSpeed * mouseX * Time.deltaTime);
        camTrans.Rotate(Vector3.right, rotateSpeed * -mouseY * Time.deltaTime);


    }
}
