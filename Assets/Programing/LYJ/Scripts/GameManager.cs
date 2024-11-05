using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static StageManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public float curPrice = 0f;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] TextMeshProUGUI gainPriceText;

    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxHealth = 100f;

    [SerializeField] private TextMeshProUGUI potionCountText;
    [SerializeField] private TextMeshProUGUI grenadeCountText;
    private int potionCount = 0;
    private int grenadeCount = 0;
    private const int maxItemCount = 3;

    [SerializeField] private TextMeshProUGUI stageNumberText;
    [SerializeField] private TextMeshProUGUI waveNumberText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //씬이 로드될 때 호출되는 초기화 메소드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGameState();
    }

    private void ResetGameState()
    {
        //필요한 값을 초기화 (아래 내용을 게임 필요에 따라 설정)
        potionCount = 0;
        grenadeCount = 0;
        curPrice = 0; //초기 골드 값
        currentHealth = maxHealth;

        UpdateUI();
        UpdatePriceText();
        UpdateHealthUI();
    }

    private void Start()
    {
        InitializeHealth();
        UpdatePriceText();
        UpdateUI();
    }

    private void InitializeHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (Input.GetKeyDown(KeyCode.E))
        {
            UsePotion();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseGrenade();
        }
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
        Debug.Log("돈이 부족하여 구매할 수 없습니다.");
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
        if (priceText != null && goldText != null)
        {
            priceText.text = $"보유 골드: {curPrice} G";
            goldText.text = $"보유 골드: {curPrice} G";
        }
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

    // 아이템 수집 기능 (플레이어.cs에 붙여 사용할 때 지우고 사용)
    //private void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.CompareTag("Potion"))
    //    {
    //        potionCount++;
    //        UpdateUI();
    //        Destroy(collider.gameObject);
    //    }
    //    else if (collider.CompareTag("Grenade"))
    //    {
    //        grenadeCount++;
    //        UpdateUI();
    //        Destroy(collider.gameObject);
    //    }
    //}

    private void UpdateUI()
    {
        if (potionCountText != null)
            potionCountText.text = $"{potionCount}";

        if (grenadeCountText != null)
            grenadeCountText.text = $"{grenadeCount}";
    }

    public void IncrementPotionCount()
    {
        if (potionCount <= maxItemCount)
        {
            potionCount++;
            UpdateUI();
        }
    }

    public void IncrementGrenadeCount()
    {
        if (grenadeCount <= maxItemCount)
        {
            grenadeCount++;
            UpdateUI();
        }
    }

    public int GetPotionCount() => potionCount;
    public int GetGrenadeCount() => grenadeCount;

    /// <summary>
    /// 피해를 받는 메서드
    /// </summary>
    /// <param name="damageAmount"></param>
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthUI();
        if (currentHealth <= 100)
        {
            UIManager.Instance.ShowUI("Game Over Canvas");
            Debug.Log("플레이어가 사망했습니다.");
        }
    }

    /// <summary>
    /// 체력을 회복하는 메서드
    /// </summary>
    /// <param name="percentage">회복할 비율 (0~1) 0.7 = 70% </param>
    public void Heal(float percentage)
    {
        currentHealth = maxHealth * percentage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        Debug.Log($"체력이 {percentage * 100}%로 회복되었습니다.");
    }

    private void UpdateGainedGoldText(float amount)
    {
        if (gainPriceText != null)
        {
            gainPriceText.text = $"{amount} G";
        }
    }

    /// <summary>
    /// 골드를 추가
    /// </summary>
    /// <param name="amount">골드 양</param>
    public void AddGold(float amount)
    {
        curPrice += amount;
        UpdatePriceText();
        UpdateGainedGoldText(amount);
    }
    
    public void UsePotion()
    {
        if (potionCount > 0)
        {
            if (currentHealth < maxHealth)
            {
                float healthToRestore = maxHealth * 0.5f;
                currentHealth += healthToRestore;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                potionCount--;
                UpdateUI();
                UpdateHealthUI();
                Debug.Log($"체력이 회복되었습니다. 현재 체력: {currentHealth}");
            }
            else
            {
                Debug.Log("체력이 이미 최대입니다. 포션을 사용할 수 없습니다.");
            }
        }
        else
        {
            Debug.Log("포션이 없습니다.");
        }
    }

    public void DecrementPotionCount()
    {
        if (potionCount > 0)
        {
            potionCount--;
            UpdateUI();
        }
    }

    public void UseGrenade()
    {
        if (grenadeCount > 0)
        {
            grenadeCount--;
            UpdateUI();
            // 수류탄 사용 코드 추가
            Debug.Log("수류탄을 사용했습니다.");
        }
        else
        {
            Debug.Log("수류탄이 없습니다.");
        }
    }
        
    /// <summary>
    /// 스테이지 정보
    /// </summary>
    /// <param name="stage">현재 스테이지</param>
    /// <param name="curWave">현재 웨이브</param>
    /// <param name="fullWave">해당 스테이지 전체 웨이브</param>
    public void StageWaveText(int stage, int curWave, int fullWave, StageState curState, StageState preState)
    {
        stageNumberText.text = $"{stage} Stage";
        if (curState == StageState.Normal || curState == StageState.Elite)
            waveNumberText.text = $"{curWave} / {fullWave} Wave";
        else if(preState == StageState.Store)
            waveNumberText.text = $"{curState} Wave";
    }
}
