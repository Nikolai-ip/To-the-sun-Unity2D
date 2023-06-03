using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionEnviromentController : UINotifier
{
    private InteractiveEntity _interactiveEntity;
    private Animator _animator;

    public InteractiveEntity InteractiveEntity {
        get => _interactiveEntity;
        set
        {
            _interactiveEntity = value;
            var UIText = InteractiveEntity is null ? string.Empty : InteractiveEntity.UITextInteraction;
            OnEntityCanChanged(UIText);
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void EntityInteraction()
    {
        if (InteractiveEntity is null) 
            return;

        _animator.SetTrigger(InteractiveEntity.TriggerAnimation);

        InteractiveEntity.Interact();

        OnStateChanged(InteractiveEntity.UITextInteraction);
    }
}
