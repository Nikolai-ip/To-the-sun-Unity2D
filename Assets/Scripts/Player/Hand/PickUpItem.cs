using UnityEngine;

public class PickUpItem : UINotifier
{
    private Item _currentItem;
    private Rigidbody2D _currentItemRB;
    private Item _nearItem;
    private bool _isItemInHand = false;
    private ItemsThrower _thrower;

    public Item CurrentItem => _currentItem;

    public bool IsItemInHand
    {
        get => _isItemInHand;
        set
        {
            _isItemInHand = value;

            if (_currentItem != null)
            {
                OnStateChanged(_currentItem.UITextInteraction);
            }
        }
    }

    public Item NearItem
    {
        get => _nearItem;
        set
        {
            _nearItem = value;

            var UIText = _nearItem is null ? string.Empty : _nearItem.UITextInteraction;

            OnEntityCanChanged(UIText);
        }

    }

    private void Start()
    {
        _thrower = GetComponent<ItemsThrower>();
    }

    public void PickUpOrDrop()
    {
        if (_isItemInHand)
        {
            Drop();
        }

        if (NearItem is null)
        {
            return;
        }

        PickUp();
    }

    public void PickUp()
    {
        if (NearItem is Key)
        {
            var key = NearItem as Key;

            key.OnStateChange(true);

            var playerInteractionController = GetComponentInParent<InteractionEnviromentController>();
            playerInteractionController.InteractiveEntity = null;

            Destroy(NearItem);
            NearItem = null;
            return;
        }

        _currentItem = NearItem;
        _currentItem.transform.parent = transform;
        _currentItem.transform.localPosition = Vector2.zero;
        _currentItemRB = _currentItem.GetComponent<Rigidbody2D>();
        _currentItemRB.bodyType = RigidbodyType2D.Kinematic;

        IsItemInHand = true;
    }

    public void Throw()
    {
        if (!IsItemInHand)
        {
            return;
        }

        var droppedItem = Drop();

        _thrower.Throw(droppedItem);
    }

    public void CalculateTrajectory()
    {
        if (!IsItemInHand)
        {
            return;
        }

        _thrower.CalculateTrajectory();
    }

    public Item Drop()
    {
        var droppedItem = _currentItem;

        _currentItem.transform.parent = null;
        _currentItem = null;
        _currentItemRB.bodyType = RigidbodyType2D.Dynamic;
        _currentItemRB = null;
        IsItemInHand = false;

        return droppedItem;
    }
}
