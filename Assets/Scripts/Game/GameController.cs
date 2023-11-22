using System;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour, ILoadable
{
    [FormerlySerializedAs("_player")] [SerializeField] private PlayerActor playerActor;
    private Vector2 _lastCheckpointPosition;
    
    private void Start()
    {
        playerActor.Died += RespawnPlayerActor;
        Checkpoint.CheckpointReached += UpdateCheckpoint;
        SetFrameRate(60);
    }

    public void Load()
    {
        throw new NotImplementedException();
    }

    private void UpdateCheckpoint(Checkpoint newCheckpoint)
    {
        var position = newCheckpoint.GetComponent<Transform>().position;
        _lastCheckpointPosition = position;
    }

    public void RespawnPlayerActor()
    {
        var transform = playerActor.GetComponent<Transform>();
        transform.position = _lastCheckpointPosition;
    }

    public void SetFrameRate(int frameRate)
    {
        Application.targetFrameRate = frameRate;
    }
}