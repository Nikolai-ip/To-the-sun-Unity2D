using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(fileName = "Patrolling state", menuName = "ScriptableObjects/States/Patrolling", order = 1)]

    public class Patrolling : BaseState
    {
        private Vector2 _leftPatrollBorder;
        private Vector2 _rightPatrollBorder;
        [SerializeField] private float _moveVelocity;
        private bool isFinishMove;
        private Vector2 _target;
        private Coroutine _moveCoroutine;
        public override void Init(StateMachine stateMachine)
        {
            base.Init(stateMachine);
            isFinishMove = true;
            var chieldPositions = stateMachine.GetComponentsInChildren<Transform>().ToList();
            _leftPatrollBorder = chieldPositions.Find(pos => pos.tag == "LeftBorder").position;
            _rightPatrollBorder = chieldPositions.Find(pos => pos.tag == "RightBorder").position;
        }

        public override void Update()
        {
            base.Update();
            if (isFinishMove)
            {
                isFinishMove = false;
                _target = _target == _leftPatrollBorder? _rightPatrollBorder: _leftPatrollBorder;
                _moveCoroutine = stateMachine.StartCoroutine(MoveObjectToPosition(_target));
            }
        }
        protected IEnumerator MoveObjectToPosition(Vector3 targetPosition)
        {
            var delay = new WaitForFixedUpdate();
            float timeElapsed = 0f;
            Vector3 startingPosition = stateMachine.Tr.position;
            float duration = Vector2.Distance(startingPosition, targetPosition)/_moveVelocity;
            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                float t = Mathf.Clamp01(timeElapsed / duration);
                stateMachine.Tr.position = new Vector2(Vector3.Lerp(startingPosition, targetPosition, t).x, stateMachine.Tr.position.y);
                yield return delay;
            }
            stateMachine.Tr.position = new Vector3(targetPosition.x, stateMachine.Tr.position.y);
            isFinishMove = true;
        }
        public override void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_rightPatrollBorder, 0.1f);
            Gizmos.DrawWireSphere(_leftPatrollBorder, 0.1f);

        }

        public override void Enter()
        {
            isFinishMove = false;
        }

        public override void Exit()
        {
            stateMachine.StopCoroutine(_moveCoroutine);
        }

        public override void TransactionCheck()
        {

        }
    }
}

