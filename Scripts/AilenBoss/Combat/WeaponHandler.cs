using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weapon1;
    [SerializeField] private GameObject weapon2;

    public void EnableWeapon(string weaponName)
    {
        if (weaponName == "Weapon1")
        {
            weapon1.SetActive(true);
        }
        else if (weaponName == "Weapon2")
        {
            weapon2.SetActive(true);
        }
    }

    public void DisableWeapon()
    {
        weapon1.SetActive(false);
        weapon2.SetActive(false);
    }
}
