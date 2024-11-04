using UnityEngine;

public class Teleport : MonoBehaviour, IInteractable
{
    [SerializeField] StageManager stageManager;

    public void Interact()
    {
        //if (stageManager.StageNum == stageManager.LastStage)
        //    UIManager.Instance.ShowUI("Boss Stage Canvas");
        //else
        //    UIManager.Instance.ShowUI("Next Stage Canvas");
    }
}