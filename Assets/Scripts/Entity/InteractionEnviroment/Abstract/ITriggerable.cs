using System;
using UnityEngine;

public interface ITriggerable
{
    IStateChangeNotifier TriggerEntity { get; set; }
    void Trigger(bool state);
}
