using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class EquipmentManager : MonoBehaviour
{
     public int currentlyEquippedWeapon = -1;// -1 for no weapon
     private GameObject currentWeaponObject = null;
     public Transform weaponBarrel = null;

     [SerializeField] private Transform WeaponHolderR = null;
     [SerializeField] public int damage;

     public Animator _animator;
     private bool _hasAnimator;
     private int _animIDWeapon = Animator.StringToHash("Weapon");
     public int _animIDShoot = Animator.StringToHash("Shoot");
     private Inventory inventory;
     public PlayerHUD hud;

     private void Start()
     {
          GetReferences();
     }
          
     private void GetReferences()
     {
          _hasAnimator = TryGetComponent(out _animator);
          inventory = GetComponent<Inventory>();
          hud = GetComponent<PlayerHUD>();

     }

     private void Update()
     {
          if (Keyboard.current.digit1Key.wasPressedThisFrame && currentlyEquippedWeapon != 0)
          {
               UnequipWeapon();
               EquipWeapon(inventory.GetItem(0));
          }

          if (Keyboard.current.digit2Key.wasPressedThisFrame && currentlyEquippedWeapon != 1)
          {
               UnequipWeapon();
               EquipWeapon(inventory.GetItem(1));
          }

          if (Keyboard.current.digit3Key.wasPressedThisFrame && currentlyEquippedWeapon != 2)
          {
               UnequipWeapon();
               EquipWeapon(inventory.GetItem(2));
          }

          if (Keyboard.current.digit4Key.wasPressedThisFrame && currentlyEquippedWeapon != 3)
          {
               UnequipWeapon();
               EquipWeapon(inventory.GetItem(3));
          }

          if (Keyboard.current.gKey.wasPressedThisFrame && currentlyEquippedWeapon != -1)
          {
               UnequipWeapon();
               currentlyEquippedWeapon = -1;
          }

     }


     private void EquipWeapon(Weapon weapon)
     {
          currentlyEquippedWeapon = (int)weapon.weaponSlot;
          damage = weapon.damage;
          currentWeaponObject = Instantiate(weapon.prefab, WeaponHolderR);
          weaponBarrel = currentWeaponObject.transform.GetChild(0);
          _animator.SetBool(_animIDWeapon, true);
          hud.UpdateWeaponUI(weapon);
     }

     private void UnequipWeapon()
     {
          _animator.SetBool(_animIDWeapon, false);
          Destroy(currentWeaponObject);
     }


}
