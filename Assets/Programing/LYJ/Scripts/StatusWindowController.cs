using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusWindowController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flameCountText;
    [SerializeField] private TextMeshProUGUI iceCountText;
    [SerializeField] private TextMeshProUGUI electricityCountText;
    [SerializeField] private TextMeshProUGUI earthCountText;

    [SerializeField] int flameCount = 0;
    [SerializeField] int iceCount = 0;
    [SerializeField] int electricityCount = 0;
    [SerializeField] int earthCount = 0;

    private void Start()
    {
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flame"))
        {
            flameCount++;
            UpdateUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Ice"))
        {
            iceCount++;
            UpdateUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Electricity"))
        {
            electricityCount++;
            UpdateUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Earth"))
        {
            earthCount++;
            UpdateUI();
            Destroy(other.gameObject);
        }
    }

    private void UpdateUI()
    {
        flameCountText.text = $"{flameCount}";
        iceCountText.text = $"{iceCount}";
        electricityCountText.text = $"{electricityCount}";
        earthCountText.text = $"{earthCount}";
    }
}
