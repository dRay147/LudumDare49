using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorScript : MonoBehaviour
{
    private float previousPoints;
    private float points;

    // Start is called before the first frame update
    void Start()
    {
        previousPoints = points;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increasePoints(float addedPoints)
    {
        points += addedPoints;
    }
}
