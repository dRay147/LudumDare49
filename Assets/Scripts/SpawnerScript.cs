using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] List<GameObject> balls;
    private Vector3 direction;
    private bool occupied;
    // Start is called before the first frame update
    void Start()
    {
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBall(int ballNr)
    {
        // set direction for Ball
        direction = transform.parent.gameObject.transform.position - transform.position;
        //randomise a little rotation
        direction = Quaternion.AngleAxis(Random.Range(-30, 30), Vector3.up) * direction;
        GameObject tempBall = Instantiate(balls[ballNr-1], transform.position + new Vector3(0, balls[ballNr-1].transform.localScale.y, 0) / 2, transform.rotation);
        tempBall.GetComponent<EnemyMovement>().setDirection(direction);
    }

    public void setOccupied(bool occ)
    {
        occupied = occ;
    }

    public bool getOccupied()
    {
        return occupied;
    }
}
