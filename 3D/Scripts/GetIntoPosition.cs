using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIntoPosition : MonoBehaviour
{
    public float dstFromPlayer = 25;

    public float range = 5;

    void Start()
    {
        StartCoroutine(MoveToPosition());
    }

    IEnumerator MoveToPosition()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3 (Random.Range(-range, range), Random.Range(-range, range), dstFromPlayer);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
    }
}
