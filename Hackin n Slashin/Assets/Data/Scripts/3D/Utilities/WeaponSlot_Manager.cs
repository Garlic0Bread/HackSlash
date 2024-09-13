using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot_Manager : MonoBehaviour
{
    WeaponSlot_Holder lefthandSlot;
    WeaponSlot_Holder righthandSlot;

    private void Awake()
    {
        WeaponSlot_Holder[] weaponSlot_holder = GetComponentsInChildren<WeaponSlot_Holder>();//find all weapon slots
        foreach(var weaponSlot in weaponSlot_holder) //check whether weapon slot is left or right handed
        {
            if (weaponSlot.isLeftHandSlot)
            {
                lefthandSlot = weaponSlot;
            }
            else if(weaponSlot.isRightHandSlot)
            {
                righthandSlot = weaponSlot;
            }
        
        }
    }

    public void LoadWeapon_To_Slot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            lefthandSlot.LoadWeaponModel(weaponItem);
        }
        else
        {
            righthandSlot.LoadWeaponModel(weaponItem);
        }
    }
}
