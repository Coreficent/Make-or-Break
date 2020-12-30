namespace Coreficent.Interface
{
    using Coreficent.Utility;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ArtifactButton : SpriteButton
    {
        [SerializeField] private bool _activated;
        private string _predicate;
        protected override void Awake()
        {
            base.Awake();
            SanityCheck.Check(this, _activated);

            _button.interactable = _activated;

            DebugLogger.Awake(this);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
        }
    }
}

