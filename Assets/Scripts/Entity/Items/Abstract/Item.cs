using UnityEngine;

public abstract class Item : Entity
{
    [SerializeField] private string _uiTextInteraction;
    [SerializeField] private string _uiTextChangedState;

    protected bool _isOnHand = false;
    public Transform Tr { get; protected set; }
    public SpriteRenderer SpriteRenderer { get; protected set; }
    public Rigidbody2D Rb { get; protected set; }

    public override string UITextInteraction => _uiTextInteraction;
    public string UITextStateChanged => _uiTextChangedState;

    public virtual void Start()
    {
        Tr = GetComponent<Transform>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerExit2D(collision);
    }

    protected void TriggerEnter2D(Collider2D collision)
    {
        var hand = collision.GetComponentInChildren<PickUpItem>();
        if (hand == null)
            return;

        if (hand.TryGetComponent(out PickUpItem pickUpItem)) pickUpItem.NearItem = this;
    }

    protected void TriggerExit2D(Collider2D collision)
    {
        var hand = collision.GetComponentInChildren<PickUpItem>();

        if (hand == null)
            return;

        if (hand.TryGetComponent(out PickUpItem pickUpItem)) pickUpItem.NearItem = null;
    }
}