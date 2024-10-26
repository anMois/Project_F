using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{
    private GameObject menuCanvas;
    private GameObject manualCanvas;

    private void Start()
    {
        menuCanvas = GameObject.Find("In Game Menu Canvas");
        manualCanvas = GameObject.Find("In Game Manual Canvas");

        if (menuCanvas != null && manualCanvas != null)
        {
            menuCanvas.SetActive(false);
            manualCanvas.SetActive(false);
        }
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
            Time.timeScale = 0; //게임 멈춤
        }
        else
        {
            Time.timeScale = 1; //게임 재개
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
