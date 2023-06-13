using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyDoor : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            var pickup = player.gameObject.GetComponentInChildren<PickUpItem>();
            SetCanInteract(pickup, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            var pickup = player.gameObject.GetComponentInChildren<PickUpItem>();
            SetCanInteract(pickup, false);
        }
    }
    private void SetCanInteract(PickUpItem pickup, bool isCanInteract)
    {
        if (ReferenceEquals(pickup.CurrentItem, _door.TriggerEntity))
        {
            var key = pickup.CurrentItem as Key;

            if (key != null)
            {
                key.IsCanInteract = isCanInteract;
            }
        }
    }
}
