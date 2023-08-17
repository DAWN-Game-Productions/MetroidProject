using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{

    public GameObject remaining;
    public GameObject total;
    [SerializeField] GameObject player;
    //private PlayerController playercon;


    private void Awake()
    {
        //playercon = player.GetComponent<PlayerController>();
        updateAmmoHUD();
    }

    // Update is called once per frame
    void Update()
    {
        updateAmmoHUD();
    }

    private void updateAmmoHUD()
    {
        if (player.GetComponent<PlayerController>().bulletsRemaining < 0)
        {
            remaining.GetComponent<TextMeshProUGUI>().text = "0";
        }
        else
        {
            remaining.GetComponent<TextMeshProUGUI>().text = (player.GetComponent<PlayerController>().bulletsRemaining).ToString();
        }
        total.GetComponent<TextMeshProUGUI>().text = (player.GetComponent<PlayerController>().magSize).ToString();
    }
}
