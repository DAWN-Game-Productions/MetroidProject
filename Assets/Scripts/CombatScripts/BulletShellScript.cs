using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShellScript : MonoBehaviour
{
    private float MaxLifeTime = 2f;
    //private float Damage = 50f;
    private LayerMask EnemyLMask = 7;

    // Start is called before the first frame update
    void Start()
    { 
        Destroy(gameObject, MaxLifeTime);  
    }

    private void OnTriggerEnter (Collider other){
        Rigidbody targetRB = other.GetComponent<Rigidbody>();
        if(!targetRB)
            Debug.Log("No rigid body detected on collision");
        
        LayerMask lm = targetRB.GetComponent<LayerMask>();
        // if(!lm)
        //     Debug.Log("No layer mask detected on collision");

        

        // Collider target = Physics.OverlapSphere(transform.position, 0.1f, EnemyLMask)[0];
        // Rigidbody targetRB = target.GetComponent<Rigidbody>();
        // LayerMask lm = target.GetComponent<LayerMask>();

        if(targetRB && lm == EnemyLMask){
            HealthBar hp = targetRB.GetComponent<HealthBar>();
            if(!hp){
                Debug.Log("No hp");
            }
            else{
                hp.takeDamage(50);
            }
        }        

        Destroy(gameObject);
    }
}
