using UnityEngine;
using System;

public class Player : MonoBehaviour, IDieable
{
    public event Action Died;

    public void Die()
    {
        Died?.Invoke();
    }
}
