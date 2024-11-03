using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Dictionary<string, GameObject> uiCanvases = new Dictionary<string, GameObject>();

    public bool IsAnimationCompleted { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("UICanvas"))
            {
                uiCanvases[child.name] = child.gameObject;
                child.gameObject.SetActive(false); // 모든 UI 비활성화
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HideUI("Main Canvas");
        HideUI("In Game Menu Canvas");
        HideUI("Player HP Canvas");
        HideUI("Player Item Canvas");
        HideUI("Next Stage Canvas");
        HideUI("Stage Clear Canvas");
        HideUI("Boss Stage Canvas");
        HideUI("Boss Stage HP Canvas");
        HideUI("Game Clear Canvas");
        HideUI("Game Over Canvas");

        if (SceneManager.GetActiveScene().name == "StageWord")
        {
            ShowUI("Start Item Canvas");
        } 

        //if (SceneManager.GetActiveScene().name == "Stage1")
        //{
        //    ShowUI("Start Item Canvas");
        //}
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// 특정 UI를 활성화시킬 때 사용
    /// </summary>
    /// <param name="uiName"></param>
    public void ShowUI(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            GameObject canvas = uiCanvases[uiName];
            canvas.SetActive(true);

            if (IsUIActive("Status Window Canvas") ||
                IsUIActive("In Game Menu Canvas") ||
                IsUIActive("Start Item Canvas") ||
                IsUIActive("Next Stage Canvas") ||
                IsUIActive("Boss Stage Canvas") ||
                IsUIActive("Stage Clear Canvas") ||
                IsUIActive("Store Canvas") ||
                IsUIActive("Main Canvas"))
            {
                //Debug.Log($"{uiName} 보여짐");
                Cursor.lockState = CursorLockMode.None;
            }

            if (uiName == "Game Over Canvas")
            {
                Transform backgroundPanel = canvas.transform.Find("BackgroundPanel");
                if (backgroundPanel != null)
                {
                    backgroundPanel.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log($"{uiName} UI를 찾을 수 없습니다.");
        }
    }


    /// <summary>
    /// 특정 UI를 비활성화 시킬 때 사용
    /// </summary>
    /// <param name="uiName"></param>
    public void HideUI(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            GameObject canvas = uiCanvases[uiName];
            uiCanvases[uiName].SetActive(false);

            Debug.Log($"{uiName} 숨겨짐");
            Cursor.lockState = CursorLockMode.Locked;

            if (uiName == "Game Over Canvas")
            {
                Transform backgroundPanel = canvas.transform.Find("BackgroundPanel");
                if (backgroundPanel != null)
                {
                    backgroundPanel.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log($"{uiName} UI를 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 특정 UI가 활성화되어 있는지 확인
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns>활성화 상태</returns>
    public bool IsUIActive(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            return uiCanvases[uiName].activeSelf;
        }
        else
        {
            Debug.Log($"{uiName} UI를 찾을 수 없습니다.");
            return false;
        }
    }

    /// <summary>
    /// UI 캔버스를 가져오는 메서드
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns>찾은 UI 캔버스</returns>
    public GameObject GetUICanvas(string uiName)
    {
        if (uiCanvases.ContainsKey(uiName))
        {
            return uiCanvases[uiName];
        }
        else
        {
            Debug.Log($"{uiName} UI를 찾을 수 없습니다.");
            return null;
        }
    }

    /// <summary>
    /// 모든 UI 비활성화 시킬 때 사용
    /// </summary>
    public void HideAllUI()
    {
        foreach (var uiCanvas in uiCanvases.Values)
        {
            uiCanvas.SetActive(false);
        }
    }
}
