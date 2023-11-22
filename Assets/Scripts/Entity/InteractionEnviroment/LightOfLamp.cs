using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class LightOfLamp : MonoBehaviour
{    
    [SerializeField] private float _turnOnTime;
    [SerializeField] private float _turnOffTime;
    [SerializeField] private Light2D _light;
    [SerializeField] private float _intensity;
    [SerializeField] private AnimationCurve _idleFlickerCurve;
    [SerializeField] private float _flickerSpeed;
    private bool _isFlicker;
    private void OnValidate()
    {
        _light.intensity = _intensity;
    }

    private void Start()
    {
        _isFlicker = _light.intensity > 0;
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        float elapsedTime = 0;
        while (true)
        {
            if (_isFlicker)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / _flickerSpeed;
                _light.intensity = _intensity * _idleFlickerCurve.Evaluate(percentageComplete);
                if (percentageComplete >= 1)
                {
                    elapsedTime = 0;
                }

            }
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HideController player)) player.NumberOfLampsAbove++;
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HideController player)) player.NumberOfLampsAbove--;
    }
    public void TurnOn()
    {
        _isFlicker = false;
        StartCoroutine(SmoothLightTransition(_intensity, _turnOnTime));
    }

    public void TurnOff()
    {
        _isFlicker = false;
        StartCoroutine(SmoothLightTransition(0, _turnOffTime));
    }
    
    private IEnumerator SmoothLightTransition(float targetIntensity, float time)
    {
        float elapsedTime = 0;
        var tick = new WaitForFixedUpdate();
        float originIntensity = _light.intensity;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / time;
            _light.intensity = Mathf.Lerp(originIntensity, targetIntensity, percentageComplete);
            yield return tick;
        }
        if (targetIntensity != 0)
            _isFlicker = true;
    }
}