using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponShooting : MonoBehaviour
{
     private float lastShootTime = 0;

     [SerializeField] private int currentAmmo;
     [SerializeField] private int currentAmmoStorage;
     [SerializeField] private bool magazineIsEmpty = false;
     [SerializeField] private bool canShoot;
     [SerializeField] private bool canReload;

     private ThirdPersonShooterController controller;
     private Camera cam;
     private Inventory inventory;
     private EquipmentManager equipmentManager;

     private void Start()
     {
          GetReferences();
          canShoot = true;
          canReload = true;
     }

     private void GetReferences()
     {
          cam = Camera.main;
          inventory = GetComponent<Inventory>();
          equipmentManager = GetComponent<EquipmentManager>();
          controller = GetComponent<ThirdPersonShooterController>();
     }

     private void Update()
     {
          if (Mouse.current.leftButton.isPressed)
          {
               Shoot();
          }
          else if(Keyboard.current.rKey.wasPressedThisFrame)
          {
               Reload(equipmentManager.currentlyEquippedWeapon);
          }
          else
          {
               equipmentManager._animator.SetBool(equipmentManager._animIDShoot, false);
          }
     }

     private void RaycastShoot(Weapon currentWeapon)
     {
          Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
          RaycastHit hit;

          float range = currentWeapon.range;

          Instantiate(currentWeapon.muzzleFlashParticles, equipmentManager.weaponBarrel);
     }

     private void Shoot()
     {
          CheckCanShoot(equipmentManager.currentlyEquippedWeapon);
          
          if(canShoot && canReload)
          {
               Weapon currentWeapon = inventory.GetItem(equipmentManager.currentlyEquippedWeapon);
               controller.SetProjectileDamage(currentWeapon.damage);
               if (Time.time > lastShootTime + currentWeapon.fireRate)
               {
                    lastShootTime = Time.time;
                    RaycastShoot(currentWeapon);
                    UseAmmo((int)currentWeapon.weaponSlot, 1, 0);
                    equipmentManager._animator.SetBool(equipmentManager._animIDShoot, true);
                    controller.ProjectileLaunch(equipmentManager.weaponBarrel);
               }
          }
          else
          {
               
          }
     }

     private void UseAmmo(int slot, int currentAmmoUsed, int currentAmmoStoredUsed)
     {
          if(currentAmmo <= 0)
          {
               magazineIsEmpty = true;
               CheckCanShoot(equipmentManager.currentlyEquippedWeapon);
          }
          else
          {
               currentAmmo -= currentAmmoUsed;
               currentAmmoStorage -= currentAmmoStoredUsed;
               equipmentManager.hud.UpdateAmmoUI(currentAmmo, currentAmmoStorage);
          }
     }

     private void Reload(int slot)
     {
          if (canReload)
          {
               int ammoToReload = inventory.GetItem(slot).magazineSize - currentAmmo;
               if (currentAmmoStorage >= ammoToReload)
               {
                    currentAmmo += ammoToReload;
                    UseAmmo(slot, 0, ammoToReload);
                    magazineIsEmpty = false;
                    CheckCanShoot(slot);
                    equipmentManager._animator.SetTrigger("Reload");
               }
          } 
     }
     private void CheckCanShoot(int slot)
     {
          if(magazineIsEmpty)
          {
               canShoot = false;
          }
          else
          {
               canShoot = true;
          }
     }


     public void InitAmmo(int slot, Weapon currentWeapon)
     {
          currentAmmo = currentWeapon.magazineSize;
          currentAmmoStorage = currentWeapon.storedAmmo;
     }

}
