using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PickUpItem _pickUpItem;
    [SerializeField] private LadderGrabbing _ladderGrabbing;
    [SerializeField] private DataSaver _dataSaver;
    [SerializeField] private Menu _pauseMenu;

    private Animator _playerAnimator;
    private PlayerInput _playerInput;
    private PlayerMoveController _playerMove;
    private InteractionEnviromentController _playerInteractionController;

    private bool _isCalcultaingTrajectory;
    private bool IsCalculatingTrajectory 
    {
        get => _isCalcultaingTrajectory;
        set
        {
            _isCalcultaingTrajectory = value;
            if (_isCalcultaingTrajectory)
            {
                _playerAnimator.SetTrigger("TossStart");
            }
            else
            {
                _playerAnimator.SetTrigger("TossEnd");
            }
        }
    }

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerMove = GetComponent<PlayerMoveController>();
        _playerAnimator = GetComponent<Animator>();
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
        _playerInput.Main.TakePause.performed += context => SwitchPauseMenu();
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
        _playerInput.Main.TakePause.performed -= context => SwitchPauseMenu();
    }

    private void FixedUpdate()
    {
        

        if (IsCalculatingTrajectory)
        {
            _pickUpItem.CalculateTrajectory();
        }
        else
        {
            _playerMove.Move(_playerInput.Main.Move.ReadValue<Vector2>());
            _ladderGrabbing.MoveUpDownOnLadder(_playerInput.Main.MoveOnLadder.ReadValue<Vector2>());
        }
    }

    private void Interaction()
    {
        if (_pauseMenu.IsPause)
        {
            return;
        }

        _playerInteractionController.EntityInteraction();
    }

    private void PickUp()
    {
        if (_pauseMenu.IsPause)
        {
            return;
        }

        _pickUpItem.PickUpOrDrop();
    }

    private void Throw()
    {
        if (_pauseMenu.IsPause)
        {
            return;
        }
        _pickUpItem.Throw();
        ToogleCalculateTrajectory();
    }

    private void ToogleCalculateTrajectory()
    {
        IsCalculatingTrajectory = !IsCalculatingTrajectory;
    }

    private void OnJump()
    {
        if (_pauseMenu.IsPause)
        {
            return;
        }

        _playerMove.Jump();
    }

    public void SwitchPauseMenu()
    {
        _pauseMenu.SwitchMenus();
    }

    private void SaveGame()
    {
        _playerMove.Move(_playerInput.Main.Move.ReadValue<Vector2>());
     //   _ladderGrabbing.MoveUpDownOnLadder(_playerInput.Main.MoveOnLadder.ReadValue<Vector2>());
        _dataSaver.Save();
    }

    private void LoadGame()
    {
        _dataSaver.Load();
    }
}
