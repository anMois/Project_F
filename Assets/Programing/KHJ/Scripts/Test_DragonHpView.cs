using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_DragonHpView : MonoBehaviour
{
    [SerializeField] BossDragon hpModel;
    [SerializeField] Text hpText;
    [SerializeField] int maxHp;

    private void Start()
    {
        maxHp = hpModel.CurHp;
        UpdateHp(hpModel.CurHp);
    }

    public void UpdateHp(int hp)
    {
        hpText.text = $"{hpModel.CurHp} / {maxHp}";
    }
}
