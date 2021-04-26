using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThreeD
{
    public class SpaceShip : MonoBehaviour
    {
        public float speed = 200;

        public float fireRate = 12;
        float downtime = 0;
        float cooldown = 0;

        float pitch;
        float roll;
        float yaw;

        public float health = 50;

        public GameObject shipProjectile;

        Rigidbody rb;

        public Text dstScore;
        public Text killCountScore;
        public Text hp;
        public GameObject respawnText;

        AudioSource gun;

        int killCount = 0;
        float dst = 0;

        bool dead = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            gun = GetComponent<AudioSource>();
            cooldown = 1 / fireRate;
            killCountScore.text = "Kill Count: 0";
        }

        void Update()
        {
            if (!dead)
            {
                pitch = Input.GetAxis("Vertical") * Time.deltaTime * 50;
                roll = -Input.GetAxis("Horizontal") * Time.deltaTime * 75;
                yaw = Input.GetAxis("Yaw") * Time.deltaTime * 20;

                transform.Rotate(pitch, yaw, roll, Space.Self);

                if (Input.GetKey(KeyCode.Space))
                {
                    Shoot();
                }

                hp.text = "Health: " + health;

                downtime -= Time.deltaTime;

                speed += .16f * Time.deltaTime;
                dst += Time.deltaTime * speed;

                dstScore.text = "Dst Made: " + dst;
            } else
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Respawn();
                }
            }
        }

        void Shoot()
        {
            if (downtime <= 0)
            {
                Instantiate(shipProjectile, transform.position, transform.rotation);
                downtime = cooldown;
                gun.Play();
            }
        }

        void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public void OnTargetDeath()
        {
            killCount++;
            killCountScore.text = "Kill Count: " + killCount;
        }

        void Die()
        {
            speed = 20;
            dead = true;
            respawnText.SetActive(true);
            killCount = 0;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        void Respawn()
        {
            dead = false;
            respawnText.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Wall")
            {
                TakeDamage(10);
                col.enabled = false;
                rb.angularVelocity = Random.onUnitSphere * 2;
                StartCoroutine(StabalizeShip());
            } else if (col.gameObject.tag == "Asteroid")
            {
                TakeDamage(20);
                col.enabled = false;
                col.GetComponent<Asteroid>().Die();
                rb.angularVelocity = Random.onUnitSphere * 2;
                StartCoroutine(StabalizeShip());
            } else if (col.gameObject.tag == "Anomoly")
            {
                col.enabled = false;
                rb.angularVelocity = Random.onUnitSphere * 3;
                StartCoroutine(StabalizeShip());
            }
        }

        IEnumerator StabalizeShip()
        {
            yield return new WaitForSeconds(6);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
