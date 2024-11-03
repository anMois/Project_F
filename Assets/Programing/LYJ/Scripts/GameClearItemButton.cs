using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameClearItemButton : MonoBehaviour
{
    public ItemData itemData;
    [SerializeField] private InGameController inGameController;
    [SerializeField] private Button button; //사진 버튼 (설명창 활성화)
    [SerializeField] private Button itemBuyButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI itemBuyButtonText;

    private bool isPurchased = false;

    private static List<GameClearItemButton> allButtons = new List<GameClearItemButton>();

    private void Awake()
    {
        allButtons.Add(this);
    }

    private void OnDestroy()
    {
        allButtons.Remove(this);
    }

    private void Start()
    {
        inGameController = FindObjectOfType<InGameController>();
        button = GetComponent<Button>();
        itemBuyButton = transform.Find("Item Buy Button")?.GetComponent<Button>();
        buttonText = transform.Find("Button Text")?.GetComponent<TextMeshProUGUI>();
        itemBuyButtonText = transform.Find("Item Buy Button Text")?.GetComponent<TextMeshProUGUI>();

        if (button != null)
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

            button.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemName;
            itemBuyButton.GetComponent<Image>().sprite = itemData.itemImage;
        }
    }

    /// <summary>
    /// 버튼 클릭시 설명창 활성화
    /// </summary>
    public void OnButtonClick()
    {
        if (inGameController != null && itemBuyButton != null && itemData != null)
        {
            inGameController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage, itemData.elemental);
        }
    }

    /// <summary>
    /// 버튼 클릭시 아이템 구매 후 인벤토리 저장
    /// </summary>
    public void ItemBuyButtonClick()
    {
        if (!isPurchased && itemData != null)
        {
            StatusWindowController.Instance.AddItemToInventory(itemData.itemImage, itemData.itemName, itemData.description, itemData.elemental);
            isPurchased = true;

            if (inGameController != null)
            {
                inGameController.ActivateContinueButton();
            }

            Debug.Log($"{itemData.itemName}이(가) 인벤토리에 추가되었습니다.");

            DisableOtherButtons();
        }
    }

    //선택되지 않은 버튼 비활성화
    private void DisableOtherButtons()
    {
        foreach (var button in allButtons)
        {
            if (button != this && button.itemBuyButton != null)
            {
                button.itemBuyButton.interactable = false;
            }
        }
    }

    private void AddEventTrigger(GameObject target, UnityEngine.Events.UnityAction<BaseEventData> enterAction, UnityEngine.Events.UnityAction<BaseEventData> exitAction)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>() ?? target.AddComponent<EventTrigger>();
        EventTrigger.Entry enterEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        enterEntry.callback.AddListener(enterAction);
        trigger.triggers.Add(enterEntry);

        EventTrigger.Entry exitEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        exitEntry.callback.AddListener(exitAction);
        trigger.triggers.Add(exitEntry);
    }

    public void OnButtonHoverEnter(BaseEventData data)
    {
        buttonText.text = "상세보기";
        buttonText.gameObject.SetActive(true);

        itemBuyButtonText.gameObject.SetActive(false);
    }

    public void OnButtonHoverExit(BaseEventData data)
    {
        buttonText.gameObject.SetActive(false);
    }

    public void OnItemBuyButtonHoverEnter(BaseEventData data)
    {
        itemBuyButtonText.text = "선택하기";
        itemBuyButtonText.gameObject.SetActive(true);

        buttonText.gameObject.SetActive(false);
    }

    public void OnItemBuyButtonHoverExit(BaseEventData data)
    {
        itemBuyButtonText.gameObject.SetActive(false);
    }
}