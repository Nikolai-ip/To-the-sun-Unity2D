using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private float _maxSpeed = 5.0f;
    [SerializeField] private float _speed;
    private Vector2 currentVelocity = Vector2.zero; 

    private float _originalGravityScale;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    [Space]
    [Header("Jump")]
    [SerializeField] private float _gravityFallScale = 1;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    private Transform _tr;
    private bool _canMove = true;
    private Animator _animator;
    private bool _isJumping = false;

    [Header("Ledge info")]
    [SerializeField] private Vector2 offset1;
    [SerializeField] private Vector2 offset2;
    [SerializeField] private float _moveToClimbDuration;

    private Vector2 climbBeginPos;
    private Vector2 climbEndPos;
    private bool canGrabLedge = true;
    private bool canClimb;
    private LedgeDetection _ledgeDetection;
    public bool LedgeDetection { get; set; }
    [Header("Jump Animation")]
    [SerializeField] private float _distanceToEndJumpAnimation;
    [SerializeField] private float _dy;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _tr = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _ledgeDetection = GetComponentInChildren<LedgeDetection>();
        _originalGravityScale = _rb.gravityScale;
    }
    public void Move(Vector2 moveInput)
    {
        if (_canMove)
        {
            Vector2 targetPosition = transform.position + new Vector3(moveInput.x * _speed, 0);
            Vector2 newPosition = Vector2.SmoothDamp(_rb.position, targetPosition, ref currentVelocity, _smoothTime, _maxSpeed);
            _rb.velocity = new Vector2(currentVelocity.x, _rb.velocity.y);
            Flip(moveInput.x);

            if (!_isJumping && !Mathf.Approximately(moveInput.x, 0))
            {
                _animator.SetBool("IsWalk", true);
            }
            else
            {
                _animator.SetBool("IsWalk", false);
            }
        }
         

    }
    private void Flip(float inputHorizontal)
    {
        if (!Mathf.Approximately(inputHorizontal, 0))
        {
            _tr.localScale = new Vector2(Mathf.Sign(inputHorizontal), 1);
        }

    }

    public void Jump()
    {
         if (IsGrounded())
            JumpFromGround();
    }
    private void JumpFromGround()
    {
        _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        _animator.SetTrigger("Jump");
        _animator.SetBool("IsJumping", true);
        _isJumping = true;
    }

    private void FixedUpdate()
    {
        SetFallGravityScale(_originalGravityScale * _gravityFallScale);
        if (_isJumping && _rb.velocity.y < 0)
        {
            var ground = Physics2D.OverlapCircle(new Vector2(_tr.position.x, _tr.position.y - _dy), _distanceToEndJumpAnimation, _groundLayer);
            if (ground != null)
            {
                _animator.SetBool("IsJumping", false);
                _isJumping = false;
            }
        }
    }

    private void SetFallGravityScale(float gravityScale)
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = gravityScale;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -_maxFallSpeed));
        }
        else
        {
            _rb.gravityScale = _originalGravityScale;
        }
    }
    private bool IsGrounded()
    {
        Vector2 topLeftPoint = _tr.position;
        topLeftPoint.x -= _collider.bounds.extents.x;
        topLeftPoint.y += _collider.bounds.extents.y;
        Vector2 bottomRightPoint = _tr.position;
        bottomRightPoint.x += _collider.bounds.extents.x;
        bottomRightPoint.y -= _collider.bounds.extents.y;
        return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, _groundLayer);
    }
    private void Update()
    {
        CheckForLedge();
    }
    private bool canSmoothMoveToClimb = true;
    private void CheckForLedge()
    {
        if (LedgeDetection && canGrabLedge)
        {
            canGrabLedge = false;
            Vector2 ledgePos = _ledgeDetection.Position;
            climbBeginPos = ledgePos + offset1;
            climbEndPos = ledgePos + offset2;
            canClimb = true;
            _animator.SetTrigger("Climb");
            _ledgeDetection.CanDetected = false;
            LedgeDetection = false;
            _collider.enabled = false;
        }
        if (canClimb)
        {
            if (canSmoothMoveToClimb)
                StartCoroutine(MoveObjectToPosition(_tr, climbBeginPos, _moveToClimbDuration));
            else
                _tr.position = climbBeginPos;
        }
    }
    IEnumerator MoveObjectToPosition(Transform objectToMove, Vector3 targetPosition, float duration)
    {
        canSmoothMoveToClimb = false;
        float timeElapsed = 0f;
        Vector3 startingPosition = objectToMove.position;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);
            objectToMove.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null;
        }
        objectToMove.transform.position = targetPosition;
       
    }
    private void LedgeClimbOver()
    {
        canClimb = false;
        _tr.position = climbEndPos;
        _ledgeDetection.CanDetected = true;
        canSmoothMoveToClimb = true;
        _collider.enabled = true;
        Invoke(nameof(AllowLedgeGrab), 0.1f);
    }
    private void AllowLedgeGrab()
    {
        canGrabLedge = true;
    }
}
