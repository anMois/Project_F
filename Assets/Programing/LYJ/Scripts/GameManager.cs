using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public float curPrice = 10000f;
    [SerializeField] TextMeshProUGUI priceText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdatePriceText();
    }

    /// <summary>
    /// 아이템 구매 (돈 차감) - CSV로 받아온 아이템만 가능
    /// </summary>
    /// <param name="itemPrice"></param>
    /// <returns></returns>
    public bool PurchaseItem(float itemPrice)
    {
        if (curPrice >= itemPrice)
        {
            curPrice -= itemPrice;
            UpdatePriceText();
            return true;
        }
        return false;
    }
    /// <summary>
    /// 아이템 구매 - 포션, 수류탄 전용
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public bool PotionGrenadeItem(float price)
    {
        if (curPrice >= price)
        {
            curPrice -= price;
            UpdatePriceText();
            return true;
        }
        else
        {
            Debug.Log("돈이 부족하여 구매할 수 없습니다.");
            return false;
        }
    }

    private void UpdatePriceText()
    {
        if (priceText != null)
        {
            priceText.text = $"Possession Gold: {curPrice} G";
        }
    }
}
