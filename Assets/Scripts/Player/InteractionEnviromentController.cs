using UnityEngine;

namespace Player
{
    public class InteractionEnviromentController : UINotifier
    {
        private Animator _animator;
        private Entity _interactiveEntity;

        public Entity InteractiveEntity
        {
            get => _interactiveEntity;
            set
            {
                _interactiveEntity = value;
                var UIText = InteractiveEntity is null ? string.Empty : InteractiveEntity.UITextInteraction;
                OnEntityCanChanged(UIText);
            }
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void EntityInteraction()
        {
            if (InteractiveEntity is null || !InteractiveEntity.enabled)
            {
                OnEntityCanChanged(string.Empty);
                return;
            }

            _animator.SetTrigger(InteractiveEntity.TriggerAnimation);

            var interactive = InteractiveEntity as IInteractivable;
            interactive.Interact();

            var UIText = InteractiveEntity is null ? string.Empty : InteractiveEntity.UITextInteraction;
            OnStateChanged(UIText);
        }
    }
}