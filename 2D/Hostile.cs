using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour, IDamageable
{
    public float health = 10;

    public float speed = 3;

    bool moving = false;

    public GameObject enemyProjectile;

    public event System.Action<Hostile> OnHostileDeath;

    SpriteRenderer GFX;

    Transform relativeMotion;

    void Start()
    {
        GFX = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(GetInPosition());
    }

    void Update()
    {
        transform.position += Vector3.up * Input.GetAxis("Vertical") * -9 * Time.deltaTime;
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
        OnHostileDeath(this);
        Destroy(gameObject);
    }

    public void AssignRelativeMotion(Transform relativeMotionTransform)
    {
        relativeMotion = relativeMotionTransform;
    }

    IEnumerator GetInPosition()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(Random.Range(4, 9), Random.Range(-4.5f, 4.5f), 0);
        float t = 0;
        while(t < 1)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        StartCoroutine(Move());
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        Instantiate(enemyProjectile, transform.position, Quaternion.identity, relativeMotion);
        yield return new WaitForSeconds(Random.Range(.3f, 1));
        StartCoroutine(Shoot());
    }

    IEnumerator Move ()
    {
        if (!moving)
        {
            moving = true;
            Vector3 targetPosition = new Vector3(transform.position.x, Random.Range(-2f, 2f), 0);
            Vector3 dsp = targetPosition - transform.position;
            Vector3 dir = dsp.normalized;
            float sqrDstTravelled = 0;
            float dstTravelled = 0;
            float sqrDstDesired = dsp.sqrMagnitude;
            transform.eulerAngles = Vector3.forward * dir.y * -4;
            yield return new WaitForSeconds(2);
            while(sqrDstTravelled < sqrDstDesired)
            {
                transform.position += dir * speed * Time.deltaTime;
                dstTravelled += speed * Time.deltaTime;
                sqrDstTravelled = dstTravelled * dstTravelled;
                yield return null;
            }
            moving = false;
            StartCoroutine(Move());
        }
        yield return null;
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
