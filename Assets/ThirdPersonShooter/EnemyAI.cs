using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
     public NavMeshAgent agent;
     public Transform player;
     public LayerMask whatIsGround, whatIsPlayer;
     public float maxHealth = 100;
     public float health;
     public Image healthBar;
     Animator animator;


     //Patroling
     public Vector3 walkPoint;
     bool walkPointSet;
     public float walkPointRange;

     //Attacking
     public float timeBetweenAttacks;
     bool alreadyAttacked;
     public GameObject projectile;
     public Transform spawnProjectilePosition;

     //States
     public float sightRange, attackRange;
     public bool playerInSightRange, playerInAttackRange;

     private void Start()
     {
          health = maxHealth;
          healthBar.fillAmount = health / maxHealth;
          animator = GetComponent<Animator>();
     }
     private void Awake()
     {
          player = GameObject.Find("PlayerArmature").transform;
          agent = GetComponent<NavMeshAgent>();
     }

     private void Update()
     {
          healthBar.fillAmount = health / maxHealth;
          if (health > 0)
          {
               //Check for sight and attack range
               playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
               playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

               if (!playerInSightRange && !playerInAttackRange) Patroling();
               if (playerInSightRange && !playerInAttackRange) ChasePlayer();
               if (playerInSightRange && playerInAttackRange) AttackPlayer();
          }
     }

     private void Patroling()
     {

          if (!walkPointSet) SearchWalkPoint();

          if (walkPointSet)
               agent.SetDestination(walkPoint);

          Vector3 distanceToWalkPoint = transform.position - walkPoint;

          //Walkpoint reached
          if (distanceToWalkPoint.magnitude < 1f)
               walkPointSet = false;
     }

     private void SearchWalkPoint()
     {
          //Calculate random point in range
          float randomZ = Random.Range(-walkPointRange, walkPointRange);
          float randomX = Random.Range(-walkPointRange, walkPointRange);

          walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

          //Check if walkpoint is on ground
          if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
               walkPointSet = true;
     }

     private void ChasePlayer()
     {
          animator.SetTrigger("Walk Forward");
          agent.SetDestination(player.position);
     }

     private void AttackPlayer()
     {
          //Make sure enemy doesn't move
          agent.SetDestination(transform.position);

          transform.LookAt(player);

          if (!alreadyAttacked)
          {
               animator.SetTrigger("Attack 02");
               //Attack code here
               Rigidbody rb = Instantiate(projectile, spawnProjectilePosition.transform.position, spawnProjectilePosition.transform.rotation).GetComponent<Rigidbody>();
               rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
               rb.AddForce(transform.up * 8f, ForceMode.Impulse);
               alreadyAttacked = true;
               Invoke(nameof(ResetAttack), timeBetweenAttacks);
          }
     }

     private void ResetAttack()
     {
          alreadyAttacked = false;
     }

     public void TakeDamage(int damage)
     {
          health -= damage;
          animator.SetTrigger("Take Damage");
          if (health <= 0) Invoke(nameof(Die), 0.5f);
     }

     private void Die()
     {
          animator.SetTrigger("Die");
          alreadyAttacked = true;
          Destroy(gameObject,1.5f);
     }

     private void OnDrawGizmosSelected()
     {
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(transform.position, sightRange);

          Gizmos.color = Color.blue;
          Gizmos.DrawWireSphere(transform.position, attackRange);
     }

    
}
