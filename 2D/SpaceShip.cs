using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{

    public float speed = 2;

    public GameObject projectile;
    public float fireRate = 2;
    float cooldown;
    float downTime = 0;

    public float health = 25;

    SpriteRenderer GFX;

    public Transform relativeMotion;

    int hostilesKilled = 0;

    public Text killCount;

    void Start()
    {
        cooldown = 1 / fireRate;
        GFX = GetComponentInChildren<SpriteRenderer>();
        killCount.text = "Hostiles Killed: " + hostilesKilled;
    }

    void Update()
    {
        //Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //transform.position += Vector3.up * (Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
        relativeMotion.position -= Vector3.up * (Input.GetAxis("Vertical") * speed * Time.deltaTime);
        //relativeMotion.eulerAngles = Vector3.forward * Input.GetAxis("Vertical") * -4;

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }

        downTime -= Time.deltaTime;

        transform.eulerAngles = Vector3.forward * Input.GetAxis("Vertical") * 4;
    }

    void Shoot()
    {
        if (downTime <= 0)
        {
            Instantiate(projectile, transform.position + Vector3.right * .9f + Vector3.up * -.13f, Quaternion.identity, relativeMotion);
            downTime = cooldown;
        }
    }

    public void OnHostileDeath(Hostile hostile)
    {
        hostilesKilled++;
        killCount.text = "Hostiles Killed: " + hostilesKilled;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(AnimateDamage());
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator AnimateDamage ()
    {
        float time = 0;
        float t = 0;
        bool normalColour = true;
        while(time < .5f)
        {
            GFX.color = Color.Lerp(Color.white, Color.red, t);
            if (normalColour)
            {
                t += Time.deltaTime * 20;
                if (t >= 1)
                {
                    normalColour = false;
                }
            } else
            {
                t -= Time.deltaTime * 20;
                if (t <= 0)
                {
                    normalColour = true;
                }
            }
            time += Time.deltaTime;
            yield return null;
        }

        GFX.color = Color.white;
    }

}
