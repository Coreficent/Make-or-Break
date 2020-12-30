﻿namespace Coreficent.Artifact
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ArtifactExecutor
    {
        public static ArtifactExecutor Singleton = new ArtifactExecutor();

        public Dictionary<string, Artifact> ArtifactLookup = new Dictionary<string, Artifact>();
        public bool Transitioning = false;

        private List<Artifact> _artifacts = new List<Artifact>();

        public void Initialize(Transform artifactContainer)
        {
            foreach (Transform artifactTransform in artifactContainer)
            {
                Artifact artifact = artifactTransform.GetComponent<Artifact>();

                _artifacts.Add(artifact);
                ArtifactLookup.Add(artifactTransform.name, artifact);
            }
        }

        public void Run()
        {
            if (!Transitioning)
            {
                foreach (Artifact artifact in _artifacts)
                {
                    if (artifact.CanAdvance())
                    {
                        artifact.Advance();
                        Transitioning = true;
                        return;
                    }
                }
            }
        }
    }
}
