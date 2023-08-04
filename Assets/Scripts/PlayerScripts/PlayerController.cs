using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //private float distance = 10;

    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private bool grounded;

    //For jumping
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject player;

    private void Awake(){
        rb2D = transform.GetComponent<Rigidbody2D>();
        bc2D = transform.GetComponent<BoxCollider2D>();
        
        mainCam.orthographicSize = 20f;
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

        if(grounded && Input.GetKeyDown(KeyCode.Space)){
            rb2D.velocity = Vector2.up * jumpVelocity;
        }

        // Smooth look up and down camera movements
        bool isMoving = rb2D.velocity != Vector2.zero;
        if (grounded && !isMoving && Input.GetKey(KeyCode.W))
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(mainCam.transform.position.x, player.transform.position.y + 5f, mainCam.transform.position.z), 0.02f);
        }
        else if (grounded && !isMoving && Input.GetKey(KeyCode.S))
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(mainCam.transform.position.x, player.transform.position.y - 5f, mainCam.transform.position.z), 0.02f);
        }
        else
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, mainCam.transform.position.z), 0.02f);
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
