using System;
using Player;
using UnityEngine;

public class Checkpoint : MonoBehaviour, ILoadable
{
    [SerializeField] private bool _isReached;

    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private BoxCollider2D _collider;

    public bool IsReached
    {
        get => _isReached;
        set
        {
            if (value)
            {
                _isReached = true;
                CheckpointReached?.Invoke(this);
                _collider.enabled = false;
                return;
            }

            _isReached = false;
            _collider.enabled = true;
        }
    }

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, _collider.size);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerActor player)) IsReached = true;
    }

    public void Load()
    {
        throw new NotImplementedException();
    }

    public static event Action<Checkpoint> CheckpointReached;
}