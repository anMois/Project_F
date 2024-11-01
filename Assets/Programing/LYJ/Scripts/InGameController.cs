using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    [Header("StoreController")]
    private GameObject storeCanvas;
    private GameObject explanationCanvas;
    private Button storeExitButton;
    private bool isStoreInitialized = false; //���� �ʱ�ȭ ����

    [Header("Stage Clear")]
    private GameObject gameClearCanvas;
    private GameObject nextStageCanvas;
    private Button continueButton;
    private Button oneStage;
    private Button twoStage;
    private Button threeStage;
    private GameObject stageClearAniCanvas;

    [Header("Boss Canvas UI")]
    private GameObject bossStageCanvas;
    private Button bossStageMoveButton;


    private void Start()
    {
        storeCanvas = UIManager.Instance.GetUICanvas("Store Canvas");
        explanationCanvas = UIManager.Instance.GetUICanvas("Explanation Canvas");
        gameClearCanvas = UIManager.Instance.GetUICanvas("Stage Clear Canvas");
        nextStageCanvas = UIManager.Instance.GetUICanvas("Next Stage Canvas");
        bossStageCanvas = UIManager.Instance.GetUICanvas("Boss Stage Canvas");
        stageClearAniCanvas = UIManager.Instance.GetUICanvas("Stage Clear Ani Canvas");

        storeCanvas.SetActive(false);
        explanationCanvas.SetActive(false);
        gameClearCanvas.SetActive(false);
        nextStageCanvas.SetActive(false);
        bossStageCanvas.SetActive(false);
        stageClearAniCanvas.SetActive(false);

        storeExitButton = storeCanvas.transform.Find("Store Exit Button")?.GetComponent<Button>();
        continueButton = gameClearCanvas.transform.Find("Continue Button")?.GetComponent<Button>();
        oneStage = nextStageCanvas.transform.Find("Option1_Button")?.GetComponent<Button>();
        twoStage = nextStageCanvas.transform.Find("Option2_Button")?.GetComponent<Button>();
        threeStage = nextStageCanvas.transform.Find("Option3_Button")?.GetComponent<Button>();
        bossStageMoveButton = bossStageCanvas.transform.Find("Boss Stage Move Button")?.GetComponent<Button>();

        if (storeExitButton != null)
        {
            storeExitButton.onClick.AddListener(StoreExitButtonClick);
        }

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(NextStageCanvasActive);
        }

        if (oneStage != null)
        {
            oneStage.onClick.AddListener(() => StageMove(1));
        }
        if (twoStage != null)
        {
            twoStage.onClick.AddListener(() => StageMove(2));
        }
        if (threeStage != null)
        {
            threeStage.onClick.AddListener(() => StageMove(3));
        }

        if (bossStageMoveButton != null)
        {
            bossStageMoveButton.onClick.AddListener(() => StageMove(4));
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

    /// <summary>
    /// �������� Ŭ���� (���� ���� ��) ���� �������� ���� UI Ȱ��ȭ
    /// </summary>
    public void NextStageCanvasActive()
    {
        gameClearCanvas.SetActive(false);
        nextStageCanvas.SetActive(true);
    }

    /// <summary>
    /// ������ ���������� �̵�
    /// </summary>
    /// <param name="stageNumber">�̵��� �������� ��ȣ</param>
    public void StageMove(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                Debug.Log("ù ��° ���������� �̵��մϴ�.");
                break;
            case 2:
                Debug.Log("�� ��° ���������� �̵��մϴ�.");
                break;
            case 3:
                Debug.Log("�� ��° ���������� �̵��մϴ�.");
                break;
            case 4:
                Debug.Log("���� ���������� �̵��մϴ�.");
                break;
        }
    }
}