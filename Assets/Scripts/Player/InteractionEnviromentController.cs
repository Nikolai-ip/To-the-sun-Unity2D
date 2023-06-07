using UnityEngine;

public class InteractionEnviromentController : UINotifier
{
    private Entity _interactiveEntity;
    private Animator _animator;

    public Entity InteractiveEntity {
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

        var interactive = InteractiveEntity as IInteractivable;
        interactive.Interact();

        OnStateChanged(InteractiveEntity.UITextInteraction);
    }
}
