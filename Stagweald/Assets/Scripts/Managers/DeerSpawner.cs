using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class DeerSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject[] deer; //4 kinds of deer (for now)


    [Header("Spawning Specs")]
    public int spawnTime; //duration between spawning deer (in seconds)
    public float worldSize;
    private bool coroutineRunning;
    public LayerMask layerMask;
    public int distanceFromPlayer;


    void Start()
    {
        coroutineRunning = false;
    }

    void Update()
    {
        if(!coroutineRunning)
        {
            StartCoroutine(SpawnDeer());
        }
    }


    private IEnumerator SpawnDeer()
    {
        coroutineRunning = true;
        GameObject newDeer = deer[Random.Range(0,deer.Count())];
        //have a deer, now need to decide a spawn location
        //find a spawn location, away from a certain distance from the player, within world boundaries
        Vector3 position = GenerateSpawnLocation();
        Instantiate(newDeer, position, Quaternion.Euler(0, Random.Range(0, 360), 0));

        yield return new WaitForSeconds(spawnTime);
        coroutineRunning = false;
    }

    private Vector3 GenerateSpawnLocation()
    {
        //start way up in the air, raycast straight down, boom thats our spawn point
        Vector3 randomPos = new Vector3(Random.Range(-worldSize, worldSize), 100, Random.Range(-worldSize, worldSize));
        RaycastHit hit;
        if(Physics.Raycast(randomPos, Vector3.down, out hit, 120, layerMask))
        {
            if(Vector3.Distance(hit.point,player.transform.position) > distanceFromPlayer)
            {
                return hit.point;
            }
        }
        return GenerateSpawnLocation();
    }



}
