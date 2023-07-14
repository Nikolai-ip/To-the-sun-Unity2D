using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AISystem
{
    public class Shoot : BaseState
    {
        enum AnimatorLayer
        {
            WithoutWeapon,
            WithWeapon
        }
        public override void CheckTransaction()
        {
        }
        private bool _rotating = false;
        public override void Enter()
        {
            if (IsPlayerBehindEnemy(stateMachine.Player.transform, stateMachine.Tr))
            {
                stateMachine.Animator.SetTrigger("Rotate");
                _rotating = true;
            }
            else
            {
                stateMachine.Animator.SetTrigger("TakeOffGun");
            }
        }
        public bool IsPlayerBehindEnemy(Transform playerTransform, Transform enemyTransform)
        {
            float playerPosX = playerTransform.position.x;
            float enemyPosX = enemyTransform.position.x;
            float playerScaleX = playerTransform.localScale.x;
            float enemyScaleX = enemyTransform.localScale.x;

            return (playerPosX < enemyPosX && enemyScaleX == 1) || (playerPosX > enemyPosX && enemyScaleX == -1);

        }
        public override void Exit()
        {
            stateMachine.Weapon.SetActive(false);
            stateMachine.Animator.SetLayerWeight((int)AnimatorLayer.WithWeapon, 0);
        }

        public override void Update()
        {
            base.Update();
            RotateWeapon(stateMachine.Player.transform.position, stateMachine.Enemy.RotateWeaponAngle);
        }
        public override void FixedUpdate()
        {
        }
        public override void AnimationEventHandler()
        {
            if (_rotating)
            {
                stateMachine.Animator.SetTrigger("TakeOffGun");
                stateMachine.Tr.localScale = new Vector3(-1, 1, 1);
                _rotating = false;
            }
            else
            {
                stateMachine.Weapon.SetActive(true);
                stateMachine.Animator.SetLayerWeight((int)AnimatorLayer.WithWeapon, 1);
                float localScaleX = stateMachine.Tr.localScale.x;
                stateMachine.Weapon.transform.localScale = new Vector2(localScaleX, localScaleX);
            }
        }
        public void FireAShot()
        {
            stateMachine.Weapon.GetComponent<Animator>().SetTrigger("Shoot");
        }
        private void RotateWeapon(Vector2 target, float rotateAngle)
        {
            Vector3 diff = new Vector3(target.x, target.y) - stateMachine.Weapon.transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            stateMachine.Weapon.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - rotateAngle);
        }
        public Shoot(StateMachine stateMachine) : base(stateMachine)
        {
        }

    }

}
