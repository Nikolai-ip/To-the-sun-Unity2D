using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace AISystem
{
    public class StateMachine : MonoBehaviour
    {
        public Transform Tr { get; private set; }   
        public Rigidbody2D Rb { get; private set; }
        public Player Player { get; private set; }
        private BaseState _currentState;
        public Enemy Enemy { get; private set; }
        public Animator Animator { get; private set; }
        public Patrolling PatrollingState { get; private set; }
        private void Start()
        {
            Enemy = GetComponent<Enemy>();  
            Tr = GetComponent<Transform>(); 
            Rb = GetComponent<Rigidbody2D>();
            Player = FindObjectOfType<Player>();
            Animator = GetComponent<Animator>();
            PatrollingState = new Patrolling(this);
            ChangeState(PatrollingState);
        }
        public void ChangeState(BaseState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
        private void Update()
        {
            _currentState?.Update();
        }
        private void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
        public void AnimationEventHandler()
        {
            _currentState?.AnimationEventHandler();
        }

    }
}

