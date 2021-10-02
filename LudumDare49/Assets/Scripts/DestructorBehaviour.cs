using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        // collide with destructor, get destroyed
        Destroy(collision.collider.gameObject);
        
    }

}
