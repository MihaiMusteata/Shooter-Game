using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
     public static InventoryManager instance;
     public List<Item> items = new List<Item>();

     public Transform ItemContent;
     public GameObject InvetoryItem;
     public GameObject _inventory;

     public GameObject[] weapons = new GameObject[3];
     private void Start()
     {
          for (int i = 0; i < weapons.Length; i++)
          {
               weapons[i].SetActive(false);
          }
     }
     public void Inventory()
     {
          if (_inventory.activeSelf)
          {
               _inventory.SetActive(false);
               Cursor.lockState = CursorLockMode.Locked;
               Cursor.visible = false;
          }
          else
          {
               _inventory.SetActive(true);
          }
     }

     private void Awake()
     {
          instance = this;


     }

     public void AddItem(Item item)
     {
          items.Add(item);
     }

     public void ListItems()
     {
          foreach (Transform item in ItemContent)
          {
               if (item == ItemContent.GetChild(0))
               {
                    continue;
               }
               Destroy(item.gameObject);
          }

          foreach (var item in items)
          {
               if (item == ItemContent.GetChild(0))
               {
                    continue;
               }
               GameObject itemObject = Instantiate(InvetoryItem, ItemContent);
               var itemName = itemObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
               var itemIcon = itemObject.transform.Find("ItemIcon").GetComponent<Image>();
               Debug.Log(itemName);

               itemName.text = item.itemName;
               itemIcon.sprite = item.icon;
          }
     }

     private void SwitchWeapon(int weaponIndex)
     {
          for (int i = 0; i < weapons.Length; i++)
          {
               if (i == weaponIndex)
               {
                    weapons[i].SetActive(true); 
               }
               else
               {
                    weapons[i].SetActive(false); 
               }
          }
     }

     public void Update()
     {
          if (Keyboard.current.digit1Key.wasPressedThisFrame)
          {
               SwitchWeapon(0);
          }

          if(Keyboard.current.digit2Key.wasPressedThisFrame)
          {
               SwitchWeapon(1);
          }

          if(Keyboard.current.digit3Key.wasPressedThisFrame)
          {
               SwitchWeapon(2);
          }
     }


}
