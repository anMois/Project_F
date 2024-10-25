using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public ItemData itemData;
    private MainController mainController;

    private void Start()
    {
        mainController = FindObjectOfType<MainController>();
    }

    public void OnButtonClick()
    {
        if (mainController != null)
        {
            mainController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }
}
