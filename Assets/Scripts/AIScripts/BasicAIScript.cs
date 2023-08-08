using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAIScript : MonoBehaviour
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

    }
}
