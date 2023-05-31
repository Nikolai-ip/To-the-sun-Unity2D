using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private GameObject _currentItem;
    private bool _isItemInHand = false;

    public void PickUp(GameObject item)
    {
        if (_isItemInHand)
        {
            Drop();
        }

        _currentItem = item;
        _currentItem.transform.parent = transform;
        _currentItem.transform.localPosition = Vector2.zero;

        _isItemInHand = true;
    }

    public void Throw(GameObject gameObject)
    {
        if (!_isItemInHand)
        {
            return;
        }

        // curve line
    }

    public void Drop()
    {
        _currentItem.transform.parent = null;
        _currentItem = null;

        _isItemInHand = false;
    }
}
