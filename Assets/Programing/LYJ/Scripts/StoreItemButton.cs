using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour
{
    public ItemData itemData;
    private StoreController storeController;
    private Button button;
    private Button itemBuyButton;
    private Button potionButton;
    private Button grenadeButton;

    private bool isPurchased = false;

    private void Start()
    {
        storeController = FindObjectOfType<StoreController>();
        button = GetComponent<Button>();
        itemBuyButton = transform.Find("Item Buy Button")?.GetComponent<Button>();
        potionButton = GameObject.Find("Potion Buy Button").GetComponent<Button>();
        grenadeButton = GameObject.Find("Grenade Buy Button").GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }

        if (itemBuyButton != null)
        {
            itemBuyButton.onClick.AddListener(ItemBuyButtonClick);
        }

        if (potionButton != null)
        {
            potionButton.onClick.RemoveAllListeners();
            potionButton.onClick.AddListener(() => PotionGrenadeBuyButtonClick(potionButton));
        }

        if (grenadeButton != null)
        {
            grenadeButton.onClick.RemoveAllListeners();
            grenadeButton.onClick.AddListener(() => PotionGrenadeBuyButtonClick(grenadeButton));
        }
    }

    public void OnButtonClick()
    {
        if (storeController != null && itemBuyButton != null && itemData != null)
        {
            storeController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage);
        }
    }

    public void ItemBuyButtonClick()
    {
        if (isPurchased)
        {
            Debug.Log($"{itemData.itemName}은 이미 구매되었습니다.");
            return;
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
        itemBuyButton.interactable = false;
    }

    public void PotionGrenadeBuyButtonClick(Button clickedButton)
    {
        clickedButton.interactable = false;

        if (GameManager.Instance.PotionGrenadeItem(1000))
        {
            Debug.Log($"구매 완료");
            Debug.Log($"남은 돈: {GameManager.Instance.curPrice}");
        }
        else
        {
            Debug.Log("돈이 부족하여 구매할 수 없습니다.");
            Debug.Log($"남은 돈: {GameManager.Instance.curPrice}");
            clickedButton.interactable = true;
        }
    }
}
