using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{

    public abstract class BaseState:ScriptableObject
    {
        protected StateMachine stateMachine;
        public abstract void Enter();
        public abstract void Exit();
        public virtual void Update() 
        {
            TransactionCheck();

        }

        public virtual void FixedUpdate() { }
        public abstract void TransactionCheck();
        public virtual void OnDrawGizmos() { }

        public virtual void Init(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
    }

}
