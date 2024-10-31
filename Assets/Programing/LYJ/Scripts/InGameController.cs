using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    [Header("StoreController")]
    private GameObject storeCanvas;
    private GameObject explanationCanvas;
    private Button storeExitButton;
    private bool isStoreInitialized = false; //상점 초기화 여부

    [Header("Stage Clear")]
    private GameObject gameClearCanvas;
    private GameObject nextStageCanvas;
    private Button continueButton;
    private Button oneStage;
    private Button twoStage;
    private Button threeStage;


    private void Start()
    {
        storeCanvas = UIManager.Instance.GetUICanvas("Store Canvas");
        explanationCanvas = UIManager.Instance.GetUICanvas("Explanation Canvas");
        gameClearCanvas = UIManager.Instance.GetUICanvas("Stage Clear Canvas");
        nextStageCanvas = UIManager.Instance.GetUICanvas("Next Stage Canvas");

        storeCanvas.SetActive(false);
        explanationCanvas.SetActive(false);
        gameClearCanvas.SetActive(false);
        nextStageCanvas.SetActive(false);

        storeExitButton = storeCanvas.transform.Find("Store Exit Button")?.GetComponent<Button>();
        continueButton = gameClearCanvas.transform.Find("Continue Button")?.GetComponent<Button>();
        oneStage = nextStageCanvas.transform.Find("Option1_Button")?.GetComponent<Button>();
        twoStage = nextStageCanvas.transform.Find("Option2_Button")?.GetComponent<Button>();
        threeStage = nextStageCanvas.transform.Find("Option3_Button")?.GetComponent<Button>();

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
    }

    private void Update()
    {
        if (explanationCanvas.activeSelf && Input.GetMouseButtonDown(0))
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
    }

    public void NextStageCanvasActive()
    {
        gameClearCanvas.SetActive(false);
        nextStageCanvas.SetActive(true);
    }

    public void StageMove(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                // 첫 번째 스테이지로 이동
                Debug.Log("첫 번째 스테이지로 이동합니다.");
                break;
            case 2:
                Debug.Log("두 번째 스테이지로 이동합니다.");
                break;
            case 3:
                Debug.Log("세 번째 스테이지로 이동합니다.");
                break;
        }
    }
}
