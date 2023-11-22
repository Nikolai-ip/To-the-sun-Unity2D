using Player;
using Player.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private DataSaver _dataSaver;
    [SerializeField] private Menu _pauseMenu; 
    private StateMachinesController _playerStateMachine;


    private PlayerInput _playerInput;
    private InteractionEnvironmentController _playerInteractionController;
    private PlayerMoveController _playerMove;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerMove = GetComponent<PlayerMoveController>();
        _playerInteractionController = GetComponent<InteractionEnvironmentController>();
        _playerStateMachine = GetComponent<StateMachinesController>();
    }

    private void FixedUpdate()
    {
        if (_pauseMenu.IsPause) return;
        _playerStateMachine.InputToStateMachine(_playerInput.Main.MoveXAxis);
        _playerStateMachine.InputToStateMachine(_playerInput.Main.MoveYAxis);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Main.Jump.performed += JumpPerformed;
        _playerInput.Main.PickUpItem.performed += PickUpItemPerformed;
        _playerInput.Main.Interaction.performed += InteractionPerformed;
        _playerInput.Main.ThrowItem.started += ThrowItem;
        _playerInput.Main.ThrowItem.canceled += ThrowItem;
        _playerInput.Main.SaveGame.performed += SaveGame;
        _playerInput.Main.LoadGame.performed += LoadGame;
        _playerInput.Main.TakePause.performed += SwitchPauseMenu;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Main.Jump.performed -= JumpPerformed;
        _playerInput.Main.PickUpItem.performed -= PickUpItemPerformed;
        _playerInput.Main.Interaction.performed -= InteractionPerformed;
        _playerInput.Main.ThrowItem.started -= ThrowItem;
        _playerInput.Main.ThrowItem.canceled -= ThrowItem;
        _playerInput.Main.SaveGame.performed -= SaveGame;
        _playerInput.Main.LoadGame.performed -= LoadGame;
        _playerInput.Main.TakePause.performed -= SwitchPauseMenu;
    }

    private void JumpPerformed(InputAction.CallbackContext ctx)
    {
        if (_pauseMenu.IsPause) return;

        _playerStateMachine.InputToStateMachine(_playerInput.Main.Jump);
    }

    private void PickUpItemPerformed(InputAction.CallbackContext ctx)
    {
        if (_pauseMenu.IsPause) return;
        _playerStateMachine.InputToStateMachine(_playerInput.Main.PickUpItem);
    }

    private void InteractionPerformed(InputAction.CallbackContext ctx)
    {
        if (_pauseMenu.IsPause) return;
        _playerStateMachine.InputToStateMachine(_playerInput.Main.Interaction);
    }

    private void ThrowItem(InputAction.CallbackContext ctx)
    {
        if (_pauseMenu.IsPause) return;
        _playerStateMachine.InputToStateMachine(_playerInput.Main.ThrowItem);
    }

    private void OnJump(InputAction _)
    {
        if (_pauseMenu.IsPause) return;
        _playerMove.Jump();
    }

    private void SwitchPauseMenu(InputAction.CallbackContext ctx)
    {
        _pauseMenu.SwitchMenus();
    }

    private void SaveGame(InputAction.CallbackContext ctx)
    {
        _dataSaver.Save();
    }

    private void LoadGame(InputAction.CallbackContext ctx)
    {
        _dataSaver.Load();
    }
}