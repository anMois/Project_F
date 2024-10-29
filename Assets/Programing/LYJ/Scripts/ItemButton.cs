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

    /// <summary>
    /// ItemButton.cs 를 가지고 있는 버튼을 눌렀을 때
    /// 아이템 이름, 설명, 이미지를 가져옴
    /// </summary>
    public void OnButtonClick()
    {
        if (mainController != null && itemData != null)
        {
            mainController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }
}
