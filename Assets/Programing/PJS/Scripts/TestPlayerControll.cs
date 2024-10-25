using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerControll : MonoBehaviour
{
    [SerializeField] Transform cameraTrans;
    [SerializeField] Rigidbody rigid;
    [SerializeField] float speed;

    private void Start()
    {
        cameraTrans = Camera.main.transform;
    }

    private void Update()
    {
        MoveTest();
    }

    private void MoveTest()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, z);

        rigid.velocity = dir.normalized * speed;
    }
}
