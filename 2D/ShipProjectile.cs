using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProjectile : MonoBehaviour
{
    float speed = 12;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable hitDamageable = col.GetComponent<IDamageable>();
        if (hitDamageable != null)
        {
            hitDamageable.TakeDamage(4);
            Destroy(gameObject);
        }
    }
}
