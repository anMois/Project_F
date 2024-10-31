using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Main Menu Buttons")]
    [SerializeField] Button newGameButton;
    [SerializeField] Button itemBookButton;
    [SerializeField] Button manualButton;
    [SerializeField] Button bookExitButton;
    [SerializeField] Button bookChangeButton;
    [SerializeField] Button book2ExitButton;
    [SerializeField] Button bookPageReturnButton;
    [SerializeField] Button manualExitButton;
    [SerializeField] Button exitButton;

    [Header("In-Game Buttons")]
    [SerializeField] Button returnGameButton;
    [SerializeField] Button inGameManualButton;
    [SerializeField] Button giveUpButton;
    [SerializeField] Button leaveGameButton;
    [SerializeField] Button inGameManualExitButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button inGameExitButton;

    [Header("Item Explanation UI")]
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image itemImageComponent;

    [Header("Status Window UI")]
    [SerializeField] Button statusWindowCloseButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeButtons();
    }


    private void InitializeButtons()
    {
        // Main Menu Buttons
        if (newGameButton) newGameButton.onClick.AddListener(ChangeStage1Scene);
        if (itemBookButton) itemBookButton.onClick.AddListener(ClickBookButton);
        if (manualButton) manualButton.onClick.AddListener(ClickManualButton);
        if (bookExitButton) bookExitButton.onClick.AddListener(BookExitButton);
        if (bookChangeButton) bookChangeButton.onClick.AddListener(BookChangeButton);
        if (book2ExitButton) book2ExitButton.onClick.AddListener(BookExitButton);
        if (bookPageReturnButton) bookPageReturnButton.onClick.AddListener(BookPageReturnButton);
        if (manualExitButton) manualExitButton.onClick.AddListener(ManualExitButton);
        if (exitButton) exitButton.onClick.AddListener(ExitButton);

        // In-Game Buttons
        if (returnGameButton) returnGameButton.onClick.AddListener(ClickReturnGameButton);
        if (inGameManualButton) inGameManualButton.onClick.AddListener(ClickInGameManualButton);
        if (giveUpButton) giveUpButton.onClick.AddListener(ClickGiveUpButton);
        if (leaveGameButton) leaveGameButton.onClick.AddListener(ClickLeaveGame);
        if (inGameManualExitButton) inGameManualExitButton.onClick.AddListener(ClickInGameManualExitButton);
        if (restartButton) restartButton.onClick.AddListener(ClickRestartButton);
        if (inGameExitButton) inGameExitButton.onClick.AddListener(ClickExitButton);

        // Status Window Button
        if (statusWindowCloseButton) statusWindowCloseButton.onClick.AddListener(ClickStatusWindowCloseButton);
    }

    private void Update()
    {
        if (UIManager.Instance.IsUIActive("Main Explanation Canvas") && Input.GetMouseButtonDown(0))
        {
            Explanation();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UIManager.Instance.IsUIActive("Main Canvas") && 
                !UIManager.Instance.IsUIActive("In Game Manual Canvas") &&
                !UIManager.Instance.IsUIActive("Start Item Canvas"))
            {
                ToggleInGameMenu();
            }
        }

        if (UIManager.Instance.IsUIActive("In Game Menu Canvas") || UIManager.Instance.IsUIActive("In Game Manual Canvas"))
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            ShowInGameUI();
        }
    }

    public void ShowInGameUI()
    {
        UIManager.Instance.ShowUI("Player HP Canvas");
        UIManager.Instance.ShowUI("Player Item Canvas");
    }

    private void ToggleInGameMenu()
    {
        bool isActive = UIManager.Instance.IsUIActive("In Game Menu Canvas");
        if (isActive)
        {
            UIManager.Instance.HideUI("In Game Menu Canvas");
        }
        else
        {
            UIManager.Instance.ShowUI("In Game Menu Canvas");
        }
    }

    public void ShowExplanation(string itemName, string description, Sprite itemImage)
    {
        UIManager.Instance.ShowUI("Main Explanation Canvas");

        itemNameText.text = itemName;
        descriptionText.text = description;
        itemImageComponent.sprite = itemImage;
    }

    public void Explanation()
    {
        UIManager.Instance.HideUI("Main Explanation Canvas");
    }

    public void ChangeStage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ClickBookButton()
    {
        UIManager.Instance.HideUI("Main Canvas");
        UIManager.Instance.ShowUI("Book Canvas");
    }

    public void BookChangeButton()
    {
        UIManager.Instance.HideUI("Book Canvas");
        UIManager.Instance.ShowUI("Book Canvas 2");
    }

    public void BookExitButton()
    {
        UIManager.Instance.HideUI("Book Canvas");
        UIManager.Instance.HideUI("Book Canvas 2");
        UIManager.Instance.ShowUI("Main Canvas");
    }

    public void BookPageReturnButton()
    {
        UIManager.Instance.HideUI("Book Canvas 2");
        UIManager.Instance.ShowUI("Book Canvas");
    }

    public void ClickManualButton()
    {
        UIManager.Instance.HideUI("Main Canvas");
        UIManager.Instance.ShowUI("Manual Canvas");
    }

    public void ClickInGameManualButton()
    {
        UIManager.Instance.HideUI("In Game Menu Canvas");
        UIManager.Instance.ShowUI("In Game Manual Canvas");
    }

    public void ClickInGameManualExitButton()
    {
        UIManager.Instance.HideUI("In Game Manual Canvas");
        UIManager.Instance.ShowUI("In Game Menu Canvas");
    }

    public void ManualExitButton()
    {
        UIManager.Instance.HideUI("Manual Canvas");
        UIManager.Instance.ShowUI("Main Canvas");
    }

    public void ClickReturnGameButton()
    {
        UIManager.Instance.HideUI("In Game Menu Canvas");
    }

    public void ClickGiveUpButton()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void ClickLeaveGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ClickRestartButton()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void ClickExitButton()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ClickStatusWindowCloseButton()
    {
        UIManager.Instance.HideUI("Status Window Canvas");
    }
}
