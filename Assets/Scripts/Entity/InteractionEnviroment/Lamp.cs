using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : ActivableEntity
{
    [SerializeField] private Collider2D _collider;
    private LightOfLamp[] _lightSources;
    private void Start()
    {
        IsActive = true;
        _lightSources = GetComponentsInChildren<LightOfLamp>();
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
        foreach (var lightOfLamp in _lightSources)
        {
            lightOfLamp.TurnOn();
        }
        IsActive = true;
    }

    protected override void TurnOff()
    {
        foreach (var lightOfLamp in _lightSources)
        {
            lightOfLamp.TurnOff();
        }
        IsActive = false;
    }

}