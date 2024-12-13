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
    private Transform camTransform;
    private bool _freeMode = false;

    void Awake()
    {
        camTransform = GameObject.FindWithTag("MainCamera").transform;
        controller = GetComponent<CharacterController>();
        ToggleCollision();
    }

    void Update()
    {
        if (!canMove) return;
        Vector3 movement;
        if (_freeMode)
        {
            movement = (camTransform.forward * input.y + camTransform.right * input.x).normalized;
        }
        else
        {
            movement = new Vector3(input.x, 0, input.y);
            movement = transform.TransformDirection(movement);
        }
        movement *= movementSpeed * Time.deltaTime / Time.timeScale;
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
    }

    public void ChangeSimulationSpeed(InputAction.CallbackContext context)
    {
        if (canMove && context.performed) WorldManager.Instance.ChangeSimulationSpeed(context.ReadValue<float>() > 0);
    }

    public void ToggleCollision()
    {
        if (collides) this.gameObject.layer = 0;
        else this.gameObject.layer = 6;
    }

    public void ToggleFreeMode(InputAction.CallbackContext context)
    {
        if (context.performed) _freeMode = !_freeMode;
    }
}
