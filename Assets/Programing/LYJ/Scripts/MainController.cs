using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainController : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button itemBookButton;
    [SerializeField] Button manualButton;
    [SerializeField] Button bookExitButton;
    [SerializeField] Button manualExitButton;
    [SerializeField] Button exitButton;

    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image itemImageComponent;

    private void Start()
    {
        InitializeButtons();

        UIManager.Instance.ShowUI("Main Canvas");
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
        if (UIManager.Instance.IsUIActive("Main Explanation Canvas") && Input.GetMouseButtonDown(0))
        {
            Explanation();
        }
    }

    /// <summary>
    /// 설명창에서 아이템 정보를 출력함
    /// </summary>
    public void ShowExplanation(string itemName, string description, Sprite itemImage)
    {
        UIManager.Instance.ShowUI("Main Explanation Canvas");

        // 아이템 정보 업데이트
        itemNameText.text = itemName;
        descriptionText.text = description;
        itemImageComponent.sprite = itemImage;
    }

    /// <summary>
    /// 설명창 닫기
    /// </summary>
    public void Explanation()
    {
        UIManager.Instance.HideUI("Main Explanation Canvas");
    }

    /// <summary>
    /// 씬 전환 - 인게임
    /// </summary>
    public void ChangeStage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    /// <summary>
    /// 도감 버튼 클릭시
    /// </summary>
    public void ClickBookButton()
    {
        UIManager.Instance.HideUI("Main Canvas");
        UIManager.Instance.ShowUI("Book Canvas");
    }

    /// <summary>
    /// 도감 Exit 버튼 클릭시
    /// </summary>
    public void BookExitButton()
    {
        UIManager.Instance.HideUI("Book Canvas");
        UIManager.Instance.ShowUI("Main Canvas");
    }

    /// <summary>
    /// Manual 버튼 클릭시
    /// </summary>
    public void ClickManualButton()
    {
        UIManager.Instance.HideUI("Main Canvas");
        UIManager.Instance.ShowUI("Manual Canvas");
    }

    /// <summary>
    /// Manual Exit 버튼 클릭시
    /// </summary>
    public void ManualExitButton()
    {
        UIManager.Instance.HideUI("Manual Canvas");
        UIManager.Instance.ShowUI("Main Canvas");
    }

    /// <summary>
    /// Exit 버튼 클릭시
    /// </summary>
    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
