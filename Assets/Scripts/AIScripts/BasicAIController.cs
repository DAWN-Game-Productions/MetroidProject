using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIController : MonoBehaviour
{

    public int maxHealth = 100; // can probably get this number from somewhere else down the line
    public int currentHealth;
    [SerializeField] private GameObject VisionCone;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private GameObject bullet;

    private float shotCooldown = 1.8f;
    private float lastTimeFired;

    private Vector2 bulletVelocity = new Vector2(15f, 0);

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the health bar of the enemy. if the healthbar is 0 destroy the enemy
        if (checkDeath())
        {
            //Debug.Log("Enemy Killed Baby");
            Destroy(gameObject);
        }
    }

    public bool checkDeath()
    {
        if (currentHealth <= 0)
            return true;
        return false;
    }

    // IEnumerator OnTriggerStay2D(Collider2D other){
    //     yield return new WaitForSeconds(shotCooldown);
    //     if(other.gameObject.layer == 3){
    //         instantiateBullet(bullet, fireTransform.position, -bulletVelocity);
    //     }
    // }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
        {
            if (Time.time - lastTimeFired >= shotCooldown)
            {
                instantiateBullet(bullet, fireTransform.position, -bulletVelocity);
                lastTimeFired = Time.time;
            }
            else
                return;
        }
    }

    private void instantiateBullet(GameObject bullet, Vector3 position, Vector2 velocity)
    {
        GameObject bulletInstance = Instantiate(bullet, position, Quaternion.Euler(0, 0, 90));
        bulletInstance.GetComponent<BulletShellController>().setOrigin(0);
        Rigidbody2D rbBulletInstance = bulletInstance.GetComponent<Rigidbody2D>();
        rbBulletInstance.velocity = velocity;
    }
}
