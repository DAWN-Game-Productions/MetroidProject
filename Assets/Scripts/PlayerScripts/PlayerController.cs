using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private bool grounded;

    //For jumping
    [SerializeField] private LayerMask platformLayerMask;

    private void Awake(){
        rb2D = transform.GetComponent<Rigidbody2D>();
        bc2D = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float moveSpeed = 10f;
        float jumpVelocity = 3.5f; 

        if(Input.GetKey(KeyCode.A)){
            rb2D.velocity = new Vector2(-moveSpeed, rb2D.velocity.y);
        }
        else if(Input.GetKey(KeyCode.D)){
            rb2D.velocity = new Vector2(+moveSpeed, rb2D.velocity.y);
        }
        else{
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }

        //REMOVE KEYCODE W LATER
        if(grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))){
            rb2D.velocity = Vector2.up * jumpVelocity;
        }

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

}
