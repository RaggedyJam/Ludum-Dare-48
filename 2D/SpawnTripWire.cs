using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTripWire : MonoBehaviour
{
    public GameObject tripwire;

    public Transform relativeMotion;

    void Start()
    {
        StartCoroutine(SpawnTripwire());
    }

    IEnumerator SpawnTripwire()
    {
        yield return new WaitForSeconds(Random.Range(2, 6));
        Instantiate(tripwire, Vector3.right * 15, Quaternion.identity, relativeMotion);
        StartCoroutine(SpawnTripwire());
    }
}
