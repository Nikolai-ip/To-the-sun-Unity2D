using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.HandStates
{
    [CreateAssetMenu(fileName = "FreeHand", menuName = "ScriptableObjects/HandStates/FreeHandState")]
    public class FreeHandState: HandState
    {
        public override void HandleInput(ref InputAction inputAction)
        {
            if (inputAction == null) return;
            if (inputAction.name == "PickUpItem")
            { 
                if (sm.PickUpItem.TryPickUp())
                {
                    animator.SetTrigger("Take");
                    sm.ChangeState(sm.UnfreeHand, this);
                }
            }
        }
    }
}