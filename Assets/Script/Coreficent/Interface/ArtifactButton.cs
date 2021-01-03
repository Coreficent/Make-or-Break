namespace Coreficent.Interface
{
    using Coreficent.Cursor;
    using Coreficent.Logic;
    using Coreficent.Utility;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ArtifactButton : SpriteButton
    {
        [SerializeField] private bool _activated;
        [SerializeField] private string _creation;
        protected override void Start()
        {
            base.Start();

            SanityCheck.Check(this, _activated, _creation);

            _button.interactable = _activated;

            DebugLogger.Start(this);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (CursorController.Singleton.CursorOn && _button.interactable)
            {
                base.OnPointerClick(eventData);

                Executor.Singleton.ResetAdvancedState();

                Artifact artifact = Executor.Singleton.ArtifactLookup[_creation];

                artifact.NextState = "Originate";
                artifact.Advance();
            }
        }
    }
}

