using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeD
{
    public class Stars : MonoBehaviour
    {
        public GameObject star;

        void Start()
        {
            StartCoroutine(DrawStars());
        }

        IEnumerator DrawStars()
        {
            Instantiate(star, new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 100), Quaternion.identity, transform);
            yield return new WaitForSeconds(Random.Range(.1f, .2f));
            StartCoroutine(DrawStars());
        }
    }
}
