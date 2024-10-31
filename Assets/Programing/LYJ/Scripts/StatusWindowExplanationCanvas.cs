using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusWindowExplanationCanvas : MonoBehaviour
{
    public static StatusWindowExplanationCanvas Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private Image itemImage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetExplanation(ItemData itemData)
    {
        itemNameText.text = itemData.itemName;
        itemDescriptionText.text = itemData.description;
        itemImage.sprite = itemData.itemImage;
    }
}
