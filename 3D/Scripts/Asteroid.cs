using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, ThreeD.IDamageable
{
    public float health = 36;

    public GameObject asteroidDeath;

    void Start()
    {
        transform.localScale = Vector3.one * Random.Range(3, 10);
        Destroy(gameObject, 10);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameObject newFX = Instantiate(asteroidDeath, transform.position, Quaternion.identity);
        Destroy(newFX, 2);
        Destroy(transform.parent.gameObject);
    }
}
