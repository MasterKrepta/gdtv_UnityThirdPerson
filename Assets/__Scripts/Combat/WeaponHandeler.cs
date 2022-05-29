using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandeler : MonoBehaviour
{
    [SerializeField] GameObject weapon;

    public void EnableWeapon()
    {
        weapon.SetActive(true);
    }

    public void DisableWeapon()
    {
        weapon.SetActive(false);
    }
}
