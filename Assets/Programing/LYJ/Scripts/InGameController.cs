using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    [SerializeField] Button returnGameButton;
    [SerializeField] Button manualButton;
    [SerializeField] Button giveUpButton;
    [SerializeField] Button leaveGameButton;
    [SerializeField] Button manualExitButton;

    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;

    private void Start()
    {
        InitializeButtons();

        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            UIManager.Instance.ShowUI("Player HP Canvas");
            UIManager.Instance.ShowUI("Player Item Canvas");
        }
    }

    private void InitializeButtons()
    {
        returnGameButton.onClick.AddListener(ClickReturnGameButton);
        manualButton.onClick.AddListener(ClickManualButton);
        giveUpButton.onClick.AddListener(ClickGiveUpButton);
        leaveGameButton.onClick.AddListener(ClickLeaveGame);
        manualExitButton.onClick.AddListener(ManualExitButton);

        restartButton.onClick.AddListener(ClickRestartButton);
        exitButton.onClick.AddListener(ClickExitButton);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UIManager.Instance.IsUIActive("In Game Manual Canvas"))
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
        }

        if (UIManager.Instance.IsUIActive("In Game Menu Canvas") || UIManager.Instance.IsUIActive("In Game Manual Canvas"))
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Return Game 버튼 클릭시
    /// </summary>
    public void ClickReturnGameButton()
    {
        UIManager.Instance.HideUI("In Game Menu Canvas");
    }

    /// <summary>
    /// Manual 버튼 클릭시
    /// </summary>
    public void ClickManualButton()
    {
        UIManager.Instance.HideUI("In Game Menu Canvas");
        UIManager.Instance.ShowUI("In Game Manual Canvas");
    }

    /// <summary>
    /// Manual Exit 버튼 클릭시
    /// </summary>
    public void ManualExitButton()
    {
        UIManager.Instance.HideUI("In Game Manual Canvas");
        UIManager.Instance.ShowUI("In Game Menu Canvas");
    }

    /// <summary>
    /// Give up 버튼 클릭시
    /// </summary>
    public void ClickGiveUpButton()
    {
        //메인 로비 이동
        SceneManager.LoadScene("GameStart");
    }

    /// <summary>
    /// Leave Game 버튼 클릭시
    /// </summary>
    public void ClickLeaveGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Restart 버튼 클릭시
    /// </summary>
    public void ClickRestartButton()
    {
        SceneManager.LoadScene("Stage1");
    }

    /// <summary>
    /// Exit 버튼 클릭시
    /// </summary>
    public void ClickExitButton()
    {
        SceneManager.LoadScene("GameStart");
    }
}
