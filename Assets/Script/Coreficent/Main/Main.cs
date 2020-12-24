namespace Coreficent.Main
{
    using Coreficent.Artifact;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        private Dictionary<string, GameObject> _artifactLookup = new Dictionary<string, GameObject>();
        private Transform _artifactContainer = null;

        // initializer
        private void Start()
        {
            DebugLogger.Log("main initialized");
            _artifactContainer = transform.Find("Display").transform.Find("Artifact");
            ConstructLookup();

            foreach (Transform artifact in _artifactContainer)
            {
                bool ca = artifact.GetComponent<Artifact>().CanAdvance(_artifactLookup);
                DebugLogger.Log(artifact.name + " can advance", ca);
            }
        }

        // game loop
        private void Update()
        {

        }

        private void ConstructLookup()
        {
            foreach (Transform artifact in _artifactContainer)
            {
                _artifactLookup.Add(artifact.name, artifact.gameObject);
            }
        }
    }
}
