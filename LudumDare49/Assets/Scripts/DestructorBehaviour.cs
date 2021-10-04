using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestructorBehaviour : MonoBehaviour
{
    private float overallpoints;
    private float previousPoints;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO update points in UI

        
        
    }

    public void increasePoints(float addedPoints)
    {
        overallpoints += addedPoints;
    }

    
}
