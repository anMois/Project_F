using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public ItemData itemData;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image itemImage;

    private GameObject explanationCanvas;

    private void Start()
    {
        explanationCanvas = GameObject.Find("Explanation Canvas");

        if  (explanationCanvas != null )
        {
            explanationCanvas.SetActive(false);
        }

    }

    public void OnButtonClick()
    {
        if (explanationCanvas != null)
        {
            explanationCanvas.SetActive(true);
        }

        itemNameText.text = itemData.itemName;
        descriptionText.text = itemData.description;
        itemImage.sprite = itemData.itemImage;
    }
}
