using System;
using Player;
using UnityEngine;

public abstract class ActivableEntity : Entity, IInteractivable, IStateChangeNotifier
{
    [SerializeField] private string _UITextEnabled;
    [SerializeField] private string _UITextDisabled;
    private bool _isActive;

    public bool IsActive
    {
        get => _isActive;
        protected set
        {
            _isActive = value;
            StateChanged?.Invoke(_isActive);
        }
    }

    public override string UITextInteraction => IsActive ? _UITextDisabled : _UITextEnabled;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled) return;

        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
            playerInteractionController.InteractiveEntity = this;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
            playerInteractionController.InteractiveEntity = null;
    }

    public void Interact()
    {
        if (IsActive)
            TurnOff();
        else
            TurnOn();
    }

    public event Action<bool> StateChanged;

    protected abstract void TurnOn();
    protected abstract void TurnOff();

    protected void OnStateChanged(bool state)
    {
        StateChanged?.Invoke(state);
    }
}