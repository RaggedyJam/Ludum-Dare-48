using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripWire : MonoBehaviour
{
    public LineRenderer wire;

    public Transform topEnd;
    public Transform bottomEnd;

    bool active = true;

    public LayerMask layerMask;

    void Start()
    {
        float dst = Random.Range(1, 6);
        topEnd.position =  new Vector3 (15, dst / 2, 0);
        bottomEnd.position = new Vector3 (15, -dst / 2, 0);
        Vector3[] points = {topEnd.localPosition + Vector3.up * .5f, bottomEnd.localPosition - Vector3.up * .5f};
        wire.SetPositions(points);

        transform.eulerAngles = Vector3.forward * Random.Range(0, 360);
        transform.position = Vector3.up * Random.Range(-3, 3) + Vector3.right * 15;

        Destroy(gameObject, 10);
    }

    void Update()
    {
        transform.position -= Vector3.right * Time.deltaTime * 3;
        if (active)
        {
            RaycastHit2D hit = Physics2D.Linecast(topEnd.position, bottomEnd.position);

            if (hit)
            {
                print(hit.collider.name);
                SpaceShip player = hit.collider.GetComponent<SpaceShip>();
                if (player != null)
                {
                    player.TakeDamage(2);
                    active = false;
                }
            }
        }
    }
}
