using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ItemPickUp : MonoBehaviour
{
     [SerializeField] private float pickupRange;
     [SerializeField] private LayerMask pickupLayer;
     private Inventory inventory;
     private Camera cam;

     private void Start()
     {
          GetReferences();
     }
     private void Update()
     {
          if (Keyboard.current.eKey.wasPressedThisFrame)
          {
               Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
               RaycastHit hit;
               if(Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
               {
                    Weapon newItem = hit.transform.GetComponent<ItemController>().item as Weapon;
                    inventory.AddItem(newItem);
                    Destroy(hit.transform.gameObject);
                    Debug.Log(hit);
               }
          }
     }

     private void GetReferences()
     {
          cam = Camera.main;
          inventory = GetComponent<Inventory>();
     }
}
