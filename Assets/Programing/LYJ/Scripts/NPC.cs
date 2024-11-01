using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private InGameController storeController;

    private void Start()
    {
        storeController = FindObjectOfType<InGameController>();
    }

    public void Interact()
    {
        if (storeController != null)
        {
            storeController.ShowStoreCanvas();
        }
    }
}