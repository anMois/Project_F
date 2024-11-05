using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTag : MonoBehaviour
{
    [SerializeField] string tagString;

    private void Awake()
    {
        SetChildrenTag(tagString);
    }

    private void SetChildrenTag(string tagString)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(transform);

        while (queue.Count > 0)
        {
            Transform child = queue.Dequeue();
            for (int i = 0; i < child.childCount; i++)
            {
                queue.Enqueue(child.GetChild(i));
            }

            child.tag = tagString;
        }
    }

}
