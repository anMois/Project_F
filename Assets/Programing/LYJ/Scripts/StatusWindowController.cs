using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusWindowController : MonoBehaviour
{
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

    private void Start()
    {
        UpdateUI();
        UpdateDisplayImage();
    }

    private void OnTriggerEnter(Collider other)
    {
        InGameItemReference itemReference = other.GetComponent<InGameItemReference>();

        if (itemReference != null)
        {
            InGameItem item = itemReference.item;
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

            UpdateUI();
            UpdateDisplayImage();
            Destroy(other.gameObject);
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
}
