using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WeaponUI : MonoBehaviour
{
     [SerializeField] private Image icon;
     [SerializeField] private TextMeshProUGUI magazineSizeText;
     [SerializeField] private TextMeshProUGUI storedAmmoText;

     public void UpdateInfo(Sprite weaponIcon, int magazineSize, int storedAmmo)
     {
          icon.sprite = weaponIcon;
          magazineSizeText.text = magazineSize.ToString();
          storedAmmoText.text = storedAmmo.ToString();
     }
     public void UpdateAmmoUI(int magazineSize, int storedAmmo)
     {
          magazineSizeText.text = magazineSize.ToString();
          storedAmmoText.text = storedAmmo.ToString();
     }
}
