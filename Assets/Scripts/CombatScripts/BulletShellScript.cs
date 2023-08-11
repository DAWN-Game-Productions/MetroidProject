using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellScript : MonoBehaviour
{
    private float MaxLifeTime = 2f;
    private int Damage = 50;
    private LayerMask EnemyLMask = 7;
    [SerializeField] private GameObject bulletPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, MaxLifeTime);  
    }

    private void OnTriggerEnter2D (Collider2D other){
        // Create Interface for the enemies that each different kind of enemy will implement. Change BasicAIScript to search
        //      for said interface(Down the line, not needed right now)
        BasicAIScript ai = other.GetComponent<BasicAIScript>();

        if(!ai)
            Debug.Log("object not found");
        
        if(ai.gameObject.layer == EnemyLMask){
            ai.currentHealth -= Damage;
        }        

        Destroy(bulletPrefab);
    }
}