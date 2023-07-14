using UnityEngine;

public class KeyDoor : Door, IInteractivable
{
    private bool _isLocked = true;
    private bool _isOpen = false;

    public override string UITextInteraction
    {
        get
        {
            if (_isLocked)
            {
                return "Door is locked. Find the key";
            }

            if (_isOpen)
            {
                return "Close the door";
            }
            else
            {
                return "Open the door";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
        {
            playerInteractionController.InteractiveEntity = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
        {
            playerInteractionController.InteractiveEntity = null;
        }
    }

    public void Interact()
    {
        if (_isLocked) 
            return;

        if (_isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public override void Trigger(bool state)
    {
        _isLocked = !_isLocked;
    }
}
