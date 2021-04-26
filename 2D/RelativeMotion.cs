using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMotion : MonoBehaviour
{
    void Update()
    {
        transform.position -= Vector3.up * Input.GetAxis("Vertical") * 6 * Time.deltaTime;
    }
}
