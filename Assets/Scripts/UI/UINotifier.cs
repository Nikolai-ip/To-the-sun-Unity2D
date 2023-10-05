using System;
using UnityEngine;

public class UINotifier : MonoBehaviour
{
    [SerializeField] public string KeyForInteraction;

    public event Action<string> StateChanged;
    public event Action<string> EntityCanChanged;

    protected virtual void OnStateChanged(string args) // EventArgs
    {
        StateChanged?.Invoke(args);
    }

    protected virtual void OnEntityCanChanged(string args) // EventArgs
    {
        EntityCanChanged?.Invoke(args);
    }
}