using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour
{
    public ItemData itemData;
    private InGameController inGameController;
    private StatusWindowController statusWindowController;
    [SerializeField] Button button; //이름 쓰여져 있는 (설명창 활성화)
    [SerializeField] Button itemBuyButton;
    [SerializeField] Button potionButton;
    [SerializeField] Button grenadeButton;

    private bool isPurchased = false; //구매가 되었는지 확인

    private void OnEnable()
    {
        InitializeButtons();
        ResetButtonStates();
    }

    private void Start()
    {
        inGameController = FindObjectOfType<InGameController>();
        statusWindowController = StatusWindowController.Instance;
    }

    private void InitializeButtons()
    {
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
            potionButton.onClick.RemoveAllListeners(); //버튼 리스너 중복 방지 (여러번 호출되는 오류 해결)
            potionButton.onClick.AddListener(() => PotionGrenadeBuyButtonClick(potionButton));
        }

        if (grenadeButton != null)
        {
            grenadeButton.onClick.RemoveAllListeners(); //버튼 리스너 중복 방지 (여러번 호출되는 오류 해결)
            grenadeButton.onClick.AddListener(() => PotionGrenadeBuyButtonClick(grenadeButton));
        }
    }

    private void ResetButtonStates()
    {
        isPurchased = false;
        if (itemBuyButton != null)
        {
            itemBuyButton.interactable = true;
        }
        if (potionButton != null)
        {
            potionButton.interactable = true;
        }
        if (grenadeButton != null)
        {
            grenadeButton.interactable = true;
        }
    }


    /// <summary>
    /// 현재 스크립트를 가지고 있는 버튼 클릭시 데이터를 가져옴
    /// </summary>
    public void OnButtonClick()
    {
        if (inGameController != null && itemBuyButton != null && itemData != null)
        {
            SoundManager.Instance.ButtonClickSound();
            inGameController.ShowExplanation(itemData.itemName, itemData.description, itemData.itemImage, itemData.elemental);
        }
    }

    /// <summary>
    /// 아이템 구매 버튼 클릭시 - 포션 수류탄 X
    /// </summary>
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
            if (statusWindowController != null)
            {
                statusWindowController.AddItemToInventory(itemData.itemImage, itemData.itemName, itemData.description, itemData.elemental);
            }
            SoundManager.Instance.BuyItemSound();
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
        itemBuyButton.interactable = false; //버튼 비활성화
    }

    /// <summary>
    /// 포션, 수류탄 구매 버튼 클릭시
    /// </summary>
    /// <param name="clickedButton"></param>
    public void PotionGrenadeBuyButtonClick(Button clickedButton)
    {
        clickedButton.interactable = false;

        //버튼 클릭 시 포션과 수류탄 가격에 따라 구매 여부를 결정
        if (clickedButton == potionButton)
        {
            if (GameManager.Instance.PotionGrenadeItem(1000))
            {
                SoundManager.Instance.BuyItemSound();
                GameManager.Instance.IncrementPotionCount();
                Debug.Log($"포션 구매 완료");
                Debug.Log($"남은 돈: {GameManager.Instance.curPrice}");
            }
            else
            {
                Debug.Log("돈이 부족하여 포션을 구매할 수 없습니다.");
                Debug.Log($"남은 돈: {GameManager.Instance.curPrice}");
                clickedButton.interactable = true;
            }
        }
        else if (clickedButton == grenadeButton)
        {
            if (GameManager.Instance.PotionGrenadeItem(1000))
            {
                SoundManager.Instance.BuyItemSound();
                GameManager.Instance.IncrementGrenadeCount();
                Debug.Log($"수류탄 구매 완료");
                Debug.Log($"남은 돈: {GameManager.Instance.curPrice}");
            }
            else
            {
                Debug.Log("돈이 부족하여 수류탄을 구매할 수 없습니다.");
                Debug.Log($"남은 돈: {GameManager.Instance.curPrice}");
                clickedButton.interactable = true;
            }
        }
    }

}
