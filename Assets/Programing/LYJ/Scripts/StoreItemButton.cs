using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour
{
    public ItemData itemData;
    private StoreController storeController;
    [SerializeField] Button button;
    [SerializeField] Button itemBuyButton;

    private bool isPurchased = false;

    private void Start()
    {
        storeController = FindObjectOfType<StoreController>();
        button = GetComponent<Button>();
        itemBuyButton = transform.Find("Item Buy Button").GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (itemBuyButton != null)
        {
            itemBuyButton.onClick.AddListener(ItemBuyButtonClick);
        }
    }

    public void OnButtonClick()
    {
        if (storeController != null && itemData != null)
        {
            storeController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }

    public void ItemBuyButtonClick()
    {
        if (isPurchased) // 이미 구매한 경우
        {
            Debug.Log($"{itemData.itemName}은 이미 구매되었습니다.");
            return; // 구매하지 않도록 조기 리턴
        }

        bool success = GameManager.Instance.PurchaseItem(itemData.price);
        if (success)
        {
            Debug.Log($"{itemData.itemName}을(를) 구매했습니다.");
            Debug.Log($"남은 돈: {GameManager.Instance.curPrice}");
            isPurchased = true;
            UpdateButtonState();
        }
        else
        {
            Debug.Log("돈이 부족하여 구매할 수 없습니다.");
            Debug.Log($"남은 돈: {GameManager.Instance.curPrice}");
        }
    }

    private void UpdateButtonState()
    {
        ColorBlock colors = itemBuyButton.colors;
        colors.normalColor = Color.gray;
        colors.disabledColor = Color.gray;
        itemBuyButton.colors = colors;
        itemBuyButton.interactable = false;
    }
}
