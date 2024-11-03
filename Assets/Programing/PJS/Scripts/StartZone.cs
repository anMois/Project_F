using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartZone : MonoBehaviour
{
    public UnityAction OnAreaOut;

    private void Start()
    {
        GetComponent<SphereCollider>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            Debug.Log("³ª¿È");
            OnAreaOut?.Invoke();
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
