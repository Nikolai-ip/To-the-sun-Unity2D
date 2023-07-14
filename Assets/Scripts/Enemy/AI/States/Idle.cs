using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class Idle : BaseState
    {
        public override void Enter() { }
        public override void Exit() { }
        public override void CheckTransaction()
        {

        }

        public Idle(StateMachine sm):base(sm) { }
    }
}