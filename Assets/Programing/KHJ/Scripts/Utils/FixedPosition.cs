using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPosition : MonoBehaviour
{
    public Transform origin;

    private void LateUpdate()
    {
        if (origin != null)
        {
            // referenceTransform의 위치와 회전을 그대로 유지합니다.
            transform.position = origin.position;
            transform.rotation = origin.rotation;
        }
    }
}
