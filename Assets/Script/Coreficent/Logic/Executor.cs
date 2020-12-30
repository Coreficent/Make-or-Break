namespace Coreficent.Logic
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Executor
    {
        public static Executor Singleton = new Executor();

        public Dictionary<string, Artifact> ArtifactLookup = new Dictionary<string, Artifact>();
        public bool _transitioning = false;

        private List<Artifact> _artifacts = new List<Artifact>();

        public bool Transitioning
        {
            get
            {
                foreach (Artifact artifact in _artifacts)
                {
                    if (artifact.Transitioning)
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        public void Initialize(Transform artifactContainer)
        {
            foreach (Transform artifactTransform in artifactContainer)
            {
                Artifact artifact = artifactTransform.GetComponent<Artifact>();

                _artifacts.Add(artifact);
                ArtifactLookup.Add(artifactTransform.name, artifact);
            }
        }

        public void ResetAdvancedState()
        {
            foreach (Artifact artifact in _artifacts)
            {
                artifact.Advanced = false;
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
                        return;
                    }
                }
            }
        }
    }
}
