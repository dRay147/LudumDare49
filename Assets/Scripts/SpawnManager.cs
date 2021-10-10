using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float elapsedTime;
    [SerializeField] private int lowerBound;
    [SerializeField] private int upperBound;
    private int spawnAmount;

    private float nextIncreaseLower;   
    private float timeBetweenIncreasesLower;
    private float nextIncreaseUpper;
    private float timeBetweenIncreasesUpper;
    private float nextIncreaseAmount;
    private float timeBetweenIncreasesAmount;

    private float nextSpawn;
    private float timeBetweenNextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        nextIncreaseLower = 70.0f;
        timeBetweenIncreasesLower = 45.0f;
        nextIncreaseUpper = 45.0f;
        timeBetweenIncreasesUpper = 20.0f;
        nextIncreaseAmount = 20.0f;
        timeBetweenIncreasesAmount = 30.0f;
        spawnAmount = 1;
        nextSpawn = 2.0f;
        timeBetweenNextSpawn = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(Time.time > nextSpawn)
        {
            if(Time.time > nextIncreaseUpper && upperBound < 9)
            {
                nextIncreaseUpper += timeBetweenIncreasesUpper;
                upperBound += 1;
            }
            
            if(Time.time > nextIncreaseLower && lowerBound < 7)
            {
                nextIncreaseLower += timeBetweenIncreasesLower;
                lowerBound += 1;
            }

            if (Time.time > nextIncreaseAmount && spawnAmount <= 4)
            {
                nextIncreaseAmount += timeBetweenIncreasesAmount;
                spawnAmount += 1;
                timeBetweenNextSpawn += 1.0f;
            }

            for (int i = 0; i < spawnAmount; i++)
            {
                //choose random spawner
                int randomSpawner = Random.Range(0, transform.childCount);
                //choose random ball
                int rand = Random.Range(lowerBound, upperBound);
                SpawnerScript spawner = transform.GetChild(randomSpawner).GetComponent<SpawnerScript>();

                while(spawner.getOccupied())
                {
                    randomSpawner = (randomSpawner + 1) % transform.childCount;
                    spawner = transform.GetChild(randomSpawner).GetComponent<SpawnerScript>();
                    Debug.Log("SPAWNER BLOCKED; RESET");
                }
                spawner.spawnBall(rand);
                spawner.setOccupied(true);
                
            }
            // free the spawners again
            foreach(Transform child in transform)
            {
                child.gameObject.GetComponent<SpawnerScript>().setOccupied(false);
            }

            nextSpawn += timeBetweenNextSpawn;
        }
    }

    private void chooseSpawner() { 
    }
}
