namespace Coreficent.Main
{
    using Coreficent.Artifact;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        public static bool Transitioning = false;


        [SerializeField] private Transform _artifactContainer;

        private readonly List<Artifact> _artifacts = new List<Artifact>();

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
            if (!Transitioning)
            {
                foreach (Artifact artifact in _artifacts)
                {
                    if (artifact.CanAdvance())
                    {
                        artifact.Advance();
                        return;
                    }
                }
            }
        }

        private void ConstructLookup()
        {
            foreach (Transform artifactTransform in _artifactContainer)
            {
                Artifact artifact = artifactTransform.GetComponent<Artifact>();

                _artifacts.Add(artifact);
                Artifact.ArtifactLookup.Add(artifactTransform.name, artifact);
            }
        }
    }
}
