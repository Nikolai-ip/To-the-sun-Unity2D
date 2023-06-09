using UnityEngine;

public abstract class Item : Entity
{
    [SerializeField] private string _uiTextInteraction;

    protected bool _isOnHand = false;

    public override string UITextInteraction => _uiTextInteraction;

    protected void TriggerEnter2D(Collider2D collision)
    {
        var hand = collision.GetComponentInChildren<PickUpItem>();

        if (hand == null)
            return;

        if (hand.TryGetComponent(out PickUpItem pickUpItem))
        {
            pickUpItem.NearItem = this;
        }
    }

    protected void TriggerExit2D(Collider2D collision)
    {
        var hand = collision.GetComponentInChildren<PickUpItem>();

        if (hand == null)
            return;

        if (hand.TryGetComponent(out PickUpItem pickUpItem))
        {
            pickUpItem.NearItem = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerExit2D(collision);
    }
}
