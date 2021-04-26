using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThreeD
{
    public class RelativeMotion : MonoBehaviour
    {
        SpaceShip player;

        public bool chase = false;
        public float speed = 0;

        void Start()
        {
            player = FindObjectOfType<SpaceShip>();
        }

        void Update()
        {
            transform.position += -player.transform.forward * Time.deltaTime * player.speed;

            if (chase)
            {
                Vector3 dsp = player.transform.position - transform.position;
                Vector3 dir = dsp.normalized;
                transform.position += dir * Time.deltaTime * speed;
            }
        }
    }
}
