using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public abstract class BaseState
    {
        protected readonly Animator animator;
        private readonly Rigidbody2D _rb;
        protected readonly StateMachine stateMachine;
        protected readonly Transform tr;
        private float _elapsedTime;
        private Coroutine _moveCoroutine;
        protected List<StateType> actionChain;
        protected int currentActionIndex;

        protected bool isRotating;

        private bool _moveCoroutineIsDone;

        protected BaseState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            _rb = stateMachine.GetComponent<Rigidbody2D>();
            tr = stateMachine.GetComponent<Transform>();
            animator = this.stateMachine.GetComponent<Animator>();
            isRotating = false;
            _moveCoroutineIsDone = false;
            currentActionIndex = 0;
            Name = GetType().Name;
        }

        public string Name { get; private set; }

        protected StateType CurrentState { get; set; }


        protected void NextState()
        {
            currentActionIndex++;
            if (currentActionIndex == actionChain.Count)
            {
                ActionChainIsOver();
                currentActionIndex = 0;
            }

            CurrentState = actionChain[currentActionIndex];
        }

        protected virtual void ActionChainIsOver()
        {
        }

        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
            if (_moveCoroutine != null)
            {
                stateMachine.StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }

            _moveCoroutineIsDone = false;
        }

        public virtual void FixedUpdate()
        {
        }


        public virtual void AnimationEventHandler()
        {
        }

        protected void MoveState(float speed)
        {
            if (!_moveCoroutineIsDone && _moveCoroutine == null)
            {
                var currentTarget = GetMoveTarget();
                animator.SetBool("IsWalk", true);
                _moveCoroutine = stateMachine.StartCoroutine(MoveTo(currentTarget, speed));
            }
            else if (_moveCoroutineIsDone)
            {
                _moveCoroutine = null;
                _moveCoroutineIsDone = false;
                animator.SetBool("IsWalk", false);
                NextState();
            }
        }

        private IEnumerator MoveTo(Vector2 targetPos, float speed)
        {
            var dir = (targetPos - (Vector2)tr.position).normalized;
            var tick = new WaitForFixedUpdate();
            Flip(dir.x);
            float t = 0;
            while (Math.Abs(tr.position.x - targetPos.x) > 0.1)
            {
                _rb.velocity = dir * speed;
                t += Time.deltaTime;
                yield return tick;
            }
            _moveCoroutineIsDone = true;
        }

        private IEnumerator MoveByTime(Vector2 targetPos, float duration)
        {
            var tick = new WaitForFixedUpdate();
            Flip((targetPos - (Vector2)tr.position).normalized.x);
            float elapsedTime = 0;
            Vector2 originPos = tr.position;
            while (elapsedTime<duration)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / duration;
                tr.position = Vector2.Lerp(originPos, targetPos, percentageComplete);
                yield return tick;
            }
            _moveCoroutineIsDone = true;
        }
        protected virtual Vector2 GetMoveTarget()
        {
            return Vector2.zero;
        }

        protected void IdleState(float idleTime)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > idleTime)
            {
                NextState();
                _elapsedTime = 0;
            }
        }

        protected void RotateState()
        {
            if (!isRotating)
            {
                isRotating = true;
                animator.SetTrigger("Rotate");
            }
        }

        private void Flip(float x)
        {
            var localScale = tr.localScale;
            localScale = new Vector3(Math.Sign(x), localScale.y);
            tr.localScale = localScale;
        }

        protected bool PlayerIsBehind()
        {
            var dir = stateMachine.PlayerActor.transform.position - tr.position;
            return (dir.x < 0 && tr.localScale.x == 1) || (dir.x > 0 && tr.localScale.x == -1);
        }

        public virtual void HearNoise(Vector2 noiseSourcePos)
        {
            var distanceToNoise = Vector2.Distance(noiseSourcePos, tr.position);
            var nearNoiseDistance = stateMachine.Enemy.DistanceForNearNoise;
            var farNoiseDistance = stateMachine.Enemy.DistanceForFarNoise;

            if (distanceToNoise < nearNoiseDistance)
            {
                stateMachine.ChangeState(stateMachine.NearNoiseReacting);
            }
            else if (distanceToNoise < farNoiseDistance)
            {
                Debug.Log("!");
                stateMachine.FarNoiseReacting.NoiseSourcePos = noiseSourcePos;
                stateMachine.ChangeState(stateMachine.FarNoiseReacting);
                
            }
        }

        protected enum StateType
        {
            Move,
            Idle,
            Rotate
        }
    }
}