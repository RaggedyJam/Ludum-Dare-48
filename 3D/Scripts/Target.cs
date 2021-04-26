using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ThreeD.IDamageable
{
    public float health = 30;

    public event System.Action OnDeath;

    Vector3 startPosition;
    Vector3 targetPosition;

    public GameObject deathFX;

    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        startPosition = transform.position;
        targetPosition = new Vector3 (Random.Range(-5, 5), Random.Range(-5, 5), 50);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        yield return new WaitForSeconds(Random.Range(3, 10));
        StartCoroutine(Move());
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
        OnDeath();

        Destroy(Instantiate(deathFX, transform.position, Quaternion.identity), 2);
        Destroy(gameObject);
    }
}
