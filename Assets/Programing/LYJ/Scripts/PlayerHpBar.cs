using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] Image hpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth = 100f;

    private void Start()
    {
        hpBar = GetComponent<Image>();

        if (hpText == null)
        {
            hpText = GetComponentInChildren<TextMeshProUGUI>();
        }

        currentHealth = maxHealth;
    }

    public void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); //최소 0, 최대 100
        float healthRatio = currentHealth / maxHealth; //체력 비율 계산
        hpBar.fillAmount = healthRatio; //Fill Amount 조절

        hpText.text = $"{(int)currentHealth} / {maxHealth}";
    }
}
