using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public float curPrice = 10000f;
    [SerializeField] TextMeshProUGUI priceText;

    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 100f;

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
        InitializeHealth();
        UpdatePriceText();
    }

    private void InitializeHealth()
    {
        currentHealth = maxHealth; // 체력 초기화
        UpdateHealthUI(); // UI 업데이트
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

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float healthRatio = currentHealth / maxHealth;
        if (hpBar != null)
        {
            hpBar.fillAmount = healthRatio;
        }

        if (hpText != null)
        {
            hpText.text = $"{(int)currentHealth} / {maxHealth}";
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Debug.Log("플레이어가 사망했습니다.");
        }
    }
}
