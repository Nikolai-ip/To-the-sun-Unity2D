using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace AISystem
{
    public class Patrolling : BaseState
    {
        private bool _isRightMove = true;
        private bool _canMove = true;
        private float _currentSpeed = 0;
        
        public override void Enter()
        {
            _currentSpeed = stateMachine.Enemy.MoveVelocity;
        }

        public override void Exit()
        {
        }

        public override void FixedUpdate()
        {
        }

        public override void Update()
        {
            base.Update();
            if (_canMove)
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
            stateMachine.Tr.localScale = new Vector2(Mathf.Sign((targetPosition - currentPosition).x),1);
            _currentSpeed += stateMachine.Enemy.AccelerateSpeed;
            if (Mathf.Abs(stateMachine.Tr.position.x - targetPosition.x) < 0.01f)
            {
                stateMachine.Tr.position = new Vector2(targetPosition.x, stateMachine.Tr.position.y);
                _isRightMove = !_isRightMove;
                _currentSpeed = 0;
                stateMachine.StartCoroutine(Idle());
            }
        }
        private IEnumerator Idle()
        {
            _canMove = false;
            yield return new WaitForSeconds(stateMachine.Enemy.IdlePatrollDuration);
            _canMove = true;
        }
        public override void CheckTransaction()
        {
        }

        public Patrolling(StateMachine sm):base(sm) { }
    }
}

