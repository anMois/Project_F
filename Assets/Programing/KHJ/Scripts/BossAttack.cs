using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == false)
            return;

        Debug.Log($"[Dragon] {gameObject.name}!");
        IDamageable damageable = other.GetComponent<IDamageable>();
        damageable.TakeHit(damage);
    }
}
