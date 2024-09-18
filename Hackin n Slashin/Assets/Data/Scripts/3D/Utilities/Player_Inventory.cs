using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : GenericBehaviour
{
    WeaponSlot_Manager WeaponSlot_Manager;

    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    private void Awake()
    {
        WeaponSlot_Manager = GetComponentInChildren<WeaponSlot_Manager>();
    }
    private void Start()
    {
        WeaponSlot_Manager.LoadWeapon_To_Slot(rightWeapon, false);
        WeaponSlot_Manager.LoadWeapon_To_Slot(leftWeapon, true);
    }
}
