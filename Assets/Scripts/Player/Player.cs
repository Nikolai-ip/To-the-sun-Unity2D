using UnityEngine;
using System;

public class Player : MonoBehaviour, IDieable
{
    public event Action Died;

    public void Die()
    {
        Died?.Invoke();
    }

    public void FixedUpdate()
    {
        if (GetComponent<Transform>().position.y < -8)
        {
            Die();
        }
    }
}
