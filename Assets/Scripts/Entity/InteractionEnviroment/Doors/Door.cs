using System;
using UnityEngine;

public abstract class Door : Entity, ITriggerable
{
    [SerializeField] private GameObject _triggerEntityGameObject;

    private void Start()
    {
        TriggerEntity = _triggerEntityGameObject.GetComponent<IStateChangeNotifier>();

        if (TriggerEntity == null) throw new NullReferenceException("Trigger entity is null");

        TriggerEntity.StateChanged += Trigger;
    }

    public IStateChangeNotifier TriggerEntity { get; set; }

    public abstract void Trigger(bool state);

    protected void Close()
    {
        gameObject.SetActive(true);
    }

    protected void Open()
    {
        gameObject.SetActive(false);
    }
}