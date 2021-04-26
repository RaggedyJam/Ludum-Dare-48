using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeD
{
    public class Star : MonoBehaviour
    {
        void Start()
        {
            Destroy(gameObject, 3);
        }

        void Update()
        {
            transform.position -= Vector3.forward * Time.deltaTime * 200;
        }
    }
}
