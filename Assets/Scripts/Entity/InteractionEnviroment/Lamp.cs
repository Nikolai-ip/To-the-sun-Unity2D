using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Stone>(out Stone stone))
        {
            this.enabled = false;
            var lights = gameObject.GetComponentsInChildren<Light2D>();
            GetComponent<BoxCollider2D>().enabled = false;
            foreach (var light in lights)
            {
                light.enabled = false;
            }
            OnStateChanged(false);
        }
    }
}
