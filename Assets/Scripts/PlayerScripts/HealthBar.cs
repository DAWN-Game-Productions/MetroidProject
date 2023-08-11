using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void setHealth(int health)
    {
        slider.value = health;
    }


    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // change max health without healing
    public void changeMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    public void takeDamage(int damage){
        slider.value -= damage;
        checkDeath();
    }

    private void Update(){
        checkDeath();
    }

    public void checkDeath(){
        if(slider.value <= 0){
            Destroy(gameObject);
            Debug.Log("Death detected");
        }
    }
}
