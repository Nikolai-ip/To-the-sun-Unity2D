using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.HandStates
{
    [CreateAssetMenu(fileName = "UnfreeHand", menuName = "ScriptableObjects/HandStates/UnfreeHandState")]
    public class UnfreeHandState:HandState
    {
        public override void HandleInput(ref InputAction inputAction)
        {
            if (inputAction == null) return;
            if (inputAction.name == "PickUpItem")
            {
                if (sm.PickUpItem.TryDropCurrentItem())
                {
                    sm.ChangeState(sm.FreeHand, this);
                }
            }

            if (inputAction.name == "ThrowItem" && sm.PickUpItem.CurrentItem)
            {
                sm.ChangeState(sm.Throw, this);
            }
        }
    }
}