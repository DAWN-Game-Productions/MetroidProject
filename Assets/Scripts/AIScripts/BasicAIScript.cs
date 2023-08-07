using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIScript : MonoBehaviour
{

    public HealthBar enemyHealth;
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        enemyHealth.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealth.setHealth(currentHealth);
    }
}
