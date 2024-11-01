using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusWindowController : MonoBehaviour
{
    public static StatusWindowController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI flameCountText;
    [SerializeField] private TextMeshProUGUI iceCountText;
    [SerializeField] private TextMeshProUGUI electricityCountText;
    [SerializeField] private TextMeshProUGUI earthCountText;

    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI atsText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI ranText;
    [SerializeField] private TextMeshProUGUI spdText;

    [SerializeField] private int flameCount = 0;
    [SerializeField] private int iceCount = 0;
    [SerializeField] private int electricityCount = 0;
    [SerializeField] private int earthCount = 0;

    [SerializeField] private Image displayImage;
    [SerializeField] private Sprite[] elementalImages;

    [SerializeField] private List<Image> relicUIImages;
    private List<Sprite> relicSprites = new List<Sprite>();

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private Image itemImage;

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
        UIManager.Instance.HideUI("Status Window Explanation Canvas");

        UpdateUI();
        UpdateDisplayImage();

        foreach (var relicUIImage in relicUIImages)
        {
            relicUIImage.gameObject.SetActive(false);
            relicUIImage.GetComponent<Button>().onClick.AddListener(() => ShowExplanationCanvas());
        }
    }

    private void ShowExplanationCanvas()
    {
        UIManager.Instance.ShowUI("Status Window Explanation Canvas");
    }

    private void Update()
    {
        if (UIManager.Instance.IsUIActive("Status Window Explanation Canvas") && Input.GetMouseButtonDown(0))
        {
            UIManager.Instance.HideUI("Status Window Explanation Canvas");
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetData();
    }

    public void UpdateStartStatUI(StartItemData item)
    {
        atkText.text = $"{item.ATK}";
        atsText.text = $"{item.ATS}";
        defText.text = $"{item.DEF}";
        hpText.text = $"{item.HP}";
        ranText.text = $"{item.RAN}";
        spdText.text = $"{item.SPD}";
    }


    public void ChangeStat(string statName, int value)
    {
        string logMessage = $"{statName}: {value} 변화";

        switch (statName)
        {
            case "ATK":
                atkText.text = $"{int.Parse(atkText.text) + value}";
                break;
            case "ATS":
                int currentAts = int.Parse(atsText.text);
                int atsChange = Mathf.RoundToInt(currentAts * (value / 100f));
                atsText.text = $"{currentAts + atsChange}";
                break;
            case "DEF":
                defText.text = $"{int.Parse(defText.text) + value}";
                break;
            case "HP":
                hpText.text = $"{int.Parse(hpText.text) + value}";
                break;
            case "RAN":
                ranText.text = $"{int.Parse(ranText.text) + value}";
                break;
            case "SPD":
                int currentSpd = int.Parse(spdText.text);
                int spdChange = Mathf.RoundToInt(currentSpd * (value / 100f));
                spdText.text = $"{currentSpd + spdChange}";
                break;
        }
        Debug.Log(logMessage);
    }


    private void UpdateUI()
    {
        flameCountText.text = $"{flameCount}";
        iceCountText.text = $"{iceCount}";
        electricityCountText.text = $"{electricityCount}";
        earthCountText.text = $"{earthCount}";
    }

    private void UpdateDisplayImage()
    {
        int maxCount = Mathf.Max(flameCount, iceCount, electricityCount, earthCount);

        if (maxCount == 0)
        {
            displayImage.gameObject.SetActive(false);
            return;
        }
        else
        {
            displayImage.gameObject.SetActive(true);
        }

        ElementalType manyElement = ElementalType.Flame;

        if (maxCount == flameCount)
            manyElement = ElementalType.Flame;
        else if (maxCount == iceCount)
            manyElement = ElementalType.Ice;
        else if (maxCount == electricityCount)
            manyElement = ElementalType.Electricity;
        else if (maxCount == earthCount)
            manyElement = ElementalType.Earth;

        switch (manyElement)
        {
            case ElementalType.Flame:
                displayImage.sprite = elementalImages[0];
                break;
            case ElementalType.Ice:
                displayImage.sprite = elementalImages[1];
                break;
            case ElementalType.Electricity:
                displayImage.sprite = elementalImages[2];
                break;
            case ElementalType.Earth:
                displayImage.sprite = elementalImages[3];
                break;
        }
    }

    public void ResetData()
    {
        flameCount = 0;
        iceCount = 0;
        electricityCount = 0;
        earthCount = 0;
        relicSprites.Clear();
        UpdateUI();
        UpdateDisplayImage();

        foreach (var relicUIImage in relicUIImages)
        {
            if (relicUIImage != null)
            {
                relicUIImage.gameObject.SetActive(false);
            }
        }
    }

    public void AddItemToInventory(Sprite itemSprite)
    {
        foreach (var relicUIImage in relicUIImages)
        {
            if (!relicUIImage.gameObject.activeSelf)
            {
                relicUIImage.sprite = itemSprite;
                relicUIImage.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void AddRelicImageToList(Sprite itemSprite)
    {
        foreach (var relicUIImage in relicUIImages)
        {
            if (!relicUIImage.gameObject.activeSelf)
            {
                relicUIImage.sprite = itemSprite;
                relicUIImage.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void UpdateStatusWindow(string itemName, string itemDescription, Sprite itemSprite)
    {
        // 텍스트와 이미지를 업데이트합니다.
        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemImage.sprite = itemSprite; // itemImage는 Image 컴포넌트입니다.

        // 캔버스를 활성화합니다.
        // 만약 캔버스가 비활성화되어 있다면, 활성화하는 코드를 추가하세요.
        // 예를 들어:
        // explanationCanvas.SetActive(true);
    }
}
