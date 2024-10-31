using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "StartItem")]
public class StartItemData : ScriptableObject
{
    public string itemName;
    public string specialEffects;
    public string specialEffectsDescription;
    public Sprite itemImage;
    public Sprite itemAttributesImage;

    public Sprite specialEffectsImage1;
    public Sprite specialEffectsImage2;
    public Sprite specialEffectsImage3;

    public string specialEffectsFigure1;
    public string specialEffectsFigure2;
    public string specialEffectsFigure3;

    public ElementalType elemental;

    public int ATK;
    public int ATS;
    public int DEF;
    public int HP;
    public int RAN;
    public int SPD;
}
