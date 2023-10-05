using System;
using Player.PlayerStates;
using UnityEngine;

namespace Player.StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {       
        public State CurrentState { get; protected set; }
        [SerializeField] protected string currentStateName;

        public StateMachinesController StateMachinesController { get; private set; }

        protected virtual void Start()
        {
            StateMachinesController = GetComponent<StateMachinesController>();
        }

        protected void Update()
        {
            if (CurrentState)
                CurrentState.Update();
        }

        protected void FixedUpdate()
        {
            if (CurrentState)
                CurrentState.FixedUpdate();
        }
        public void ChangeState(State state, object sender)
        {
            if (sender is not State) return;
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
            currentStateName = CurrentState.GetType().Name;
        }
        
        protected void OnAnimationEvent()
        {           
            if (CurrentState)
                CurrentState.AnimationEventHandle();
        }
    }
}