using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("One Handed Attac Animations")]
    public string OneHand_LightAttack_1;
    public string OneHand_HeavyAttack_1;
}
