using UnityEngine;
using System;

public class Player : MonoBehaviour, IDieable
{
    public event Action Died;
    public event Action DieIsOver;
    public void Die()
    {
        Died?.Invoke();
    }
    public void OnDieEnd()
    {
        DieIsOver?.Invoke();    
    }
}
