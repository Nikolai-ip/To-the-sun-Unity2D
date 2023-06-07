using UnityEngine;

public class PickUpItem : UINotifier
{
    private Item _currentItem;
    private Item _nearItem;
    private bool _isItemInHand = false;

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
        _currentItem = NearItem;
        _currentItem.transform.parent = transform;
        _currentItem.transform.localPosition = Vector2.zero;

        IsItemInHand = true;
    }

    public void Throw()
    {
        if (!IsItemInHand)
        {
            return;
        }

        // curve line
    }

    public void Drop()
    {
        _currentItem.transform.parent = null;
        _currentItem = null;

        IsItemInHand = false;
    }
}
