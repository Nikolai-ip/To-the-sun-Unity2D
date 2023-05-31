using System;
using UnityEngine;

public class InteractionEnviromentController : MonoBehaviour
{
    public event Action<InteractiveEntity> OnCanLampChanged;
    public event Action<string> OnLampStateChanged;
    private InteractiveEntity _nearGameObject;
    private Animator _animator;
    public InteractiveEntity NearGameObject {
        get => _nearGameObject;
        set
        {
            _nearGameObject = value;
            OnCanLampChanged?.Invoke(_nearGameObject);
        }
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void LampInteraction()
    {
        if (NearGameObject is null) 
            return;

        _animator.SetTrigger(NearGameObject.TriggerAnimation);
        NearGameObject.Interact();

        var UIText = (NearGameObject as ActivableEntity).UIText;
        OnLampStateChanged?.Invoke(UIText);
    }
}
