public interface ITriggerable
{
    IStateChangeNotifier TriggerEntity { get; set; }
    void Trigger(bool state);
}