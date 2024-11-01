using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    private GameObject storeCanvas;
    private GameObject explanationCanvas;
    private Button storeExitButton;
    private bool isStoreInitialized = false; //���� �ʱ�ȭ ����

    private void Start()
    {
        storeCanvas = UIManager.Instance.GetUICanvas("Store Canvas");
        explanationCanvas = UIManager.Instance.GetUICanvas("Explanation Canvas");

        storeCanvas.SetActive(false);
        explanationCanvas.SetActive(false);

        storeExitButton = storeCanvas.transform.Find("Store Exit Button")?.GetComponent<Button>();
        if (storeExitButton != null)
        {
            storeExitButton.onClick.AddListener(StoreExitButtonClick);
        }
    }

    private void Update()
    {
        if (explanationCanvas.activeSelf && Input.GetMouseButtonDown(0))
        {
            explanationCanvas.SetActive(false);
        }
    }

    /// <summary>
    /// ����â���� ������ ������ ���
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
    /// ���� Exit ��ư Ŭ����
    /// </summary>
    public void StoreExitButtonClick()
    {
        storeCanvas.SetActive(false);
    }

    /// <summary>
    /// ���� Canvas Ȱ��ȭ
    /// </summary>
    public void ShowStoreCanvas()
    {
        storeCanvas.SetActive(true);

        //�� ���� CSV �ٿ�ε� ����
        if (!isStoreInitialized)
        {
            CSVDownload csvDownload = FindObjectOfType<CSVDownload>();
            if (csvDownload != null)
            {
                StartCoroutine(csvDownload.DownloadRoutine());
            }
            isStoreInitialized = true;
        }
    }
}