using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
     public Item item;

     void Pickup()
     {
          InventoryManager.instance.AddItem(item);
          Destroy(gameObject);
     }

     private void OnTriggerEnter(Collider other)
     {
          if(other.CompareTag("Player"))
          {
               Pickup();
          }
     }
}
