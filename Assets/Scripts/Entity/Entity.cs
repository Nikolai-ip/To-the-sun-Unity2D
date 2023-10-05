using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] public string TriggerAnimation;

    public abstract string UITextInteraction { get; }
}