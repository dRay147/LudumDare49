using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour    
{
    [SerializeField] private float speed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float resetMagnitude;
    [SerializeField] private float damage;
    [SerializeField] private float spawnDelayTime;
    [SerializeField] private Color col;
    private Vector3 direction;
    private GameObject textObject;
    [SerializeField] Text textObj;

    [SerializeField] private float pointValue;

    private bool shootPermission = false;

    private Rigidbody rigid;

    [SerializeField] Vector3 lastVelocity;


    // Start is called before the first frame update
    void Start()
    {
        col.a = 255;
        textObject = GameObject.FindWithTag("CounterText");
        textObj = textObject.GetComponent<Text>();
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(spawnDelay());
        
 
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rigid.velocity;
        if (lastVelocity.magnitude < resetMagnitude && shootPermission)
        {
            ShootRandomised();
        }
    }

    private void Shoot()
    {
        direction = direction.normalized;
        //Debug.Log(direction);
        rigid.AddForce(speed * direction);
    }
    private void ShootRandomised()
    {      
        setDirection(new Vector3(Random.Range(-10, 10), 0.0f, Random.Range(-10, 10)).normalized);
        //Debug.Log(direction);
        Shoot();
    }

    private void ShootDirected(Collider target)
    {
        direction = (gameObject.transform.position - target.transform.position).normalized;
        
        rigid.AddForce(playerSpeed * direction);

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
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(damage);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Destructor"))
        {
            //other.gameObject.GetComponent<DestructorScript>().increasePoints(pointValue);
            textObj.text = (float.Parse(textObj.text) + pointValue).ToString();

            textObj.color = col;
            Destroy(gameObject);


        }
        else if(other.gameObject.CompareTag("Attack"))
        {
            ShootDirected(other);
        }
        
    }

    private IEnumerator spawnDelay()
    {
        rigid.useGravity = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(spawnDelayTime);

        rigid.useGravity = true;
        GetComponent<Collider>().enabled = true;
        Shoot();
        yield return new WaitForSeconds(2.0f);
        shootPermission = true;
    }

    public void setDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

}
