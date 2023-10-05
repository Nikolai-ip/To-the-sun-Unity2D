using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.PlayerStates
{
    [CreateAssetMenu(fileName = "Idle", menuName = "ScriptableObjects/States/IdleState", order = 1)]
    public class IdleState : PlayerState
    {
        
        public override void HandleInput(ref InputAction inputAction)
        {            
            if (inputAction == null) return;
            if (inputAction.name == "Jump")
            {
                if (sm.CheckPlayerOnGround())
                {
                    sm.ChangeState(sm.Jump,this);
                }
            }
            if (inputAction.name == "MoveXAxis")
            {
                var x = inputAction.ReadValue<Vector2>().x;
                if (!Mathf.Approximately(x, 0)) sm.ChangeState(sm.Move, this);
            }

        }      
        public override void Update()
        {
            if (rb.velocity.y < -2.5f) sm.ChangeState(sm.Fall, this);
        }

    }
}