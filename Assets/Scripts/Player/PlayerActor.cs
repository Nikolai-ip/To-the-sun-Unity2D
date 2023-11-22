using System;
using UnityEngine;

namespace Player
{
    public class PlayerActor : MonoBehaviour, IDieable
    {
        public void Die()
        {
            Died?.Invoke();
        }

        public event Action Died;
        public event Action DieIsOver;

        public void OnDieEnd()
        {
            DieIsOver?.Invoke();
        }
    }
}