using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBullet : MonoBehaviour
{
    public enum Parent { Player, Monster}   // 누구의 투사체인지 정하기
    [SerializeField] Parent curPerant;
    public Parent CurPerant { set { curPerant = value; } }  //get도 필요하면 사용자가 작성
    [SerializeField] Rigidbody rigid;
    [SerializeField] float speed;

    private void Start()
    {
        rigid.velocity = Vector3.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 쏘고 몬스터가 맞은 경우
        if(collision.transform.CompareTag("Monster") && curPerant == Parent.Player)
        {
            // 몬스터가 피격되는 기능 작업
        }
        // 몬스터가 쏘고 플레이어가 맞은 경우
        else if(collision.transform.CompareTag("Player") && curPerant == Parent.Monster)
        {
            // 플레이어가 피격되는 기능 작업
        }

        // 벽에 맞은 경우
        Destroy(gameObject);
    }
}
