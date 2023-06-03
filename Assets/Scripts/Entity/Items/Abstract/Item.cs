using UnityEngine;

public abstract class Item : Entity
{
    [SerializeField] private string _uiTextInteraction;

    public override string UITextInteraction => _uiTextInteraction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hand = collision.GetComponentInChildren<PickUpItem>();

        if (hand == null)
            return;

        if (hand.TryGetComponent(out PickUpItem pickUpItem))
        {
            pickUpItem.NearItem = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var hand = collision.GetComponentInChildren<PickUpItem>();

        if (hand == null)
            return;

        if (hand.TryGetComponent(out PickUpItem pickUpItem))
        {
            pickUpItem.NearItem = null;
        }
    }
}
