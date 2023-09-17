using UnityEngine;

public class PickUpItem : UINotifier
{
    private Item _currentItem;
    private Item _nearItem;
    private bool _isItemInHand = false;
    private ItemsThrower _thrower;
    private Animator _playerAnimator;
    public Item CurrentItem => _currentItem;
    [SerializeField] private Transform _handTr;

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
        _playerAnimator = GetComponentInParent<Animator>();
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

            NearItem.gameObject.SetActive(false);
            NearItem = null;

            return;
        }
        _playerAnimator.SetTrigger("Take");
        _currentItem = NearItem;
        _currentItem.Tr.parent = _handTr;
        _currentItem.Tr.localPosition = Vector2.zero;
        _currentItem.SpriteRenderer.sortingLayerName = "PlayerLayer";
        _currentItem.Rb.bodyType = RigidbodyType2D.Kinematic;

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
        _currentItem.Rb.bodyType = RigidbodyType2D.Dynamic;
        _currentItem.SpriteRenderer.sortingLayerName = "InteractionItem";
        _currentItem.transform.parent = null;
        _currentItem = null;
        
        IsItemInHand = false;

        _thrower.ToogleLineRenderer(false);

        return droppedItem;
    }
}
