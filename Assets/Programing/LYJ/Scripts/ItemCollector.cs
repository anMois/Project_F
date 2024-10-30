using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI potionCountText;
    [SerializeField] TextMeshProUGUI grenadeCountText;

    [SerializeField] int potionCount = 0;
    [SerializeField] int grenadeCount = 0;

    private void Start()
    {
        if (potionCountText == null)
        {
            potionCountText = GameObject.Find("Potion Count").GetComponent<TextMeshProUGUI>();
        }

        if (grenadeCountText == null)
        {
            grenadeCountText = GameObject.Find("Grenade Count").GetComponent<TextMeshProUGUI>();
        }

        UpdateUI();
    }

    private void Update()
    {
        if (UIManager.Instance.IsUIActive("In Game Menu Canvas") ||
        UIManager.Instance.IsUIActive("In Game Manual Canvas"))
        {
            return; //다른 UI가 열려 있으면 인벤토리를 열지 않음
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (UIManager.Instance.IsUIActive("Status Window Canvas"))
            {
                UIManager.Instance.HideUI("Status Window Canvas");
            }
            else
            {
                UIManager.Instance.ShowUI("Status Window Canvas");
            }
        }

        //if (UIManager.Instance.IsUIActive("Status Window Canvas"))
        //{
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //}
    }

    //아이템 Tag를 찾아 먹으면 아이템은 사라지고 개수는 증가함
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Potion"))
        {
            potionCount++;
            UpdateUI();
            Destroy(collider.gameObject);
        }

        else if (collider.CompareTag("Grenade"))
        {
            grenadeCount++;
            UpdateUI();
            Destroy(collider.gameObject);
        }
    }

    private void UpdateUI()
    {
        potionCountText.text = $"{potionCount}";
        grenadeCountText.text = $"{grenadeCount}";
    }
}
