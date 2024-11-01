using System.Collections;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        InGameItemReference itemReference = other.GetComponent<InGameItemReference>();

        if (itemReference != null)
        {
            // 아이템을 수집
            StatusWindowController.Instance.CollectItem(itemReference.item);

            Destroy(other.gameObject);

        }
    }
}
