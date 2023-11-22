using Player.HandStates;
using UnityEngine;

namespace Player.StateMachines
{
    public class HandStateMachine:StateMachine
    {
        private string _currentStateName;
        public PickUpItem PickUpItem { get; private set; }
        public ItemsThrower ItemsThrower { get; private set; }
        [field:SerializeField] public HandState FreeHand { get; private set; }
        [field:SerializeField] public HandState UnfreeHand { get; private set; }
        [field:SerializeField] public HandState Throw { get; private set; }
        [field:SerializeField] public NullState Null { get; private set; }

        private void Awake()
        {            
            
            FreeHand.Initialize(this);
            UnfreeHand.Initialize(this);
            Throw.Initialize(this);
            Null.Initialize(this);
            CurrentState = FreeHand;
            
        }
        

        protected override void Start()
        {
            base.Start();
            PickUpItem = GetComponentInChildren<PickUpItem>();
            ItemsThrower = GetComponentInChildren<ItemsThrower>();
        }
    }
}