using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public abstract class BaseState : MonoBehaviour
    {
        protected StateMachine stateMachine;
        public virtual void Enable()
        {
            enabled = true;
        }
        public virtual void Disable()
        {
            enabled = false;
        }
    }

}
