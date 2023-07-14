using UnityEngine;

public class GameController : MonoBehaviour, ILoadable
{
    private Vector2 _lastCheckpointPosition;

    [SerializeField] private Player _player;

    private void Start()
    {
        _player.Died += RespawnPlayer;
        Checkpoint.CheckpointReached += UpdateCheckpoint;
    }

    private void UpdateCheckpoint(Checkpoint newCheckpoint)
    {
        var position = newCheckpoint.GetComponent<Transform>().position;
        _lastCheckpointPosition = position;
    }

    public void RespawnPlayer()
    {
        var transform = _player.GetComponent<Transform>();
        transform.position = _lastCheckpointPosition;
    }

    public void Load()
    {
        throw new System.NotImplementedException();
    }
}
