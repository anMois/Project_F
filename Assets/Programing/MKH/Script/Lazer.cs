using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] public GameObject lazer;
    [SerializeField] float length;
    [SerializeField] float range;
    [SerializeField] float delay;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Next();
            Destroy(lazer, 5f);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Destroy(lazer);
        }
       
    }

    private void Next()
    {
        lazer.transform.localScale = new Vector3(1 + range, 1 + range, 1 + length);
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (length >= 0 && length < 3)
            {
                length++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (range >= 0 && range < 3)
            {
                range++;
            }
        }
    }

}
