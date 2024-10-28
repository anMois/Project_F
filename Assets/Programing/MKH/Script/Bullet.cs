using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerAttack obj;

    float speed = 3.0f;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


}
