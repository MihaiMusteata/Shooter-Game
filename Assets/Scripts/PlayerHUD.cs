using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private WeaponUI weaponUI;

     public void UpdateWeaponUI(Weapon newWeapon)
     {
          weaponUI.UpdateInfo(newWeapon.icon, newWeapon.magazineSize, newWeapon.storedAmmo);
     }

     public void UpdateAmmoUI(int magazineSize, int storedAmmo)
     {
          weaponUI.UpdateAmmoUI(magazineSize, storedAmmo);
     }
}
