using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] float maxHealth;
    [SerializeField] Scene scene;

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
    private bool attackCooldown;


    //Refecrences
    private CharacterController controller;
    private Animator anim;
    [SerializeField] private Image healthBar;



    // Start is called before the first frame update
    private void Start()
    {
        attackCooldown = false;
        health = maxHealth;
        sphereAttack.enabled = false;
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        anim.SetFloat("health", health);
        healthBar.GetComponent<HealthBarHandler>().SetHealthBarValue(health / maxHealth);
        if(health <= 0)
        {
            StartCoroutine(die());
        }

        Move();
        ViewControl();

        if (Input.GetKeyDown(KeyCode.Space) && attackCooldown == false)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKey("escape")) 
        {
            Application.Quit();
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
                Debug.Log("STANDIN");
                Idle();       
            }

            moveDirection *= moveSpeed;

        }

        controller.Move(moveDirection * Time.deltaTime);
        

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0.0f, 0.0f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.0f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1.0f, 0.0f, Time.deltaTime);
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
        attackCooldown = true;

        //something to do with upper and lower body ovement separated
        anim.SetLayerWeight(anim.GetLayerIndex("AttackLayer"), 1);
        anim.SetBool("Attack", true);

        yield return new WaitForSeconds(0.25f);
        sphereAttack.enabled = false;
        anim.SetBool("Attack", false);
        anim.SetLayerWeight(anim.GetLayerIndex("AttackLayer"), 0);
        yield return new WaitForSeconds(0.75f);
        attackCooldown = false;
        

    }

    public void takeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

    private IEnumerator die()
    {
        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("MyScene");
        Destroy(gameObject);

    }

}
