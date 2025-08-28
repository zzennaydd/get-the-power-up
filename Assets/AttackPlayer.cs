using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public GameObject weaponPrefab;
    public GameObject monsterBullet;
    public Transform player;          
    private float lastAttackTime = 0f;
    public float attackCooldown = 2f;
    public int damage = 5;
    public float offsetDistance = 0.5f; 
    private Rigidbody2D rb;
    public bool isWizard = false;

    public enum EnemyType { Melee, Ranged }
    public EnemyType enemyType;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        FollowPlayer();

        if (Vector2.Distance(transform.position, player.position) < 10f)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                if (enemyType == EnemyType.Melee && Vector2.Distance(transform.position, player.position) < 2f)
                {
                    Attack();
                }
                else if (enemyType == EnemyType.Ranged)
                {
                    Attack(); 
                }

                lastAttackTime = Time.time;
            }
        }
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float speed = isWizard ? 0.5f : 1.5f;
            Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;

            rb.MovePosition(newPosition);
            if (direction.x > 0)
                transform.localScale = new Vector3(1, 1, 1);  
            else if (direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1); 

        }
    }

    void Attack()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 spawnPos = transform.position + direction * offsetDistance;


        if (weaponPrefab != null)
        {
            GameObject wandEffect = Instantiate(weaponPrefab, spawnPos, Quaternion.identity);
            wandEffect.transform.parent = transform;
            Destroy(wandEffect, 0.2f);
        }

        if (isWizard && monsterBullet != null)
        {
            GameObject bullet = Instantiate(monsterBullet, spawnPos, Quaternion.identity);

        }


        if (!isWizard)
        {
            CharacterStats stats = player.GetComponent<CharacterStats>();
            if (stats != null)
            {
                stats.TakeDamage(damage);
            }
        }
    }
}
