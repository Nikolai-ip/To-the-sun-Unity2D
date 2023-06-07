using System;
using UnityEngine;

public abstract class FunctionalItem : PeacefulItem, IStateChangeNotifier, IInteractivable
{
    public event Action<bool> StateChanged;

    public bool IsCanInteract { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter2D(collision);

        if (collision.TryGetComponent(out Door door))
        {
            IsCanInteract = true;

            var pickup = gameObject.GetComponentInParent<PickUpItem>();

            if (pickup.CurrentItem == this)
            {
                var enviroment = gameObject.GetComponentInParent<InteractionEnviromentController>();
                enviroment.InteractiveEntity = this;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerExit2D(collision);

        if (collision.TryGetComponent(out Door door))
        {
            IsCanInteract = false;

            var pickup = gameObject.GetComponentInParent<PickUpItem>();

            if (pickup.CurrentItem == this)
            {
                var enviroment = gameObject.GetComponentInParent<InteractionEnviromentController>();
                enviroment.InteractiveEntity = null;
            }
        }
    }

    public void Interact()
    {
        if (!IsCanInteract)
        {
            return;
        }

        OnStateChange(true);
    }

    public void OnStateChange(bool args)
    {
        StateChanged?.Invoke(args);
    }
}
