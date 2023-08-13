using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject player;
    private PlayerController playerCon;
    private float vertical; // for camera


    private void Awake(){
        playerCon = player.GetComponent<PlayerController>();
        mainCam.orthographicSize = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCon.isMoving)
            vertical = player.transform.position.y;

        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(player.transform.position.x, vertical, mainCam.transform.position.z), 0.02f);

    }

    public void Look(InputAction.CallbackContext context)
    {
        if (playerCon.grounded && !playerCon.isMoving && context.performed)
        {
            vertical = context.ReadValue<Vector2>().y * 5f;
        }
        else
        {
            vertical = player.transform.position.y;
        }
    }
}
