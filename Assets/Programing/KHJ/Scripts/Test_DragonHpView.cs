using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_DragonHpView : MonoBehaviour
{
    [SerializeField] BossDragon hpModel;
    [SerializeField] Slider hpSlider;
    [SerializeField] int maxHp = 100;

    private void Start()
    {
        maxHp = hpModel.CurHp;
        hpSlider.maxValue = maxHp;
        UpdateHp(hpModel.CurHp);
    }

    public void UpdateHp(int hp)
    {
        hpSlider.value = hp;
    }
}
