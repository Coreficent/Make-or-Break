namespace Coreficent.Interface
{
    using Coreficent.Logic;
    using Coreficent.Utility;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ArtifactButton : SpriteButton
    {
        [SerializeField] private bool _activated;
        [SerializeField] private string _creation;
        protected override void Awake()
        {
            base.Awake();

            SanityCheck.Check(this, _activated, _creation);

            _button.interactable = _activated;

            DebugLogger.Awake(this);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            Artifact artifact = Executor.Singleton.ArtifactLookup[_creation];

            artifact.NextState = "Origin";
            artifact.Advance();
        }
    }
}

