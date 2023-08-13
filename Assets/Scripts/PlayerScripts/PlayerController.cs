using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //private float distance = 10;

    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    public bool grounded;
    private float horizontal;
    private float moveSpeed = 10f;
    private float jumpVelocity = 10f;
    public bool isMoving;
    private float verticalDeadzone = 0.6f;
    public int maxHealth = 100;
    public int currentHealth;
    public int bulletsRemaining;
    public int magSize = 9;
    private float FireTime;
    private bool isReloaded = true;

    public HealthBar healthBar;
    // will use later to flip player sprite depending on direction
    //bool isFacingRight = true;


    //For jumping
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform fireTrans;


    private void Awake(){

        rb2D = transform.GetComponent<Rigidbody2D>();
        bc2D = transform.GetComponent<BoxCollider2D>();
        
        //mainCam.orthographicSize = 20f;

        bulletsRemaining = magSize;

        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

    }

    // Update is called once per frame
    private void Update()
    {
        if((Time.time - FireTime) > 1f && isReloaded == false){
            isReloaded = true;
            bulletsRemaining = 9;
        }

        healthBar.setHealth(currentHealth);
        // update velocity based on horizontal component
        rb2D.velocity = new Vector2(horizontal * moveSpeed, rb2D.velocity.y);
        // Smooth look up and down camera movements
        isMoving = rb2D.velocity != Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 6 && !grounded){
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 6 && grounded){
            grounded = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (Mathf.Abs(context.ReadValue<Vector2>().y) > verticalDeadzone)
        {
            horizontal = 0f;
        }
        else
        {
            horizontal = context.ReadValue<Vector2>().x;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && grounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpVelocity);
        }


        // Adds functionality for variable jump (if you hold space you jump higher)
        if (context.canceled && rb2D.velocity.y > 0f)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
        }
    }

    public void SecondaryFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            bulletsRemaining--;
            if(bulletsRemaining <= 1){
                FireTime = Time.time;
                isReloaded = false;
            }

            if(isReloaded){
                GameObject bulletInstance = Instantiate(bullet, fireTrans.position, fireTrans.rotation);
            }
            else
                return;
        }
    }

}
