using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > 3.0f)
        {
            int randomSpawner = Random.Range(0, transform.childCount);
            int rand = Random.Range(1, 4);
            transform.GetChild(randomSpawner).GetComponent<SpawnerScript>().spawnBall(rand);
            elapsedTime = 0;
        }
    }
}
