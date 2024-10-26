using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    private GameObject menuCanvas;
    private GameObject manualCanvas;

    private Button returnGameButton;
    private Button manualButton;
    private Button giveUpButton;
    private Button leaveGameButton;
    private Button manualExitButton;

    private void Start()
    {
        menuCanvas = GameObject.Find("In Game Menu Canvas");
        manualCanvas = GameObject.Find("In Game Manual Canvas");

        returnGameButton = GameObject.Find("Return Game Button").GetComponent<Button>();
        manualButton = GameObject.Find("Manual Button").GetComponent<Button>();
        giveUpButton = GameObject.Find("Give Up Button").GetComponent<Button>();
        leaveGameButton = GameObject.Find("Leave Game Button").GetComponent<Button>();
        manualExitButton = GameObject.Find("Manual Exit Button").GetComponent<Button>();

        menuCanvas.SetActive(false);
        manualCanvas.SetActive(false);

        InitializeButtons();
    }

    private void InitializeButtons()
    {
        if (returnGameButton) returnGameButton.onClick.AddListener(ClickReturnGameButton);
        if (manualButton) manualButton.onClick.AddListener(ClickManualButton);
        if (giveUpButton) giveUpButton.onClick.AddListener(ClickGiveUpButton);
        if (leaveGameButton) leaveGameButton.onClick.AddListener(ClickLeaveGame);
        if (manualExitButton) manualExitButton.onClick.AddListener(ManualExitButton);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!manualCanvas.activeSelf)
            {
                bool isActive = menuCanvas.activeSelf;
                menuCanvas.SetActive(!isActive);
            }
        }

        if (menuCanvas.activeSelf || manualCanvas.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ClickReturnGameButton()
    {
        menuCanvas.SetActive(false);
    }

    public void ClickManualButton()
    {
        menuCanvas.SetActive(false);
        manualCanvas.SetActive(true);
    }

    public void ManualExitButton()
    {
        menuCanvas.SetActive(true);
        manualCanvas.SetActive(false);
    }

    public void ClickGiveUpButton()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void ClickLeaveGame()
    {
        //유니티 에디터에서는 Application.Quit();를 사용할 수 없음 테스트 용도로 아래 코드 사용
        UnityEditor.EditorApplication.isPlaying = false;

        //빌드해서 사용할 때는 아래 코드를 사용
        //Application.Quit();  
    }
}
