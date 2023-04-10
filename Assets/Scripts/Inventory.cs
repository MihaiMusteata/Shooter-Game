using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
     [SerializeField] private Weapon[] weapons;

     private WeaponShooting shooting;
     void Start()
     {
          GetReferences();
          InitializeVaiable();
     }

     private void InitializeVaiable()
     {
          weapons = new Weapon[5];
     }

     private void GetReferences()
     {
          shooting = GetComponent<WeaponShooting>();
     }
   
     public void AddItem(Weapon newItem)
     {
          int index = (int)newItem.weaponSlot;
          if (weapons[index] != null)
          {
               RemoveItem(index);
          }
          weapons[index] = newItem;

          shooting.InitAmmo((int)newItem.weaponSlot, newItem);

          
     }

     public void RemoveItem(int index)
     {
          weapons[index] = null;
     }

     public Weapon GetItem(int index)
     {
          return weapons[index];
     }



}
