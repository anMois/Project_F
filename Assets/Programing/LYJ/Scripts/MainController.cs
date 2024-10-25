using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject bookCanvas;

    private void Start()
    {
        mainCanvas = GameObject.Find("Main Canvas");
        bookCanvas = GameObject.Find("Book Canvas");

        if (mainCanvas != null && bookCanvas != null)
        {
            mainCanvas.SetActive(true);
            bookCanvas.SetActive(false);
        }
    }

    public void Change_Stage1_Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Click_Book_Button()
    {
        if (mainCanvas != null && bookCanvas != null)
        {
            mainCanvas.SetActive(false);
            bookCanvas.SetActive(true);
        }
    }

    public void Book_Exit_Button()
    {
        if (mainCanvas != null && bookCanvas != null)
        {
            mainCanvas.SetActive(true);
            bookCanvas.SetActive(false);
        }
    }

    public void Exit_Button()
    {
        //유니티 에디터에서는 Application.Quit();를 사용할 수 없음 테스트 용도로 아래 코드 사용
        UnityEditor.EditorApplication.isPlaying = false;
        
        //빌드해서 사용할 때는 아래 코드를 사용
        //Application.Quit();    
    }
}
