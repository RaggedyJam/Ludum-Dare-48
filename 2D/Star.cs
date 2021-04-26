using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    float speed;

    void Start()
    {
        speed = Random.Range(25, 100);
        Destroy(gameObject, 3);
    }

    void Update()
    {
        transform.position -= transform.right * speed * Time.deltaTime;
    }
}
