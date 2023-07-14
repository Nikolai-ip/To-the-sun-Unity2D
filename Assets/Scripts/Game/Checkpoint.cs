using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour, ILoadable
{
    [SerializeField] private bool _isReached = false;

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
    public static event Action <Checkpoint> CheckpointReached;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            IsReached = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, _collider.size);
    }

    public void Load()
    {
        throw new NotImplementedException();
    }
}
