using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] float maxHealth;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHight;

    [SerializeField] private CapsuleCollider sphereAttack;

    private Vector3 lookDirection;

    float health;
    


    //Refecrences
    private CharacterController controller;
    private Animator anim;
    [SerializeField] private Image healthBar;



    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        sphereAttack.enabled = false;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        healthBar.GetComponent<HealthBarHandler>().SetHealthBarValue(health / maxHealth);
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        Move();
        ViewControl();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }
    }


    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(moveX, 0, moveZ);

        if(isGrounded)
        {
            if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                //Walk
                Walk();
            }
            else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                //Run
                Run();
            }
            else if(moveDirection == Vector3.zero)
            {
                //Idle
                Idle();       
            }

            moveDirection *= moveSpeed;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

        }

        controller.Move(moveDirection * Time.deltaTime);
        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0.0f, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1.0f, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity);
    }

    private void ViewControl()
    {
        if (moveDirection != Vector3.zero)
        {
            lookDirection = moveDirection;
        }

        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    private IEnumerator Attack()
    {
        // activate the trigger
        sphereAttack.enabled = true;

        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.15f);
        sphereAttack.enabled = false;
        yield return new WaitForSeconds(0.75f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }

    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

}
