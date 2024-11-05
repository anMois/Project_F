using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusWindowController : MonoBehaviour
{
    public static StatusWindowController Instance { get; private set; }
    public static Action<WeaponType> OnWeaponTypeChanged;
    public PlayerAttack playerAttack;

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
    [SerializeField] private Image itemElementImage;

    [SerializeField] private Sprite[] itemElementalImage;

    private Dictionary<Sprite, (string itemName, string itemDescription)> itemInfoDict = new Dictionary<Sprite, (string, string)>();

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
        playerAttack = FindObjectOfType<PlayerAttack>();

        UpdateUI();
        UpdateDisplayImage();

        foreach (var relicUIImage in relicUIImages)
        {
            relicUIImage.gameObject.SetActive(false);

            relicUIImage.GetComponent<Button>().onClick.AddListener(() => ShowExplanationCanvas());
        }
    }

    private void Update()
    {
        if (UIManager.Instance.IsUIActive("Status Window Explanation Canvas") && Input.GetMouseButtonDown(0))
        {
            UIManager.Instance.HideUI("Status Window Explanation Canvas");
        }
    }

    private void ShowExplanationCanvas()
    {
        UIManager.Instance.ShowUI("Status Window Explanation Canvas");
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

        if (maxCount == flameCount)
        {
            displayImage.sprite = elementalImages[0];
        }
        else if (maxCount == iceCount)
        {
            displayImage.sprite = elementalImages[1];
        }
        else if (maxCount == electricityCount)
        {
            displayImage.sprite = elementalImages[2];
        }
        else if (maxCount == earthCount)
        {
            displayImage.sprite = elementalImages[3];
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

    private Dictionary<Image, (string itemName, string itemDescription, Sprite itemSprite, int elemental)> relicInfoDict
     = new Dictionary<Image, (string, string, Sprite, int)>();

    public void AddItemToInventory(Sprite itemSprite, string itemName, string itemDescription, int elemental)
    {
        if (!itemInfoDict.ContainsKey(itemSprite))
        {
            itemInfoDict[itemSprite] = (itemName, itemDescription);
        }

        foreach (var relicUIImage in relicUIImages)
        {
            if (!relicUIImage.gameObject.activeSelf)
            {
                relicUIImage.sprite = itemSprite;
                relicUIImage.gameObject.SetActive(true);

                //relicUIImage와 아이템 데이터를 함께 저장
                relicInfoDict[relicUIImage] = (itemName, itemDescription, itemSprite, elemental);

                relicUIImage.GetComponent<Button>().onClick.RemoveAllListeners();
                relicUIImage.GetComponent<Button>().onClick.AddListener(() => ShowExplanationCanvas(relicUIImage));

                switch (elemental)
                {
                    case 1:
                        flameCount++;
                        break;
                    case 2:
                        iceCount++;
                        break;
                    case 3:
                        electricityCount++;
                        break;
                    case 4:
                        earthCount++;
                        break;
                }

                UpdateUI();
                UpdateDisplayImage();
                UpdateWeapon();
                break;
            }
        }
    }

    private void ShowExplanationCanvas(Image relicUIImage)
    {
        if (relicInfoDict.TryGetValue(relicUIImage, out var itemInfo))
        {
            itemNameText.text = itemInfo.itemName;
            itemDescriptionText.text = itemInfo.itemDescription;
            itemImage.sprite = itemInfo.itemSprite;

            int elemental = itemInfo.elemental;
            if (elemental >= 1 && elemental <= itemElementalImage.Length)
            {
                itemElementImage.sprite = itemElementalImage[elemental - 1];
                itemElementImage.gameObject.SetActive(true);
            }
            else
            {
                itemElementImage.gameObject.SetActive(false);
            }
            SoundManager.Instance.ButtonClickSound();
            UIManager.Instance.ShowUI("Status Window Explanation Canvas");
        }
    }

    public List<Sprite> GetActiveRelicImages()
    {
        List<Sprite> activeRelicImages = new List<Sprite>();

        foreach (var relicUIImage in relicUIImages)
        {
            if (relicUIImage.gameObject.activeSelf)
            {
                activeRelicImages.Add(relicUIImage.sprite);
            }
        }

        return activeRelicImages;
    }

    public void UpdateWeapon()
    {
        int maxCount = Mathf.Max(flameCount, iceCount, electricityCount, earthCount);

        if (flameCount == maxCount)
        {
            OnWeaponTypeChanged?.Invoke(WeaponType.Flame);
        }
        else if (iceCount == maxCount)
        {
            OnWeaponTypeChanged?.Invoke(WeaponType.Ice);
        }
        else if (electricityCount == maxCount)
        {
            OnWeaponTypeChanged?.Invoke(WeaponType.Electricity);
        }
        else if (earthCount == maxCount)
        {
            OnWeaponTypeChanged?.Invoke(WeaponType.Earth);
        }
    }
}
