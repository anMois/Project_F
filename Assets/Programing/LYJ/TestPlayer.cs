using System.Collections;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        InGameItemReference itemReference = other.GetComponent<InGameItemReference>();

        if (itemReference != null)
        {

            Destroy(other.gameObject);

            StartCoroutine(ShowStageClearUI());

        }
    }

    private IEnumerator ShowStageClearUI()
    {
        UIManager.Instance.ShowUI("Stage Clear Ani Canvas");

        yield return new WaitForSeconds(2f);

        UIManager.Instance.HideUI("Stage Clear Ani Canvas");

        UIManager.Instance.ShowUI("Stage Clear Canvas");
    }
}
