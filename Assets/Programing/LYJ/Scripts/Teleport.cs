using UnityEngine;

public class Teleport : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        UIManager.Instance.ShowUI("Next Stage Canvas");
    }
}
