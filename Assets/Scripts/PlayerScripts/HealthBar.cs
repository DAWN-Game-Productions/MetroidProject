using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void updateHealth(float health)
    {
        slider.value = health;
    }

    public void setMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // change max health without healing
    public void changeMaxHealth(float health)
    {
        slider.maxValue = health;
    }

    public void takeDamage(float damage)
    {
        slider.value -= damage;
        Debug.Log(damage);
        Debug.Log(slider.value);
    }

    //This might just destroy the healthbar and not the enemy object. Should be placed in the enemy script I THINK

    // public void checkDeath(){
    //     if(slider.value <= 0){
    //         Destroy(gameObject);
    //         Debug.Log("Death detected");
    //     }
    // }
}
