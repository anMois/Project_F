using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test_StartZone : MonoBehaviour
{
    public UnityAction OnAreaOut;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnAreaOut?.Invoke();
        }
    }
}
