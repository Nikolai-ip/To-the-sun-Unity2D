using Player.StateMachines;
using UnityEngine;
using StateMachine = AISystem.StateMachine;

namespace Player.PlayerStates
{
    [CreateAssetMenu(fileName = "Die", menuName = "ScriptableObjects/States/DieState", order = 1)]
    public class DieState : PlayerState
    {
        private int _animationEventIndex;

        public override void Enter()
        {
            inputHandler.enabled = false;
            if (sm.EnemyKiller.transform.localScale.x * tr.localScale.x > 0)
                animator.SetTrigger("DeathFacingForward");
            else
                animator.SetTrigger("DeathFacingBack");
        }

        public override void Exit()
        {
            inputHandler.enabled = true;
            sm.EnemyKiller = null;
            _animationEventIndex = 0;
        }

        public override void AnimationEventHandle()
        {
            switch (_animationEventIndex)
            {
                case 0:
                    EnemyFire();
                    break;
                case 1:
                    GameManager.Instance.GameOver();
                    break;
            }

            _animationEventIndex++;
        }

        private void EnemyFire()
        {
            sm.EnemyKiller.GetComponent<StateMachine>().Fire();
        }

        public override void Initialize(PlayerStateMachine sm)
        {
            base.Initialize(sm);
            _animationEventIndex = 0;
        }
    }
}