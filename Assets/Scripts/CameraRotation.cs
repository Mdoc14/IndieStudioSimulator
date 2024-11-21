using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    private Transform player;
    private float yRotation = 0; 

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        player.Rotate(Vector3.up, mouseX); 
        yRotation -= mouseY; 
        yRotation = Mathf.Clamp(yRotation, -85, 85);
        transform.localRotation = Quaternion.Euler(yRotation, 0, 0); 
    }
}