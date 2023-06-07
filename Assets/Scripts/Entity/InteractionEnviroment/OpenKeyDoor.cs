using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyDoor : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            var pickup = player.gameObject.GetComponentInChildren<PickUpItem>();

            if (pickup.CurrentItem == _door.TriggerEntity)
            {
                var key = pickup.CurrentItem as Key;

                if (key != null)
                {
                    key.IsCanInteract = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            var pickup = player.gameObject.GetComponentInChildren<PickUpItem>();

            if (pickup.CurrentItem == _door.TriggerEntity)
            {
                var key = pickup.CurrentItem as Key;

                if (key != null)
                {
                    key.IsCanInteract = false;
                }
            }
        }
    }
}
