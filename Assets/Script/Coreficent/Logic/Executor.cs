namespace Coreficent.Logic
{
    using Coreficent.Cursor;
    using System.Collections.Generic;
    using UnityEngine;

    public class Executor
    {
        public static Executor Singleton = new Executor();

        public Dictionary<string, Artifact> ArtifactLookup = new Dictionary<string, Artifact>();

        private List<Artifact> _artifacts = new List<Artifact>();

        private bool _transitioning = false;
        public bool Transitioning
        {
            get
            {
                foreach (Artifact artifact in _artifacts)
                {
                    if (artifact.CanAdvance() || artifact.CurrentState != artifact.NextState)
                    {
                        return true;
                    }
                }

                return false;
            }
        }


        public void Initialize(List<Transform> artifactContainers)
        {
            foreach (Transform artifactContainer in artifactContainers)
            {
                foreach (Transform artifactTransform in artifactContainer)
                {
                    Artifact artifact = artifactTransform.GetComponent<Artifact>();

                    _artifacts.Add(artifact);
                    ArtifactLookup.Add(artifactTransform.name, artifact);
                }
            }
        }

        public void ResetAdvancedState()
        {
            foreach (Artifact artifact in _artifacts)
            {
                artifact.Advanced = false;
            }
        }

        public bool Run()
        {
            foreach (Artifact artifact in _artifacts)
            {
                if (artifact.CanAdvance())
                {
                    artifact.Advance();

                    return false;
                }
            }

            return true;
        }
    }
}
