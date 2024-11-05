using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAttackRange : MonoBehaviour
{
    public UnityAction<IDamageable> OnDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == false)
            return;

        IDamageable target = other.gameObject.GetComponent<IDamageable>();
        OnDetected?.Invoke(target);
    }
}
