using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHostiles : MonoBehaviour
{
    public GameObject hostile;

    int maxHostiles;
    int currentHostiles = 0;

    bool spawning = false;

    public Transform relativeMotion;

    SpaceShip player;

    void Start()
    {
        player = FindObjectOfType<SpaceShip>();
        maxHostiles = Random.Range(2, 7);
        StartCoroutine(SpawnHostile());
    }

    IEnumerator SpawnHostile()
    {
        if (currentHostiles < maxHostiles)
        {
            spawning = true;
            yield return new WaitForSeconds(Random.Range(1, 4));
            Vector3 spawnPosition = new Vector3(15, Random.Range(-4.5f, 4.5f), 0);
            GameObject newHostile = Instantiate(hostile, spawnPosition, Quaternion.identity);
            Hostile hostileScript = newHostile.GetComponent<Hostile>();
            hostileScript.OnHostileDeath += HostileDeath;
            hostileScript.OnHostileDeath += player.OnHostileDeath;
            hostileScript.AssignRelativeMotion(relativeMotion);
            currentHostiles++;
            StartCoroutine(SpawnHostile());
        } else
        {
            spawning = false;
        }
        yield return null;
    }

    void HostileDeath(Hostile hostile)
    {
        hostile.OnHostileDeath -= HostileDeath;
        hostile.OnHostileDeath -= player.OnHostileDeath;
        currentHostiles--;
        if (!spawning && currentHostiles == 0)
        {
            maxHostiles = Random.Range(2, 7);
            StartCoroutine(SpawnHostile());
        }
    }
}
