using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameClearItemButton : MonoBehaviour
{
    public ItemData itemData;
    [SerializeField] InGameController inGameController;
    [SerializeField] Button button; //사진 버튼 (설명창 활성화)
    [SerializeField] Button itemBuyButton;

    private bool isPurchased = false;

    private void Start()
    {
        inGameController = FindObjectOfType<InGameController>();
        button = GetComponent<Button>();
        itemBuyButton = transform.Find("Item Buy Button")?.GetComponent<Button>();

        if (button != null )
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (itemBuyButton != null)
        {
            itemBuyButton.onClick.AddListener(ItemBuyButtonClick);
        }

        SetRandomItemData();
    }

    private void SetRandomItemData()
    {
        if (CSVDownload.Instance != null && CSVDownload.Instance.itemDataList.Count > 0)
        {
            int randomIndex = Random.Range(0, CSVDownload.Instance.itemDataList.Count);
            itemData = CSVDownload.Instance.itemDataList[randomIndex];

            // 아이템 데이터에 맞게 UI 업데이트
            button.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemName;
            itemBuyButton.GetComponent<Image>().sprite = itemData.itemImage;
        }
    }

    public void OnButtonClick()
    {
        if (inGameController != null && itemBuyButton != null && itemData != null)
        {
            inGameController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }

    public void ItemBuyButtonClick()
    {
        //인벤토리에 담겨져야 함
        Debug.Log($"{itemData.itemName}을(를) 선택하여 인벤토리에 저장되었습니다.");
    }

    private void UpdateButtonState()
    {
        itemBuyButton.interactable = false; //버튼 비활성화
    }
}
