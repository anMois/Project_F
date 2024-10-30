using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "InGameItem")]
public class InGameItem : ScriptableObject
{
    public Sprite itemImage;
    public int ATK;
    public int ATS;
    public int DEF;
    public int HP;
    public int RAN;
    public int SPD;
    public string elemental;

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
