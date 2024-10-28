using System.Collections;
using System.Collections.Generic;
using TMPro;
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
