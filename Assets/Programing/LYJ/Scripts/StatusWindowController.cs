using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void Start()
    {
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        InGameItemReference itemReference = other.GetComponent<InGameItemReference>();

        if (itemReference != null)
        {
            InGameItem item = itemReference.item;
            item.ApplyEffect(this); //스크립터블 오브젝트의 효과 적용

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
            Destroy(other.gameObject);
        }
    }

    public void ChangeStat(string statName, int value)
    {
        // 스탯을 증가 또는 감소시킴
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
}
