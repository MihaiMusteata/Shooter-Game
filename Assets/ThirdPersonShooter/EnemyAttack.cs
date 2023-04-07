using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
     private Rigidbody bulletRigidbody;

     private void Awake()
     {
          bulletRigidbody = GetComponent<Rigidbody>();
     }
     private void Start()
     {
          float speed = 15f;
          bulletRigidbody.velocity = transform.forward * speed;
     }
     private void OnTriggerEnter(Collider other)
     {
          if(other.GetComponent<EnemyAI>())
          {
               return;
          }
          if (other.GetComponent<ThirdPersonController>())
          {
               other.GetComponent<ThirdPersonController>().TakeDamage(10);
          }
          


          Destroy(gameObject);
     }
}
