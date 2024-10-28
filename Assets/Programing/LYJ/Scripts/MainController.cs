using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject bookCanvas;
    private GameObject explanationCanvas;
    private GameObject manualCanvas;

    private Button newGameButton;
    private Button itemBookButton;
    private Button manualButton;
    private Button bookExitButton;
    private Button manualExitButton;
    private Button exitButton;

    private void Start()
    {
        mainCanvas = GameObject.Find("Main Canvas");
        bookCanvas = GameObject.Find("Book Canvas");
        explanationCanvas = GameObject.Find("Explanation Canvas");
        manualCanvas = GameObject.Find("Manual Canvas");

        newGameButton = GameObject.Find("New Game Button").GetComponent<Button>();
        itemBookButton = GameObject.Find("Item Book Button").GetComponent<Button>();
        manualButton = GameObject.Find("Manual Button").GetComponent<Button>();
        bookExitButton = GameObject.Find("Book Exit Button").GetComponent<Button>();
        manualExitButton = GameObject.Find("Manual Exit Button").GetComponent<Button>();
        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();
        
        InitializeCanvases();
        InitializeButtons();
    }

    private void InitializeCanvases()
    {
        mainCanvas.SetActive(true);
        bookCanvas.SetActive(false);
        explanationCanvas.SetActive(false);
        manualCanvas.SetActive(false);
    }

    private void InitializeButtons()
    {
        if (newGameButton) newGameButton.onClick.AddListener(ChangeStage1Scene);
        if (itemBookButton) itemBookButton.onClick.AddListener(ClickBookButton);
        if (manualButton) manualButton.onClick.AddListener(ClickManualButton);
        if (bookExitButton) bookExitButton.onClick.AddListener(BookExitButton);
        if (manualExitButton) manualExitButton.onClick.AddListener(ManualExitButton);
        if (exitButton) exitButton.onClick.AddListener(ExitButton);
    }

    private void Update()
    {
        if (explanationCanvas != null && explanationCanvas.activeSelf && Input.GetMouseButtonDown(0))
        {
            Explanation();
        }
    }

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

    public void Explanation()
    {
        explanationCanvas.SetActive(false);
    }

    public void ChangeStage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ClickBookButton()
    {
        CurrentCanvasState(mainCanvas, bookCanvas);
    }

    public void BookExitButton()
    {
        CurrentCanvasState(bookCanvas, mainCanvas);
    }

    public void ClickManualButton()
    {
        CurrentCanvasState(mainCanvas, manualCanvas);
    }

    public void ManualExitButton()
    {
        CurrentCanvasState(manualCanvas, mainCanvas);
    }

    private void CurrentCanvasState(GameObject toHide, GameObject toShow)
    {
        toHide.SetActive(false);
        toShow.SetActive(true);
    }

    public void ExitButton()
    {
        //유니티 에디터에서는 Application.Quit();를 사용할 수 없음 테스트 용도로 아래 코드 사용
        UnityEditor.EditorApplication.isPlaying = false;

        //빌드해서 사용할 때는 아래 코드를 사용
        //Application.Quit();    
    }
}
