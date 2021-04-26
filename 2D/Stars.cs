using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public GameObject star;

    void Start()
    {
        StartCoroutine(SpawnStars());
    }

    void Update()
    {
        transform.eulerAngles = Vector3.forward * Input.GetAxis("Vertical") * 2;
    }

    IEnumerator SpawnStars()
    {
        yield return new WaitForSeconds(Random.Range(.01f, .2f));
        Vector3 spawnPosition = new Vector3(15, Random.Range(-4.5f, 4.5f), 0);
        Instantiate(star, spawnPosition, transform.rotation, transform);
        StartCoroutine(SpawnStars());
    }
}
