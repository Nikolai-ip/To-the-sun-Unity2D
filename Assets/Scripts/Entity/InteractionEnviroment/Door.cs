using System;
using UnityEngine;

public class Door : MonoBehaviour, ITriggerable
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

    public void Trigger(bool state)
    {
        if (state)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    private void Close()
    {
        gameObject.SetActive(true);
    }

    private void Open()
    {
        gameObject.SetActive(false);
    }
}
