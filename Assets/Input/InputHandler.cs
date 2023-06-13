using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PickUpItem _pickUpItem;
    [SerializeField] private LadderGrabbing _ladderGrabbing;
    [SerializeField] private DataSaver _dataSaver;

    private PlayerInput _playerInput;
    private PlayerMoveController _playerMove;
    private InteractionEnviromentController _playerInteractionController;

    private bool IsCalculatingTrajectory;

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
        _playerInput.Main.PickUpItem.performed += context => PickUp();
        _playerInput.Main.Interaction.performed += context => Interaction();
        _playerInput.Main.ThrowItem.started += context => ToogleCalculateTrajectory();
        _playerInput.Main.ThrowItem.performed += context => Throw();
        _playerInput.Main.SaveGame.performed += context => SaveGame();
        _playerInput.Main.LoadGame.performed += context => LoadGame();
    }

    private void OnDisable()
    {
        _playerInput.Disable();

        _playerInput.Main.Jump.performed -= context => OnJump();
        _playerInput.Main.PickUpItem.performed -= context => PickUp();
        _playerInput.Main.Interaction.performed -= context => Interaction();
        _playerInput.Main.ThrowItem.started -= context => ToogleCalculateTrajectory();
        _playerInput.Main.ThrowItem.performed -= context => Throw();
        _playerInput.Main.SaveGame.performed -= context => SaveGame();
        _playerInput.Main.LoadGame.performed -= context => LoadGame();

    }

    private void FixedUpdate()
    {
        _playerMove.Move(_playerInput.Main.Move.ReadValue<Vector2>());
        _ladderGrabbing.MoveUpDownOnLadder(_playerInput.Main.MoveOnLadder.ReadValue<Vector2>());

        if (IsCalculatingTrajectory)
        {
            _pickUpItem.CalculateTrajectory();
        }
    }

    private void Interaction()
    {
        _playerInteractionController.EntityInteraction();
    }

    private void PickUp()
    {
        _pickUpItem.PickUpOrDrop();
    }

    private void Throw()
    {
        _pickUpItem.Throw();
        ToogleCalculateTrajectory();
    }

    private void ToogleCalculateTrajectory()
    {
        IsCalculatingTrajectory = !IsCalculatingTrajectory;
    }

    private void OnJump()
    {
        _playerMove.Jump();
    }

    private void SaveGame()
    {
        _dataSaver.Save();
    }

    private void LoadGame()
    {
        _dataSaver.Load();
    }
}
