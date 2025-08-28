using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMagic : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 5;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterStats stats = collision.GetComponent<CharacterStats>();
            if (stats != null)
            {
                stats.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}

