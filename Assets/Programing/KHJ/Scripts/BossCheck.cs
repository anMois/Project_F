using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheck : MonoBehaviour
{
    private BossDragon dragon;

    private void Awake()
    {
        dragon = transform.parent.parent.GetComponent<BossDragon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") == false)
            return;

        dragon.IsTargetBehind = true;
    }
}
