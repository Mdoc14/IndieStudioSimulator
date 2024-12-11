using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    public static float sensMultiplier = 1;
    private Transform player;
    private float yRotation = 0; 
    PlayerMovement playerMovement;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent;
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (!playerMovement.canMove) return;
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * sensMultiplier * Time.deltaTime/Time.timeScale;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * sensMultiplier * Time.deltaTime/Time.timeScale;

        player.Rotate(Vector3.up, mouseX); 
        yRotation -= mouseY; 
        yRotation = Mathf.Clamp(yRotation, -85, 85);
        transform.localRotation = Quaternion.Euler(yRotation, 0, 0); 
    }
}