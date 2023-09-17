using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : ActivableEntity
{
    private Animator _animator;
    [SerializeField] private Collider2D _collider;
    private static readonly int On = Animator.StringToHash("TurnOn");
    private static readonly int Off = Animator.StringToHash("TurnOff");

    private void Start()
    { 
        _animator = GetComponent<Animator>();
        IsActive = true;
    }

    protected override void TurnOn()
    {
        _animator.SetTrigger(On);
        IsActive = true;
    }

    protected override void TurnOff()
    {
        _animator.SetTrigger(Off);
        IsActive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Stone>(out Stone stone))
        {
            this.enabled = false;
            var lights = gameObject.GetComponentsInChildren<Light2D>();
            _collider.enabled = false;
            foreach (var light in lights)
            {
                light.enabled = false;
            }
            OnStateChanged(false);
        }
    }
}
