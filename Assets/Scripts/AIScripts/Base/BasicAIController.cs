using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIController : MonoBehaviour, IDamageable
{
    public enum Direction { right, left };
    public Direction enemyDirection = Direction.left;
    private SpriteRenderer enemySprite;
    [SerializeField] private GameObject VisionCone;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private GameObject bullet;
    private float scaleX;
    private float coneScaleX;
    
    public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; }
    
    private float shotCooldown = 1.8f;
    private float lastTimeFired;
    private Vector2 bulletVelocity = new Vector2(15f, 0);
    
    private IEnumerator directionCoroutine;
    private float directionChangeCooldown = 4.5f;
    
    void Awake()
    {
        coneScaleX = VisionCone.transform.localScale.x;
        scaleX = transform.localScale.x;
        directionCoroutine = SwitchFacingDirection();
        StartCoroutine(directionCoroutine);
        currentHealth = maxHealth;

        enemySprite = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.localScale = new Vector3((enemyDirection == Direction.right ? -scaleX : scaleX), transform.localScale.y, transform.localScale.z);
        VisionCone.transform.localScale = new Vector3((enemyDirection == Direction.right ? -coneScaleX : coneScaleX), VisionCone.transform.localScale.y, VisionCone.transform.localScale.z);
    }

    private IEnumerator SwitchFacingDirection(){
        while(true){
            yield return new WaitForSeconds(directionChangeCooldown);
            if(enemyDirection == Direction.left){
                enemyDirection = Direction.right;
            }
            else{
                enemyDirection = Direction.left;
            }
        }
    }

    #region bullet firing methods
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 3)
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
    #endregion

    #region healthbar interface methods
    public bool checkDeath()
    {
        if (currentHealth <= 0)
            return true;
        return false;
    }

    public void Death(){
        if(checkDeath())
            Destroy(gameObject);
    }

    public void Damage(float damageAmount){
        currentHealth -= damageAmount;
        Death();
    }
    #endregion

}
