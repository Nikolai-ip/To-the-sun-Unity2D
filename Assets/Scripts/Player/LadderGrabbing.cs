using UnityEngine;

public class LadderGrabbing : MonoBehaviour
{
    [SerializeField] private float _grabingSpeed;
    [SerializeField] private Transform _ladderChecker;
    [SerializeField] private float _checkerRadius;
    [SerializeField] private LayerMask _ladderMask;

    private bool _isOnLadder = false;
    private Rigidbody2D _rigidBody;

    public bool IsOnLadder
    {
        get => _isOnLadder;
        private set => _isOnLadder = value;
    }

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        CheckingLadder();
        ChangeRigidBodyType();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_ladderChecker.position, _checkerRadius);
    }

    private void CheckingLadder()
    {
        IsOnLadder = Physics2D.OverlapPoint(_ladderChecker.position, _ladderMask);
    }

    private void ChangeRigidBodyType()
    {
        if (IsOnLadder)
        {
            _rigidBody.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void MoveUpDownOnLadder(Vector2 vect)
    {
        if (!IsOnLadder)
        {
            return;
        }

        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, vect.y * _grabingSpeed);
    }
}
