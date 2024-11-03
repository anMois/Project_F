using UnityEngine;

public class Teleport : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("텔레포트 ui 진입 전");
        UIManager.Instance.ShowUI("Next Stage Canvas");
        Debug.Log("텔레포트 ui 진입 후");
    }
}
