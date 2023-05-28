using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMoveController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _accelerate;
    private float _originalGravityScale;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    [Space]
    [Header("Jump")]
    [SerializeField] private float _gravityFallScale = 1;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;
    private bool _isWallSliding = false;
    [SerializeField] private float _wallSlidingSpeed;
    [SerializeField] private Transform _wallCheck;
    [Range(0f,0.2f)]
    [SerializeField] private float _slideRange;
    [SerializeField] private float _jumpOffWallForceY;
    [SerializeField] private float _jumpOffWallForceX;
    [SerializeField] private float _canMoveTurnOffDuration;
    private Transform _tr;
    private bool _canMove = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _tr = GetComponent<Transform>();
        _originalGravityScale = _rb.gravityScale;
    }
    public void Move(Vector2 moveInput)
    {
        WallSlide(moveInput.x);
        if (_canMove)
        {
            if (moveInput.x == 0)
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
                return;
            }
            float targetMove = _speed * Mathf.Abs(moveInput.x);
            float dif = targetMove - Mathf.Abs(_rb.velocity.x);
            float accelRate = dif > 0.1f ? _accelerate : 0;
            float movement = Mathf.Abs(_rb.velocity.x) + accelRate;
            _rb.velocity = new Vector2(movement * Mathf.Sign(moveInput.x), _rb.velocity.y);
            Flip(moveInput.x);
        }


    }
    private void Flip(float inputHorizontal)
    {
        _tr.localScale = new Vector2(Mathf.Sign(inputHorizontal), 1);
    }
    private void WallSlide(float inputHorizontal)
    {
        if (IsWalled() && !IsGrounded())
        {
            _isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -_wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            _isWallSliding = false;
        }
    }
    private bool IsWalled()
    {

        return Physics2D.OverlapCircle(_wallCheck.position, _slideRange, _wallLayer);
    }
    public void Jump()
    {
        if (_isWallSliding)
            JumpOffWall();
        else if (IsGrounded())
            JumpFromGround();
        
    }
    private void JumpFromGround()
    {
        _rb.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }
    private void JumpOffWall()
    {
        _canMove = false;
        _rb.AddForce(new Vector2(-Mathf.Sign(_tr.localScale.x) * _jumpOffWallForceX, _jumpOffWallForceY), ForceMode2D.Impulse);  
        _tr.localScale = new Vector2(-_tr.localScale.x, _tr.localScale.y);
        Invoke(nameof(TurnOnCanMove), _canMoveTurnOffDuration);

    }
    private void TurnOnCanMove()
    {
        _canMove = true;
    }
    private void FixedUpdate()
    {
        SetFallGravityScale(_originalGravityScale * _gravityFallScale);
    }
    private void SetFallGravityScale(float gravityScale)
    {
        if (_rb.velocity.y < 0 && !IsWalled())
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
}
