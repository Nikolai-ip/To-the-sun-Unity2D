using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGrabbing : MonoBehaviour
{
    [SerializeField] private float _grabingSpeed;

    public bool IsOnLadder = false;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Ladder"))
        {
            IsOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Ladder"))
        {
            IsOnLadder = false;
        }
    }

    public void ChangeRigidBodyType()
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

        _rigidBody.velocity = new Vector2(0, vect.y * _grabingSpeed);
    }
}
