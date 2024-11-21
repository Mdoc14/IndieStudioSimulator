using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private CharacterController controller;
    private Vector2 input;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(input.x, 0, input.y);
        movement = transform.TransformDirection(movement) * movementSpeed * Time.deltaTime;
        controller.Move(movement);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
