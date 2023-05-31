public abstract class ActivableEntity : InteractiveEntity
{
    public bool IsActive { get; protected set; }

    public string UIText
    {
        get
        {
            if (IsActive)
            {
                return _UITextEnabled;
            }

            return _UITextDisabled;
        }
    }


    protected abstract void TurnOn();
    protected abstract void TurnOff();

    public override void Interact()
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
}