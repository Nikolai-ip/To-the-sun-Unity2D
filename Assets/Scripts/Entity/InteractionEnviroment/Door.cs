using UnityEngine;

public class Door : MonoBehaviour, ITriggerable
{
    [SerializeField] private ActivableEntity _triggerEntity;

    public ActivableEntity TriggerEntity { get => _triggerEntity; set => _triggerEntity = value; }

    private void Start()
    {
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
        gameObject.SetActive(false);
    }

    private void Open()
    {
        gameObject.SetActive(true);
    }
}
