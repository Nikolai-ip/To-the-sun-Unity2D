using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMoveController _playerMove;
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerMove = GetComponent<PlayerMoveController>();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Main.Jump.performed += context => OnJump();
    }
    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Main.Jump.performed -= context => OnJump();
    }
    private void OnJump()
    {
        _playerMove.Jump();
    }

    private void FixedUpdate()
    {
        _playerMove.Move(_playerInput.Main.Move.ReadValue<Vector2>());
    }

}
