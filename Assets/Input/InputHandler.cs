using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMoveController _playerMove;
    private InteractionEnviromentController _playerInteractionController;
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerMove = GetComponent<PlayerMoveController>();
        _playerInteractionController = GetComponent<InteractionEnviromentController>();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Main.Jump.performed += context => OnJump();
        _playerInput.Main.LampInteraction.performed += context => OnLampInteraction();
    }
    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Main.Jump.performed -= context => OnJump();
        _playerInput.Main.LampInteraction.performed -= context => OnLampInteraction();

    }
    private void OnJump()
    {
        _playerMove.Jump();
    }
    private void OnLampInteraction()
    {
        _playerInteractionController.LampInteraction();
    }

    private void FixedUpdate()
    {
        _playerMove.Move(_playerInput.Main.Move.ReadValue<Vector2>());
    }

}
