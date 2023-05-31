using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEnviromentController : MonoBehaviour
{
    public event Action<Lamp> OnCanLampChanged;
    public event Action<bool> OnLampStateChanged;
    private Lamp _lamp;
    private Animator _animator;
    public Lamp Lamp {
        get => _lamp;
        set
        {
            _lamp = value;
            OnCanLampChanged?.Invoke(_lamp);
        }
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void LampInteraction()
    {
        if (!Lamp) return;
        _animator.SetTrigger("Lamp");
        if (Lamp.IsActive)
        {
            Lamp.TurnOff();
            OnLampStateChanged?.Invoke(false);
        }
        else
        {
            Lamp.TurnOn();
            OnLampStateChanged?.Invoke(true);
        }
    }
}
