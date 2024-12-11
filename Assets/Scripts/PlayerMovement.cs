using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private CharacterController controller;
    private Vector2 input;
    public bool canMove = true;
    public static bool collides = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        ToggleCollision();
    }

    void Update()
    {
        if (!canMove) return;
        Vector3 movement = new Vector3(input.x, 0, input.y);
        movement = transform.TransformDirection(movement) * movementSpeed * Time.deltaTime/Time.timeScale;
        controller.Move(movement);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void TogglePause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        MainMenuManager.Instance.TogglePause(canMove);
        canMove = !canMove;
    }

    public void ChangeSimulationSpeed(InputAction.CallbackContext context)
    {
        if (canMove && context.performed) WorldManager.Instance.AddSpeed(context.ReadValue<float>());
    }

    public void ToggleCollision()
    {
        if (collides) this.gameObject.layer = 0;
        else this.gameObject.layer = 6;
    }
}
