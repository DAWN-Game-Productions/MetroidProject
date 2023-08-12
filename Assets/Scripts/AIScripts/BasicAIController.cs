using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIController : MonoBehaviour
{

    public int maxHealth = 100; // can probably get this number from somewhere else down the line
    public int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the health bar of the enemy. if the healthbar is 0 destroy the enemy
        if(checkDeath()){
            //Debug.Log("Enemy Killed Baby");
            Destroy(gameObject);
        }
    }

    public bool checkDeath(){
        if(currentHealth <= 0)
            return true;
        return false;
    }
}
