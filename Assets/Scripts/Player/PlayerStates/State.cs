using Player.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.PlayerStates
{
    public abstract class State : ScriptableObject
    {
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void HandleInput(ref InputAction inputAction)
        {
        }

        public virtual void HandleCollisionEnter(Collision2D other)
        {
            
        }
        
   
        
        public virtual void LedgeDetected(){}
        public virtual void LadderDetected(){}
        public virtual void AnimationEventHandle(){}
    }
}