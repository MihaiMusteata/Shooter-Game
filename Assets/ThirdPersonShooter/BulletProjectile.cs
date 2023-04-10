using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
     private Rigidbody bulletRigidbody;
     public int damage;

     private void Awake()
     {
          bulletRigidbody = GetComponent<Rigidbody>();
     }
     private void Start()
     {
          float speed = 15f;
          bulletRigidbody.velocity = transform.forward * speed;
     }
     public void UpdateDamage(int damage)
     {
          this.damage = damage;
     }
     private void OnTriggerEnter(Collider other)
     {
          if(other.GetComponent<EnemyAI>() != null)
          {
               Debug.Log(damage);
               other.GetComponent<EnemyAI>().TakeDamage(damage);
          }
          Destroy(gameObject);
     }
}
