using System;
using UnityEngine;

public abstract class ActivableEntity : Entity, IInteractivable, IStateChangeNotifier
{
    private bool _isActive = false;
    [SerializeField] private string _UITextEnabled;
    [SerializeField] private string _UITextDisabled;

    public event Action<bool> StateChanged;
    public bool IsActive 
    { 
        get => _isActive; 
        protected set
        {
            _isActive = value;
            StateChanged?.Invoke(_isActive);
        }
    }
    public override string UITextInteraction
    {
        get => IsActive ? _UITextDisabled : _UITextEnabled;
    }

    protected abstract void TurnOn();
    protected abstract void TurnOff();

    public void Interact()
    {
        if (IsActive)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    public void OnStateChanged(bool state)
    {
        StateChanged?.Invoke(state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
        {
            playerInteractionController.InteractiveEntity = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
        {
            playerInteractionController.InteractiveEntity = null;
        }
    }
}