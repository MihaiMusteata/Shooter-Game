using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
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
          if(other.GetComponent<EnemyAI>() != null)
          {
               other.GetComponent<EnemyAI>().TakeDamage(10);
          }
          Destroy(gameObject);
     }
}
