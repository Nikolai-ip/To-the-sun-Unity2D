public interface ITriggerable
{
    ActivableEntity TriggerEntity { get; set; }
    void Trigger(bool state);
}
