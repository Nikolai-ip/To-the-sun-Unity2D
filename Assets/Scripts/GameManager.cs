using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateMachine = AISystem.StateMachine;

public class GameManager : MonoBehaviour
{
    private StateMachine _enemyStateMachine;
    private PlayerActor _playerActor;
    private Animator _playerAnimator;
    private InputHandler _playerInputHandler;
    public static GameManager Instance { get; private set; }

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
        _playerActor = FindObjectOfType<PlayerActor>();
        _playerAnimator = _playerActor.GetComponent<Animator>();
        _playerInputHandler = _playerActor.GetComponent<InputHandler>();
        _playerActor.Died += OnDeathPlayerActor;
        _playerActor.DieIsOver += GameOver;
    }

    public void StartDeathScene(Enemy enemySender)
    {
        _enemyStateMachine = enemySender.GetComponent<StateMachine>();
        _enemyStateMachine?.ChangeState(_enemyStateMachine.ShootState);
        _playerInputHandler.enabled = false;
        if (enemySender.transform.localScale.x * _playerActor.transform.localScale.x > 0)
            _playerAnimator.SetTrigger("DeathFacingForward");
        else
            _playerAnimator.SetTrigger("DeathFacingBack");
    }

    private void OnDeathPlayerActor()
    {
        _enemyStateMachine?.ShootState.FireAShot();
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}