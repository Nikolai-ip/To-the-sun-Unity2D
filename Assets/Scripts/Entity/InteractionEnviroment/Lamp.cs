using UnityEngine;

public class Lamp : ActivableEntity
{
    private Animator _animator;

    private void Start()
    { 
        _animator = GetComponent<Animator>();
        IsActive = true;
    }

    protected override void TurnOn()
    {
        _animator.SetTrigger("TurnOn");
        IsActive = true;
    }

    protected override void TurnOff()
    {
        _animator.SetTrigger("TurnOff");
        IsActive = false;
    }
}
