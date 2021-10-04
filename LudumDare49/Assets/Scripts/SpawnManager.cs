using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float elapsedTime;
    [SerializeField] private int lowerBound;
    [SerializeField] private int upperBound;
    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        elapsedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > 3.0f)
        {
            if(counter >= 6 && upperBound < 9)
            {
                upperBound += 1;
            }
            else if(counter >=9 && upperBound <9)
            {
                lowerBound += 1;
            }

            int randomSpawner = Random.Range(0, transform.childCount);
            int rand = Random.Range(lowerBound, upperBound);
            transform.GetChild(randomSpawner).GetComponent<SpawnerScript>().spawnBall(rand);
            elapsedTime = 0;
            counter += 1;
        }
    }
}
