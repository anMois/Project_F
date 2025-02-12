using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Game Clear Canvas")]
    [SerializeField] Button gameClearExitButton;
    [SerializeField] Image[] gameClearItemImage;

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
        InitializeButtons(); //버튼 초기화
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

        // Game Clear Button
        if (gameClearExitButton) gameClearExitButton.onClick.AddListener(ClickGameClearExitButton);
    }

    private void Update()
    {
        if (UIManager.Instance.IsUIActive("Main Explanation Canvas") && Input.GetMouseButtonDown(0))
        {
            Explanation();

            if (UIManager.Instance.IsUIActive("Book Canvas"))
            {
                UIManager.Instance.ShowUI("Book Canvas");
            }
            else if (UIManager.Instance.IsUIActive("Book Canvas 2"))
            {
                UIManager.Instance.ShowUI("Book Canvas 2");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UIManager.Instance.IsUIActive("Main Canvas") &&
                !UIManager.Instance.IsUIActive("In Game Manual Canvas") &&
                !UIManager.Instance.IsUIActive("Start Item Canvas") &&
                !UIManager.Instance.IsUIActive("Manual Canvas") &&
                !UIManager.Instance.IsUIActive("Book Canvas") &&
                !UIManager.Instance.IsUIActive("Book Canvas 2") &&
                !UIManager.Instance.IsUIActive("Status Window Canvas") &&
                !UIManager.Instance.IsUIActive("Stage Clear Canvas") &&
                !UIManager.Instance.IsUIActive("Boss Stage Canvas") &&
                !UIManager.Instance.IsUIActive("Game Clear Canvas") &&
                !UIManager.Instance.IsUIActive("Game Over Canvas"))
            {
                ToggleInGameMenu();
            }
        }

        if (UIManager.Instance.IsUIActive("In Game Menu Canvas") || UIManager.Instance.IsUIActive("In Game Manual Canvas") || UIManager.Instance.IsUIActive("Game Over Canvas"))
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (SceneManager.GetActiveScene().name == "StageWord")
        {
            ShowInGameUI();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (UIManager.Instance.IsUIActive("Main Canvas") ||
                UIManager.Instance.IsUIActive("Game Over Canvas") ||
                UIManager.Instance.IsUIActive("Game Clear Canvas") ||
                UIManager.Instance.IsUIActive("Stage Clear Canvas") ||
                UIManager.Instance.IsUIActive("In Game Menu Canvas"))
            {
                return;
            }

            if (UIManager.Instance.IsUIActive("Status Window Canvas"))
            {
                SoundManager.Instance.InventorySound();
                UIManager.Instance.HideUI("Status Window Canvas");
                UIManager.Instance.HideUI("Status Window Explanation Canvas");
            }
            else
            {
                SoundManager.Instance.InventorySound();
                UIManager.Instance.ShowUI("Status Window Canvas");
            }
            Time.timeScale = UIManager.Instance.IsUIActive("Status Window Canvas") ? 0 : 1;
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

    private void Explanation()
    {
        UIManager.Instance.HideUI("Main Explanation Canvas");
    }

    private void ChangeStage1Scene()
    {
        SoundManager.Instance.ButtonClickSound();
        SceneManager.LoadScene("StageWord");
    }

    private void ClickBookButton()
    {
        SoundManager.Instance.BookOpenSound();
        UIManager.Instance.ShowUI("Book Canvas");
    }

    private void BookChangeButton()
    {
        SoundManager.Instance.BookOpenSound();
        UIManager.Instance.HideUI("Book Canvas");
        UIManager.Instance.ShowUI("Book Canvas 2");
    }

    private void BookExitButton()
    {
        SoundManager.Instance.ButtonClickSound();
        UIManager.Instance.HideUI("Book Canvas");
        UIManager.Instance.HideUI("Book Canvas 2");
        UIManager.Instance.ShowUI("Main Canvas");
    }

    private void BookPageReturnButton()
    {
        SoundManager.Instance.BookOpenSound();
        UIManager.Instance.HideUI("Book Canvas 2");
        UIManager.Instance.ShowUI("Book Canvas");
    }

    private void ClickManualButton()
    {
        SoundManager.Instance.ButtonClickSound();
        UIManager.Instance.ShowUI("Manual Canvas");
    }

    private void ClickInGameManualButton()
    {
        SoundManager.Instance.ButtonClickSound();
        UIManager.Instance.ShowUI("In Game Manual Canvas");
    }

    private void ClickInGameManualExitButton()
    {
        SoundManager.Instance.ButtonClickSound();
        UIManager.Instance.HideUI("In Game Manual Canvas");
        UIManager.Instance.ShowUI("In Game Menu Canvas");
    }

    private void ManualExitButton()
    {
        SoundManager.Instance.ButtonClickSound();
        UIManager.Instance.HideUI("Manual Canvas");
        UIManager.Instance.ShowUI("Main Canvas");
    }

    private void ClickReturnGameButton()
    {
        SoundManager.Instance.ButtonClickSound();
        UIManager.Instance.HideUI("In Game Menu Canvas");
    }

    private void ClickGiveUpButton()
    {
        SoundManager.Instance.ButtonClickSound();
        SceneManager.LoadScene("GameStart");
    }

    private void ClickLeaveGame()
    {
        SoundManager.Instance.ButtonClickSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ClickRestartButton()
    {
        SoundManager.Instance.ButtonClickSound();
        //SceneManager.LoadScene("StageNext");
        SceneManager.LoadScene("GameStart");
    }

    private void ClickExitButton()
    {
        SoundManager.Instance.ButtonClickSound();
        SceneManager.LoadScene("GameStart");
    }

    private void ExitButton()
    {
        SoundManager.Instance.ButtonClickSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ClickStatusWindowCloseButton()
    {
        SoundManager.Instance.ButtonClickSound();
        UIManager.Instance.HideUI("Status Window Canvas");
    }

    public void ClickGameClearExitButton()
    {
        SoundManager.Instance.ButtonClickSound();
        SceneManager.LoadScene("GameStart");
    }

    public void FillGameClearImages(List<Sprite> relicSprites)
    {
        for (int i = 0; i < gameClearItemImage.Length; i++)
        {
            if (i < relicSprites.Count)
            {
                gameClearItemImage[i].sprite = relicSprites[i];
                gameClearItemImage[i].gameObject.SetActive(true);
            }
            else
            {
                gameClearItemImage[i].gameObject.SetActive(false); // 이미지를 비활성화 해둠
            }
        }
    }

    //게임 클리어 부분에 사용
    //GameController.Instance.ShowGameClearCanvas(); <- 이 코드로 사용하면 됨
    public void ShowGameClearCanvas()
    {
        //StatusWindowController에서 활성화된 유물 이미지를 가져옴
        List<Sprite> relicImages = StatusWindowController.Instance.GetActiveRelicImages();

        FillGameClearImages(relicImages);

        UIManager.Instance.ShowUI("Game Clear Canvas");
    }
}
