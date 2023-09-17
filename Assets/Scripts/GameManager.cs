using AISystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Animator _playerAnimator;
    private InputHandler _playerInputHandler;
    private Player _player;
    private StateMachine _enemyStateMachine;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
            return;
        }       
        _playerInputHandler.enabled = true;
        Destroy(gameObject);

    }
    private void Initialize()
    {
        _player = FindObjectOfType<Player>();
        _playerAnimator = _player.GetComponent<Animator>();
        _playerInputHandler = _player.GetComponent<InputHandler>();
        _player.Died += OnDeathPlayer;
        _player.DieIsOver += GameOver;
    }
    public void StartDeathScene(Enemy enemySender)
    {
        _enemyStateMachine = enemySender.GetComponent<StateMachine>();
        _enemyStateMachine?.ChangeState(_enemyStateMachine.ShootState);
        _playerInputHandler.enabled = false;
        if ((enemySender.transform.localScale.x * _player.transform.localScale.x) > 0)
        {
            _playerAnimator.SetTrigger("DeathFacingForward");
        }
        else
        {
            _playerAnimator.SetTrigger("DeathFacingBack");
        }
    }
    private void OnDeathPlayer()
    {
        _enemyStateMachine?.ShootState.FireAShot();
    }
    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }
}
