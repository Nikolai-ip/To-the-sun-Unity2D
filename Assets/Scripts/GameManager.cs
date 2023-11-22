using Player;
using Player.StateMachines;
using UnityEngine;
using UnityEngine.SceneManagement;
using StateMachine = AISystem.StateMachine;


public class GameManager : MonoBehaviour
{
    private PlayerActor _playerActor;
    private PlayerStateMachine _playerStateMachine;
    private HandStateMachine _handStateMachine;
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

        Destroy(gameObject);
    }

    private void Initialize()
    {
        _playerActor = FindObjectOfType<PlayerActor>();
        _playerStateMachine = _playerActor.GetComponent<PlayerStateMachine>();
        _handStateMachine = _playerActor.GetComponent<HandStateMachine>();
    }

    public void ExecuteDeathOnPlayerDetection(Enemy enemy)
    {            
        _playerStateMachine.CurrentState.Discovered(enemy);
        _handStateMachine.CurrentState.Discovered(enemy);
    }
    

    public void GameOver()
    {
        _playerStateMachine.CurrentState.Exit();
        _handStateMachine.CurrentState.Exit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}