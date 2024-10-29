using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartItemButton : MonoBehaviour
{
    public StartItemData startItemData;

    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI specialEffects;
    [SerializeField] TextMeshProUGUI specialEffectsDescription;
    [SerializeField] Image itemImage;

    [SerializeField] Image specialEffectsImage1;
    [SerializeField] Image specialEffectsImage2;
    [SerializeField] Image specialEffectsImage3;

    [SerializeField] TextMeshProUGUI specialEffectsFigure1;
    [SerializeField] TextMeshProUGUI specialEffectsFigure2;
    [SerializeField] TextMeshProUGUI specialEffectsFigure3;

    [SerializeField] GameObject startItemCanvas;
    [SerializeField] GameObject startItemExplanationCanvas;

    [SerializeField] Button button;

    [SerializeField] GameObject specialEffect1;
    [SerializeField] GameObject specialEffect2;
    [SerializeField] GameObject specialEffect3;

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
    }

    private void Update()
    {
        if (startItemExplanationCanvas != null && startItemExplanationCanvas.activeSelf && Input.GetMouseButtonDown(0))
        {
            startItemExplanationCanvas.SetActive(false);
        }
    }

    public void OnClickButton()
    {
        if (startItemExplanationCanvas != null)
            startItemExplanationCanvas.SetActive(true);


        itemNameText.text = startItemData.itemName;
        specialEffects.text = startItemData.specialEffects;
        specialEffectsDescription.text = startItemData.specialEffectsDescription;
        itemImage.sprite = startItemData.itemImage;
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
}
