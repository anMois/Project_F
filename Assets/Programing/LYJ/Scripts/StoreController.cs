using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    private GameObject storeCanvas;
    private GameObject explanationCanvas;
    private Button storeExitButton;

    private void Start()
    {
        storeCanvas = GameObject.Find("Store Canvas");
        explanationCanvas = GameObject.Find("Explanation Canvas");
        storeCanvas.SetActive(true);   
        explanationCanvas.SetActive(false);

        storeExitButton = GameObject.Find("Store Exit Button").GetComponent<Button>();

        if (storeExitButton != null)
        {
            storeExitButton.onClick.AddListener(StoreExitButtonClick);
        }
    }

    private void Update()
    {
        if (explanationCanvas != null && explanationCanvas.activeSelf && Input.GetMouseButtonDown(0))
        {
            explanationCanvas.SetActive(false);
        }
    }

    /// <summary>
    /// 설명창에서 아이템 정보를 출력
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="description"></param>
    /// <param name="itemImage"></param>
    public void ShowExplanation(string itemName, string description, Sprite itemImage)
    {
        explanationCanvas.SetActive(true);

        TextMeshProUGUI itemNameText = explanationCanvas.transform.Find("Item Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = explanationCanvas.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        Image itemImageComponent = explanationCanvas.transform.Find("Description Image").GetComponent<Image>();

        itemNameText.text = itemName;
        descriptionText.text = description;
        itemImageComponent.sprite = itemImage;
    }

    /// <summary>
    /// 상점 Exit 버튼 클릭시
    /// </summary>
    public void StoreExitButtonClick()
    {
        storeCanvas.SetActive(false);
    }
}
