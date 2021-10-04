using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] List<GameObject> balls;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBall(int ballNr)
    {
        direction = transform.parent.gameObject.transform.position - transform.position;
        GameObject tempBall = Instantiate(balls[ballNr-1], transform.position + new Vector3(0, balls[ballNr-1].transform.localScale.y, 0) / 2, transform.rotation);
        tempBall.GetComponent<EnemyMovement>().setDirection(direction);
    }
}
