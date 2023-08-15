using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellController : MonoBehaviour
{
    private float MaxLifeTime = 1.5f;
    private int Damage = 50;
    private LayerMask EnemyLMask = 7;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(MaxLifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D (Collider2D other){
        // Create Interface for the enemies that each different kind of enemy will implement. Change BasicAIScript to search
        // for said interface(Down the line, not needed right now)
        if(other.gameObject.layer == 6){
            Destroy(gameObject);
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

        Destroy(gameObject);
    }
}