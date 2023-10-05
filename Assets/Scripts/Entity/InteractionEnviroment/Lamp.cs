using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : ActivableEntity
{
    private static readonly int On = Animator.StringToHash("TurnOn");
    private static readonly int Off = Animator.StringToHash("TurnOff");
    [SerializeField] private Collider2D _collider;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        IsActive = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Stone stone))
        {
            enabled = false;
            var lights = gameObject.GetComponentsInChildren<Light2D>();
            _collider.enabled = false;
            foreach (var light in lights) light.enabled = false;
            OnStateChanged(false);
        }
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
}