using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIController : MonoBehaviour
{

    public enum Direction { right, left };
    public Direction enemyDirection = Direction.left;

    private SpriteRenderer enemySprite;

    public int maxHealth = 100; // can probably get this number from somewhere else down the line
    public int currentHealth;
    [SerializeField] private GameObject VisionCone;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private GameObject bullet;

    private float shotCooldown = 1.8f;
    private float lastTimeFired;

    private float directionChangeCooldown = 6f;

    private Vector2 bulletVelocity = new Vector2(15f, 0);

    private IEnumerator directionCoroutine;
    //private int direction = 0;

    // Start is called before the first frame update
    void Awake()
    {
        directionCoroutine = SwitchFacingDirection();
        StartCoroutine(directionCoroutine);
        currentHealth = maxHealth;

        enemySprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        enemySprite.flipX = enemyDirection == Direction.left ? true : false;

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

    private IEnumerator SwitchFacingDirection(){
        while(true){
            yield return new WaitForSeconds(directionChangeCooldown);
            if(enemyDirection == Direction.left){
                fireTransform.localPosition = new Vector3(-fireTransform.localPosition.x, fireTransform.localPosition.y, fireTransform.localPosition.z);
                VisionCone.transform.localPosition = new Vector3(-VisionCone.transform.localPosition.x, VisionCone.transform.localPosition.y, VisionCone.transform.localPosition.z);
                enemyDirection = Direction.right;
            }
            else{
                fireTransform.localPosition = new Vector3(-fireTransform.localPosition.x, fireTransform.localPosition.y, fireTransform.localPosition.z);
                VisionCone.transform.localPosition = new Vector3(-VisionCone.transform.localPosition.x, VisionCone.transform.localPosition.y, VisionCone.transform.localPosition.z);
                enemyDirection = Direction.left;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 3) // if player detected 
        {
            if (Time.time - lastTimeFired >= shotCooldown)
            {
                if(enemyDirection == Direction.left)
                    instantiateBullet(bullet, fireTransform.position, -bulletVelocity);
                else if(enemyDirection == Direction.right)
                    instantiateBullet(bullet, fireTransform.position, bulletVelocity);
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
