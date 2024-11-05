using UnityEngine;
using UnityEngine.UI;

public class BossView : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        healthSlider.value = healthSlider.maxValue;
    }

    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        healthSlider.value = (float)currentHealth / maxHealth;
    }
}
