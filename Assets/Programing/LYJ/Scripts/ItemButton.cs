using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] Image bookItemImage;
    [SerializeField] TextMeshProUGUI bookItemName;

    public ItemData itemData;
    private GameController gameController;
    private Button button;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void Update()
    {
        bookItemImage.sprite = itemData.itemImage;
        bookItemName.text = itemData.itemName;
    }

    /// <summary>
    /// ItemButton.cs 를 가지고 있는 버튼을 눌렀을 때
    /// 아이템 이름, 설명, 이미지를 가져옴
    /// </summary>
    public void OnButtonClick()
    {
        SoundManager.Instance.ButtonClickSound();
        if (gameController != null && itemData != null)
        {
            gameController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }
}
