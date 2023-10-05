using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.HandStates
{
    [CreateAssetMenu(fileName = "Throw", menuName = "ScriptableObjects/HandStates/ThrowState")]
    public class ThrowState:HandState
    {
        
        public override void HandleInput(ref InputAction inputAction)
        {
            if (inputAction == null) return;
            if (inputAction.name == "ThrowItem")
            {            
                sm.ItemsThrower.Throw(sm.PickUpItem.Drop());
                sm.ChangeState(sm.FreeHand, this);

            }

            inputAction = null;

        }
        public override void FixedUpdate()
        {
            sm.ItemsThrower.CalculateTrajectory();
        }

        public override void Enter()
        {
            animator.SetTrigger("TossStart");
            sm.ItemsThrower.ToogleLineRenderer(true);
            sm.StateMachinesController.SetFirstOrderOfExecution(sm);

        }

        public override void Exit()
        {                
            animator.SetTrigger("TossEnd");
            sm.ItemsThrower.ToogleLineRenderer(false);
        }
    }
}