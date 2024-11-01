using UnityEngine;

//게임 내에서 스크립터블 오브젝트로 생성한 아이템이
/*
 
 private void OnTriggerEnter(Collider other)
    {
        InGameItemReference itemReference = other.GetComponent<InGameItemReference>();

        if (itemReference != null)
        {
            // 아이템을 수집
            StatusWindowController.Instance.CollectItem(itemReference.item);

            Destroy(other.gameObject);

        }
    }
 
 */
//이 코드를 사용하여 떨어진 유물을 먹었을 때 인벤토리로 들어가게 됨
//문제: 해당 아이템을 클릭하여 설명창을 보면 해당 아이템 설명이 아닌 CSV 순서대로 출려되는 버그 존재
//현재 기획서 수정으로 인해 해당 코드가 필요없어짐

public enum ElementalType
{
    Flame,
    Ice,
    Electricity,
    Earth
}

[CreateAssetMenu(fileName = "Item_", menuName = "InGameItem")]
public class InGameItemData : ScriptableObject
{
    public Sprite itemImage;
    public int ATK;
    public int ATS;
    public int DEF;
    public int HP;
    public int RAN;
    public int SPD;
    public ElementalType elemental;

    public void ApplyEffect(StatusWindowController statusWindow)
    {
        ApplyStatEffect(statusWindow, "ATK", ATK);
        ApplyStatEffect(statusWindow, "ATS", ATS);
        ApplyStatEffect(statusWindow, "DEF", DEF);
        ApplyStatEffect(statusWindow, "HP", HP);
        ApplyStatEffect(statusWindow, "RAN", RAN);
        ApplyStatEffect(statusWindow, "SPD", SPD);
    }

    private void ApplyStatEffect(StatusWindowController statusWindow, string statName, int value)
    {
        statusWindow.ChangeStat(statName, value);
    }
}
