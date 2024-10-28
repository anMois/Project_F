using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour
{
    public ItemData itemData;
    private StoreController storeController;
    [SerializeField] Button button;

    private void Start()
    {
        storeController = FindObjectOfType<StoreController>();
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        if (storeController != null && itemData != null)
        {
            storeController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }
}
