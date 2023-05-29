using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [SerializeField] private float _radius;
    private Transform _tr;
    private PlayerMoveController _player;
    [SerializeField] private LayerMask _ground;
    public bool CanDetected { get; set; }
    public Vector2 Position { get; private set; }
    private void Start()
    {
        _tr = GetComponent<Transform>();
        _player = GetComponentInParent<PlayerMoveController>();
    }
    private void Update()
    {
        Position = _tr.position;
        if (CanDetected)
        {
            _player.LedgeDetection = Physics2D.OverlapCircle(_tr.position, _radius, _ground);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            CanDetected = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            CanDetected = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
