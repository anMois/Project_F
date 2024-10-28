using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public ItemData itemData;
    private MainController mainController;
    private Button button;

    private void Start()
    {
        mainController = FindObjectOfType<MainController>();
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        if (mainController != null && itemData != null)
        {
            mainController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }

        if (mainController != null)
        {
            if (itemData != null)
            {
                mainController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
            }
        }
    }
}
