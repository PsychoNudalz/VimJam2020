using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitStatesDisplayScript : MonoBehaviour
{
    [Header("Component")]
    public UnitScript currentUnit;

    [Header("Text Display States")]
    public Image CharacterImage;
    public Image WeaponImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI HP;
    public TextMeshProUGUI AC;
    public TextMeshProUGUI Speed;
    public TextMeshProUGUI Ability;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI ToHit;

    [Header("Upgrade")]
    public bool isUpgradeMode;
    public GameObject upgradMenu;

    public void updateStates(UnitScript c)
    {
        currentUnit = c;
        NameText.text = currentUnit.unitName;
        HP.text = currentUnit.ToString_Health();
        AC.text = currentUnit.ToString_AC();
        Speed.text = currentUnit.ToString_Moevement();
        Ability.text = currentUnit.ToString_Ability();
        Damage.text = currentUnit.ToString_Damage();
        ToHit.text = currentUnit.ToString_ToHit();
        CharacterImage.sprite = currentUnit.ToDisplay_Character();
        WeaponImage.sprite = currentUnit.ToDisplay_Weapon();

    }


}
