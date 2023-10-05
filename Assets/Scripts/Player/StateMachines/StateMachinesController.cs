using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Player.PlayerStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.StateMachines
{
    public class StateMachinesController:MonoBehaviour
    {
        [SerializeField]private List<StateMachine> _stateMachines;

        private void Start()
        {
            _stateMachines = new List<StateMachine>
            {
                GetComponent<HandStateMachine>(),
                GetComponent<PlayerStateMachine>()
            };
        }

        public void InputToStateMachine(InputAction inputAction)
        {
            foreach (var stateMachine in _stateMachines)
            {
                stateMachine.CurrentState.HandleInput(ref inputAction);
            }
        }
        
        public void SetFirstOrderOfExecution(StateMachine stateMachine)
        {
            int index = _stateMachines.FindIndex(x => x == stateMachine);
            if (index != 0)
            {
                (_stateMachines[0], _stateMachines[1]) = (_stateMachines[1], _stateMachines[0]);
            }
        }
    }
}