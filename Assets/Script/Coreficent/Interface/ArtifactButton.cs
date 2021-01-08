namespace Coreficent.Interface
{
    using Coreficent.Cursor;
    using Coreficent.Logic;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ArtifactButton : SpriteButton
    {
        private static List<ArtifactButton> ArtifactButtons = new List<ArtifactButton>();
        private static int Round = 0;

        [SerializeField] private List<string> _creations = new List<string>();
        [SerializeField] private int _round = 0;

        protected override void Start()
        {
            base.Start();

            SanityCheck.Check(this, _creations);

            _button.interactable = _round == Round;

            ArtifactButtons.Add(this);

            DebugLogger.Start(this);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (CursorController.Singleton.CursorOn && _button.interactable)
            {
                base.OnPointerClick(eventData);

                Executor.Singleton.ResetAdvancedState();

                foreach (string creation in _creations)
                {
                    Artifact artifact = Executor.Singleton.ArtifactLookup[creation];

                    artifact.NextState = "Originate";
                    artifact.Advance();
                }

                UpdateRound();
            }
        }

        private void UpdateRound()
        {
            ++Round;
            foreach (ArtifactButton artifact in ArtifactButtons)
            {
                artifact._button.interactable = artifact._round == Round;
            }
        }
    }
}

