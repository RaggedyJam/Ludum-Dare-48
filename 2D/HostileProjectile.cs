using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileProjectile : MonoBehaviour
{
    public float speed = 12;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position -= transform.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        SpaceShip player = col.GetComponent<SpaceShip>();
        if (player != null)
        {
            player.TakeDamage(3);
            speed = 0;
            Destroy(gameObject);
        }
    }
}
