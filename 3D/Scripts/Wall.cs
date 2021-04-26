using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void Start()
    {
        float wallGap = Random.Range(3, 20);

        StartCoroutine(AnimateWall(wallGap));

        transform.position += transform.up * Random.Range(-30, 30);

        Destroy(gameObject, 20);
    }

    void Update()
    {
        if (transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator AnimateWall(float wallGap)
    {
        Transform wall1 = transform.GetChild(0);
        Transform wall2 = transform.GetChild(1);

        wall1.localPosition = Vector3.up * 100;
        wall2.localPosition = -Vector3.up * 100;

        Vector3 startPosition1 = wall1.localPosition;
        Vector3 startPosition2 = wall2.localPosition;

        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime;
            wall1.localPosition = Vector3.Lerp(startPosition1, Vector3.up * (wallGap / 2), t);
            wall2.localPosition = Vector3.Lerp(startPosition2, -Vector3.up * (wallGap / 2), t);
            yield return null;
        }
    }
}
