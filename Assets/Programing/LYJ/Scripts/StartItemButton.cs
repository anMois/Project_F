using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartItemButton : MonoBehaviour
{
    public StartItemData startItemData;

    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemNameTextMain;
    [SerializeField] TextMeshProUGUI specialEffects;
    [SerializeField] TextMeshProUGUI specialEffectsDescription;
    [SerializeField] Image itemImageMain;
    [SerializeField] Image itemImage;
    [SerializeField] Image itemAttributesImage;

    [SerializeField] Image specialEffectsImage1;
    [SerializeField] Image specialEffectsImage2;
    [SerializeField] Image specialEffectsImage3;

    [SerializeField] TextMeshProUGUI specialEffectsFigure1;
    [SerializeField] TextMeshProUGUI specialEffectsFigure2;
    [SerializeField] TextMeshProUGUI specialEffectsFigure3;

    [SerializeField] GameObject startItemCanvas;
    [SerializeField] GameObject startItemExplanationCanvas;

    [SerializeField] GameObject specialEffect1;
    [SerializeField] GameObject specialEffect2;
    [SerializeField] GameObject specialEffect3;

    [SerializeField] Button button;
    [SerializeField] Button itemNameButton;

    private void Start()
    {
        startItemCanvas = GameObject.Find("Start Item Canvas");

        button = gameObject.GetComponent<Button>();

        if (startItemCanvas != null && startItemExplanationCanvas != null)
        {
            startItemCanvas.SetActive(true);
            startItemExplanationCanvas.SetActive(false);
        }

        button.onClick.AddListener(OnClickButton);
        itemNameButton.onClick.AddListener(GameStartButton);
    }

    private void Update()
    {
        if (startItemExplanationCanvas != null && startItemExplanationCanvas.activeSelf && Input.GetMouseButtonDown(0))
        {
            startItemExplanationCanvas.SetActive(false);
        }

        itemNameTextMain.text = startItemData.itemName;
        itemImageMain.sprite = startItemData.itemImage;
    }

    public void OnClickButton()
    {
        if (startItemExplanationCanvas != null)
            startItemExplanationCanvas.SetActive(true);


        itemNameText.text = startItemData.itemName;
        specialEffects.text = startItemData.specialEffects;
        specialEffectsDescription.text = startItemData.specialEffectsDescription;
        itemImage.sprite = startItemData.itemImage;
        itemAttributesImage.sprite = startItemData.itemAttributesImage;
        specialEffectsImage1.sprite = startItemData.specialEffectsImage1;
        specialEffectsFigure1.text = startItemData.specialEffectsFigure1;

        if (startItemData.specialEffectsImage2 != null && startItemData.specialEffectsFigure2 != null)
        {
            specialEffectsImage2.sprite = startItemData.specialEffectsImage2;
            specialEffectsFigure2.text = startItemData.specialEffectsFigure2;
            specialEffect2.gameObject.SetActive(true);
        }
        else
        {
            specialEffect2.gameObject.SetActive(false);
        }

        if (startItemData.specialEffectsImage3 != null && startItemData.specialEffectsFigure3 != null)
        {
            specialEffectsImage3.sprite = startItemData.specialEffectsImage3;
            specialEffectsFigure3.text = startItemData.specialEffectsFigure3;
            specialEffect3.gameObject.SetActive(true);
        }
        else
        {
            specialEffect3.gameObject.SetActive(false);
        }
    }

    public void GameStartButton()
    {
        startItemCanvas.SetActive(false);
        startItemExplanationCanvas.SetActive(false);

        UIManager.Instance.HideUI("Start Item Canvas");

        //선택한 유물이 가지고 있는 스테이터스 수치가 인벤토리에 반영됨
        StatusWindowController.Instance.UpdateStartStatUI(startItemData);

        //처음 선택한 유물에 대한 속성값 전달
        StatusWindowController.Instance.AddElementalCount(startItemData.elemental);

        Debug.Log($"{startItemData.itemName}을 선택하셨습니다.");
    }
}
