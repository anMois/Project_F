using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//플레이어와 상호작용 해야 함
//공격 맞으면 HP 감소
//게임매니저로 코드 이동해야 함
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
