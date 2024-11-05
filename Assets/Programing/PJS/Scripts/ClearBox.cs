using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static StageManager;

public class ClearBox : MonoBehaviour, IInteractable
{
    [SerializeField] bool isOpen;
    [SerializeField] StageManager stageManager;

    private bool isClick;

    public bool IsOpen { get { return isOpen; } set { isOpen = value; } }
    public bool IsClick { set { isClick = value; } }

    public void Interact()
    {
        if(isClick == false)
        {
            StartCoroutine(ShowStageClearUI());

            if (stageManager.PreState == StageState.Elite)
            {
                Debug.Log("2400°ñµå È¹µæ");
                GameManager.Instance.AddGold(2400);
            }
            else
            {
                Debug.Log("1500°ñµå È¹µæ");
                GameManager.Instance.AddGold(1500);
            }
        }
    }

    IEnumerator ShowStageClearUI()
    {
        UIManager.Instance.ShowUI("Stage Clear Ani Canvas");

        yield return new WaitForSeconds(2f);

        UIManager.Instance.HideUI("Stage Clear Ani Canvas");

        UIManager.Instance.ShowUI("Stage Clear Canvas");
        isClick = true;
        yield break;
    }
}
