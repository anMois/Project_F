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

    [SerializeField] private int flameCount = 0;
    [SerializeField] private int iceCount = 0;
    [SerializeField] private int electricityCount = 0;
    [SerializeField] private int earthCount = 0;

    [SerializeField] private Image displayImage;
    [SerializeField] private Sprite[] elementalImages;

    [SerializeField] private List<Image> relicUIImages;
    private List<Sprite> relicSprites = new List<Sprite>();

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
        }
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

    public void CollectItem(InGameItemData item)
    {
        item.ApplyEffect(this);

        switch (item.elemental)
        {
            case ElementalType.Flame:
                flameCount++;
                break;
            case ElementalType.Ice:
                iceCount++;
                break;
            case ElementalType.Electricity:
                electricityCount++;
                break;
            case ElementalType.Earth:
                earthCount++;
                break;
        }

        AddRelicToInventory(item.itemImage);
        UpdateUI();
        UpdateDisplayImage();
    }


    private void AddRelicToInventory(Sprite itemSprite)
    {
        if (relicSprites.Count < 20)
        {
            relicSprites.Add(itemSprite);
            UpdateRelicUI();
        }
    }

    private void UpdateRelicUI()
    {
        for (int i = 0; i < relicUIImages.Count; i++)
        {
            if (i < relicSprites.Count && relicUIImages[i] != null)
            {
                relicUIImages[i].sprite = relicSprites[i];
                relicUIImages[i].gameObject.SetActive(true);

                int index = i;
                Button button = relicUIImages[i].GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => ShowRelicInfo(index));
                }
            }
            else if (relicUIImages[i] != null)
            {
                Debug.Log("비활성화되었습니다");
                relicUIImages[i].gameObject.SetActive(false);
            }
        }
    }


    private void ShowRelicInfo(int index)
    {
        UIManager.Instance.ShowUI("Status Window Explanation Canvas");

        var itemData = CSVDownload.Instance.itemDataList[index];
        if (itemData != null)
        {
            StatusWindowExplanationCanvas.Instance.SetExplanation(itemData);
        }
        else
        {
            Debug.LogError("아이템 데이터가 null입니다.");
        }
    }


    public void ChangeStat(string statName, int value)
    {
        switch (statName)
        {
            case "ATK":
                Debug.Log($"ATK {value} 변화");
                break;
            case "ATS":
                Debug.Log($"ATS {value} 변화");
                break;
            case "DEF":
                Debug.Log($"DEF {value} 변화");
                break;
            case "HP":
                Debug.Log($"HP {value} 변화");
                break;
            case "RAN":
                Debug.Log($"RAN {value} 변화");
                break;
            case "SPD":
                Debug.Log($"SPD {value} 변화");
                break;
        }
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

}
