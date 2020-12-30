namespace Coreficent.Logic
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Executor
    {
        public static Executor Singleton = new Executor();

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
