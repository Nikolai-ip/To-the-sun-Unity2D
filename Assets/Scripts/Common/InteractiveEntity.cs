using UnityEngine;

public abstract class InteractiveEntity : MonoBehaviour
{
    [SerializeField] protected string _UITextEnabled;
    [SerializeField] protected string _UITextDisabled;
    [SerializeField] public string TriggerAnimation;

    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
        {
            playerInteractionController.NearGameObject = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out InteractionEnviromentController playerInteractionController))
        {
            playerInteractionController.NearGameObject = null;
        }
    }
}
