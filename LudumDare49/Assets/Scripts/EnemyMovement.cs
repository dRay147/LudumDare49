using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour    
{
    [SerializeField] private float speed;
    private Rigidbody rigid;

    [SerializeField] Vector3 lastVelocity;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rigid.velocity;
        if (lastVelocity.magnitude < 3)
        {
            Shoot();
        }
    }

    private void Shoot()
    {      
        Vector3 direction = new Vector3(Random.Range(-10, 10), 0.0f, Random.Range(-10, 10));
        direction = direction.normalized;
        Debug.Log(direction);
        rigid.AddForce(speed * direction);
    }

    private void ShootDirected(Collider target)
    {
        Vector3 direction = gameObject.transform.position - target.transform.position;
        
        rigid.AddForce(speed * direction);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // collide with wall, deflect
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rigid.velocity = direction * Mathf.Max(speed, 0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Destructor"))
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Attack"))
        {
            ShootDirected(other);
        }
        

    }
}
