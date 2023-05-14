using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;


    private PlayerInput inputActions;
    private InputAction movementAction;
    private InputAction accelerationAction;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    //void Update()
    //{
    //    groundedPlayer = controller.isGrounded;
    //    if (groundedPlayer && playerVelocity.y < 0)
    //    {
    //        playerVelocity.y = 0f;
    //    }

    //    Vector3 move = new Vector3(movementAction.ReadValue<Vector2>().x, 0, movementAction.ReadValue<Vector2>().y);
    //    controller.Move(move * Time.deltaTime * playerSpeed);

    //    if (move != Vector3.zero)
    //    {
    //        gameObject.transform.forward = move;
    //    }

    //    // Changes the height position of the player..
    //    //if (Input.GetButtonDown("Jump") && groundedPlayer)
    //    //{
    //    //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    //    //}

    //    //playerVelocity.y += gravityValue * Time.deltaTime;
    //    //controller.Move(playerVelocity * Time.deltaTime);
    //}



    private void Awake()
    {
        inputActions = new();
    }

    //private void OnEnable()
    //{
    //    movementAction = inputActions.Player.Movement;
    //    movementAction.Enable();

    //    accelerationAction = inputActions.Player.Acceleration;
    //    accelerationAction.Enable();

    //    //inputActions.Player.Acceleration.started += Accelerate;
    //    //inputActions.Player.Acceleration.canceled -= Accelerate;
    //    //inputActions.Player.Acceleration.Enable();
    //}

    private void Accelerate(InputAction.CallbackContext obj)
    {
        Debug.Log($"Accelerating");
    }

    private void OnDisable()
    {
        movementAction.Disable();
        accelerationAction.Disable();
    }

    private void FixedUpdate()
    {
        Debug.Log($"Movement values: {movementAction.ReadValue<Vector2>()}");
        Debug.Log($"Accelerating: {accelerationAction.ReadValue<float>()}");
    }
}
