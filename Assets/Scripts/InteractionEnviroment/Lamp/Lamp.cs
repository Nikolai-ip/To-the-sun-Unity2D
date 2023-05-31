using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private Animator _animator;
    public bool IsActive { get; private set; }
    private void Start()
    { 
        _animator = GetComponent<Animator>();
        IsActive = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
        {
            playerInteractionController.Lamp = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
        {
            playerInteractionController.Lamp = null;
        }
    }
    public void TurnOn()
    {
        _animator.SetTrigger("TurnOn");
        IsActive = true;
    }
    public void TurnOff()
    {
        _animator.SetTrigger("TurnOff");
        IsActive = false;
    }
}
