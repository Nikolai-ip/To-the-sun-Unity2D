using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] public string TriggerAnimation;

    abstract public string UITextInteraction { get; }
}
