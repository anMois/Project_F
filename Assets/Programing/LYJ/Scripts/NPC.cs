using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private StoreController storeController;

    private void Start()
    {
        storeController = FindObjectOfType<StoreController>();
    }

    public void Interact()
    {
        if (storeController != null)
        {
            storeController.ShowStoreCanvas();
        }
    }
}