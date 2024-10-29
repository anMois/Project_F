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
    private bool isStoreInitialized = false; // 상점 초기화 여부

    private void Start()
    {
        storeCanvas = GameObject.Find("Store Canvas");
        explanationCanvas = GameObject.Find("Explanation Canvas");
        storeCanvas.SetActive(false);
        explanationCanvas.SetActive(false);
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

    /// <summary>
    /// 상점 Canvas 활성화
    /// </summary>
    public void ShowStoreCanvas()
    {
        storeCanvas.SetActive(true);

        //한 번만 CSV 다운로드 시작
        if (!isStoreInitialized)
        {
            CSVDownload csvDownload = FindObjectOfType<CSVDownload>();
            if (csvDownload != null)
            {
                StartCoroutine(csvDownload.DownloadRoutine());
            }
            isStoreInitialized = true;
        }

        if (storeExitButton == null && storeCanvas.activeSelf)
        {
            storeExitButton = GameObject.Find("Store Exit Button")?.GetComponent<Button>();

            if (storeExitButton != null)
            {
                storeExitButton.onClick.AddListener(StoreExitButtonClick);
            }
        }
    }
}
