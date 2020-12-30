namespace Coreficent.Main
{
    using Coreficent.Artifact;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        [SerializeField] private Transform _artifactContainer;

        private readonly Dictionary<string, Artifact> _artifactLookup = new Dictionary<string, Artifact>();
        private readonly List<Artifact> _artifacts = new List<Artifact>();

        private bool _pendingAnimation = false;

        // initializer
        private void Start()
        {
            SanityCheck.Check(this, _artifactContainer);
            ConstructLookup();
            DebugLogger.Start(this);
        }

        // game loop
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || _pendingAnimation)
            {
                foreach (Artifact artifact in _artifacts)
                {
                    if (artifact.CanAdvance(_artifactLookup))
                    {
                        _pendingAnimation = true;
                        artifact.Advance();
                        return;
                    }
                }
                _pendingAnimation = false;
            }
        }

        private void ConstructLookup()
        {
            foreach (Transform artifactTransform in _artifactContainer)
            {
                Artifact artifact = artifactTransform.GetComponent<Artifact>();

                _artifacts.Add(artifact);
                _artifactLookup.Add(artifactTransform.name, artifact);
            }
        }
    }
}
