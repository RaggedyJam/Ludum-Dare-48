using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeD
{
    public class ShipProjectile : MonoBehaviour
    {
        public GameObject hitImpact;

        public LayerMask hitLayer;

        void Start()
        {
            Destroy(gameObject, 3);
        }

        void Update()
        {
            transform.position += transform.forward * Time.deltaTime * 100;

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100 * Time.deltaTime, hitLayer))
            {
                ThreeD.IDamageable hitObject = hit.collider.gameObject.GetComponent<IDamageable>();
                if (hitObject != null)
                {
                    hitObject.TakeDamage(6);
                    Destroy(gameObject);
                }
                GameObject newHitImpact = Instantiate(hitImpact, hit.point, Quaternion.identity);
                Destroy(newHitImpact, .6f);
            }
        }
    }
}
