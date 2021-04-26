using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    bool wall1 = true;

    public GameObject wall;
    public GameObject evenWall;

    public GameObject asteroid;

    public GameObject target;

    public GameObject anomoly;

    Target currentTarget;

    int phase = 0;

    ThreeD.SpaceShip player;

    void Start()
    {
        player = FindObjectOfType<ThreeD.SpaceShip>();

        StartCoroutine(PickPhase());
        StartCoroutine(SpawnObstacles());
        StartCoroutine(SpawnAnomoly());

        SpawnTarget();
    }

    public void OnTargetDeath()
    {
        currentTarget.OnDeath -= OnTargetDeath;
        currentTarget.OnDeath -= player.OnTargetDeath;
        SpawnTarget();
    }

    void SpawnTarget()
    {
        currentTarget = Instantiate(target, Vector3.forward * 300, Quaternion.identity).GetComponent<Target>();
        currentTarget.OnDeath += OnTargetDeath;
        currentTarget.OnDeath += player.OnTargetDeath;
    }

    IEnumerator SpawnAnomoly()
    {
        Instantiate(anomoly, Vector3.forward * 300, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(6, 12));
        StartCoroutine(SpawnAnomoly());
    }

    IEnumerator SpawnObstacles()
    {
        yield return null;

        if (phase == 0)
        {
            yield return new WaitForSeconds(6);
            SpawnWall();
        } else if (phase == 1)
        {
            yield return new WaitForSeconds(Random.Range(.05f, .75f));
            SpawnAsteroid();
        }
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator PickPhase()
    {
        phase = Random.Range(0, 2);
        yield return new WaitForSeconds(Random.Range(30, 45));
        StartCoroutine(PickPhase());
    }

    void SpawnAsteroid()
    {
        Instantiate(asteroid, new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 300), Quaternion.identity);
    }

    void SpawnWall()
    {
        GameObject wallType = (wall1)? wall : evenWall;
        GameObject newWall = Instantiate(wallType, Vector3.forward * 100, Quaternion.Euler(Vector3.forward * Random.Range(0, 360)));
        wall1 = !wall1;
    }
}
