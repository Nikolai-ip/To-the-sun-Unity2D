using System;
using UnityEngine;

public abstract class Door : Entity, ITriggerable
{
    [SerializeField] private GameObject _triggerEntityGameObject;

    private IStateChangeNotifier _triggerEntity;

    public IStateChangeNotifier TriggerEntity { get => _triggerEntity; set => _triggerEntity = value; }
    
    private void Start()
    {
        _triggerEntity = _triggerEntityGameObject.GetComponent<IStateChangeNotifier>();

        if (_triggerEntity == null)
        {
            throw new NullReferenceException("Trigger entity is null");
        }

        TriggerEntity.StateChanged += Trigger;
    }

    protected void Close()
    {
        gameObject.SetActive(true);
    }

    protected void Open()
    {
        gameObject.SetActive(false);
    }

    public abstract void Trigger(bool state);
}
