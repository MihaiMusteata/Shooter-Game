using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : Item
{
     public GameObject prefab;
     public GameObject muzzleFlashParticles;
     public int damage;
     public float fireRate;
     public float range;
     public int magazineSize;
     public int storedAmmo;
     public WeaponType weaponType;
     public WeaponSlot weaponSlot;
}
public enum WeaponType { Melee, AR, SMG, Shotgun, Sniper, Pistol }
public enum WeaponSlot { Slot1, Slot2, Slot3, Slot4, Slot5 }
