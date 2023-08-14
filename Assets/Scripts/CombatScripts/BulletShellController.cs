using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellController : MonoBehaviour
{
    private float MaxLifeTime = 2f;
    private int Damage = 50;
    private LayerMask EnemyLMask = 7;
    [SerializeField] private GameObject bulletPrefab;
    private Vector2 firePos;

    [SerializeField] private Vector2 bulletTrajectory;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, MaxLifeTime);  
    }

    private void Update(){

        if(firePos != null && (Vector3.Distance(gameObject.transform.position, firePos) > 17f))
            Destroy(bulletPrefab);
    }

    private void Awake(){
        firePos = gameObject.transform.position;
        
        Rigidbody2D bulletRB = gameObject.GetComponent<Rigidbody2D>();
        bulletRB.velocity = bulletTrajectory;
    }

    private void OnTriggerEnter2D (Collider2D other){
        // Create Interface for the enemies that each different kind of enemy will implement. Change BasicAIScript to search
        //      for said interface(Down the line, not needed right now)
        if(other.gameObject.layer == 6){
            Destroy(bulletPrefab);
            return;
        }
        else if(other.gameObject.layer == 0){
            Debug.Log("We hit the player's collider");
            return;
        }
        

        BasicAIController ai = other.GetComponent<BasicAIController>();

        if(!ai)
            Debug.Log("object not found");
        
        if(ai.gameObject.layer == EnemyLMask){
            ai.currentHealth -= Damage;
        }

        Destroy(bulletPrefab);
    }
}