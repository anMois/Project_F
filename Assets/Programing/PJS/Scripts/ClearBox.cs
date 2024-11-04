using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBox : MonoBehaviour, IInteractable
{
    [SerializeField] bool isOpen;
    public bool IsOpen { get { return isOpen; } set { isOpen = value; } }
    public void Interact()
    {
        StartCoroutine(ShowStageClearUI());
    }

    IEnumerator ShowStageClearUI()
    {
        UIManager.Instance.ShowUI("Stage Clear Ani Canvas");
        
        yield return new WaitForSeconds(2f);

        UIManager.Instance.HideUI("Stage Clear Ani Canvas");

        UIManager.Instance.ShowUI("Stage Clear Canvas");
        yield break;
    }
}
