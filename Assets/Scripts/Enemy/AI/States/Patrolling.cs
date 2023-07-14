using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace AISystem
{
    public class Patrolling : BaseState
    {
        private bool _isRightMove = true;
        private bool _canMove = true;
        private bool CanMove
        {
            get 
            {
                return _canMove; 
            } 
            set 
            { 
                _canMove = value;
                stateMachine.Animator.SetBool("IsWalk", _canMove);
            }
        }
        private float _currentSpeed = 0;
        
        public override void Enter()
        {
            _currentSpeed = stateMachine.Enemy.MoveVelocity;
            CanMove = true;
        }

        public override void Exit()
        {
            stateMachine.Animator.SetBool("IsWalk", false);
        }
        public override void Update()
        {
            base.Update();
            if (CanMove)
            {
                Move();
            }      
            
        }
        private void Move()
        {
            Vector3 currentPosition = stateMachine.Tr.position;
            Vector3 targetPosition = _isRightMove ? stateMachine.Enemy.RightBorder : stateMachine.Enemy.LeftBorder;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0.0f, stateMachine.Enemy.MoveVelocity);
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, _currentSpeed * Time.deltaTime);
            stateMachine.Tr.position = new Vector2(newPosition.x, stateMachine.Tr.position.y);
            Flip(targetPosition, currentPosition);
            _currentSpeed += stateMachine.Enemy.AccelerateSpeed;
            if (Mathf.Abs(stateMachine.Tr.position.x - targetPosition.x) < 0.01f)
            {
                stateMachine.Tr.position = new Vector2(targetPosition.x, stateMachine.Tr.position.y);
                _isRightMove = !_isRightMove;
                _currentSpeed = 0;
                stateMachine.StartCoroutine(Idle());
            }
        }
        private void Flip(Vector3 targetPosition, Vector3 currentPosition)
        {
            stateMachine.Tr.localScale = new Vector2(Mathf.Sign((targetPosition - currentPosition).x), 1);
        }
        private IEnumerator Idle()
        {
            CanMove = false;
            yield return new WaitForSeconds(stateMachine.Enemy.IdlePatrollDuration);
            stateMachine.Animator.SetTrigger("Rotate");
        }
        public override void AnimationEventHandler()
        {
            CanMove = true;
        }
        public override void CheckTransaction()
        {
        }

        public Patrolling(StateMachine sm):base(sm) { }
    }
}

