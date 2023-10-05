using Player;
using UnityEngine;

public class PickUpItem : UINotifier
{
    [SerializeField] private Transform _handTr;
    private bool _isItemInHand;
    private Item _nearItem;
    private Animator _playerAnimator;
    private ItemsThrower _thrower;
    public Item CurrentItem { get; private set; }

    public bool IsItemInHand
    {
        get => _isItemInHand;
        set
        {
            _isItemInHand = value;

            if (CurrentItem != null) OnStateChanged(CurrentItem.UITextInteraction);
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
        _playerAnimator = GetComponentInParent<Animator>();
    }

    public bool TryDropCurrentItem()
    {
        if (!_isItemInHand) return false;
        Drop();
        return true;
    }

    public bool TryPickUp()
    {
        if (NearItem is null) return false;
        CurrentItem = NearItem;
        CurrentItem.Tr.parent = _handTr;
        CurrentItem.Tr.localPosition = Vector2.zero;
        CurrentItem.SpriteRenderer.sortingLayerName = "PlayerLayer";
        CurrentItem.Rb.bodyType = RigidbodyType2D.Kinematic;
        IsItemInHand = true;
        
        return true;
    }
    
    public Item Drop()
    {
        var droppedItem = CurrentItem;
        CurrentItem.Rb.bodyType = RigidbodyType2D.Dynamic;
        CurrentItem.SpriteRenderer.sortingLayerName = "InteractionItem";
        CurrentItem.transform.parent = null;
        CurrentItem = null;

        IsItemInHand = false;

        _thrower.ToogleLineRenderer(false);

        return droppedItem;
    }
}