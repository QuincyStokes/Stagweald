using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.AI;

public class DeerSpawner : MonoBehaviour
{
    [HideInInspector]
    public static DeerSpawner Instance;

    [Header("References")]
    public GameObject player;
    public GameObject[] deer; //4 kinds of deer (for now)


    [Header("Spawning Specs")]
    public int spawnTime; //duration between spawning deer (in seconds)
    public float worldSize;
    private bool coroutineRunning;
    public LayerMask layerMask;
    public int minDistanceFromPlayer;
    public int maxDistanceFromPlayer;
    public int maxDeerAmount;
    private int currentDeerAmount;
    public int CurrentDeerAmount {
        get {return currentDeerAmount;}
        set {currentDeerAmount = value;}
    }


    void Start()
    {
        coroutineRunning = false;
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(!coroutineRunning && currentDeerAmount <= maxDeerAmount)
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
        CurrentDeerAmount += 1;

        yield return new WaitForSeconds(spawnTime);
        coroutineRunning = false;
    }

    private Vector3 GenerateSpawnLocation()
    {
        //start way up in the air, raycast straight down, boom thats our spawn point
        Vector3 randomPos = new Vector3(Random.Range(-worldSize, worldSize), 200, Random.Range(-worldSize, worldSize));
        RaycastHit hit;
        if(Physics.Raycast(randomPos, Vector3.down, out hit, 200, layerMask))
        {
            if(Vector3.Distance(hit.point, player.transform.position) > minDistanceFromPlayer &&
                Vector3.Distance(hit.point, player.transform.position) < maxDistanceFromPlayer)
            {
                randomPos = hit.point;
            }
        }

        NavMeshHit navHit;
        if(NavMesh.SamplePosition(randomPos, out navHit, 100f, NavMesh.AllAreas))
        {
            return navHit.position;
        }

        return GenerateSpawnLocation();
    }



}
