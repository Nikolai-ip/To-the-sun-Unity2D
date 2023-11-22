using UnityEngine;

namespace AISystem
{
    public class Shot : BaseState
    {
        private bool _rotating;
        private Animator _weaponAnimator;
        public Shot(StateMachine stateMachine):base(stateMachine)
        {
            _weaponAnimator = stateMachine.Weapon.GetComponent<Animator>();
        }
        
        public override void Enter()
        {
            if (PlayerIsBehind())
            {
                stateMachine.Animator.SetTrigger("Rotate");
                _rotating = true;
            }
            else
            {
                stateMachine.Animator.SetTrigger("TakeOffGun");
            }
        }

        public override void Exit()
        {
            stateMachine.Weapon.SetActive(false);
            stateMachine.Animator.SetLayerWeight((int)AnimatorLayer.WithWeapon, 0);
        }

        public override void Update()
        {
            base.Update();
            RotateWeapon(stateMachine.PlayerActor.transform.position, stateMachine.Enemy.RotateWeaponAngle);
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
                var localScaleX = stateMachine.Tr.localScale.x;
                stateMachine.Weapon.transform.localScale = new Vector2(localScaleX, localScaleX);
            }
        }

        public void FireAShot()
        {
            _weaponAnimator.SetTrigger("Shoot");
        }

        private void RotateWeapon(Vector2 target, float rotateAngle)
        {
            var diff = new Vector3(target.x, target.y) - stateMachine.Weapon.transform.position;
            diff.Normalize();
            var rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            stateMachine.Weapon.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - rotateAngle);
        }

        private enum AnimatorLayer
        {
            WithoutWeapon,
            WithWeapon
        }
        
    }
}