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
        private bool _pendingAnimation = false;

        // initializer
        private void Start()
        {
            DebugLogger.Log("main initialized");
            _artifactContainer = transform.Find("Display").transform.Find("Artifact");
            ConstructLookup();
        }

        // game loop
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || _pendingAnimation)
            {
                foreach (Transform artifact in _artifactContainer)
                {
                    if (artifact.GetComponent<Artifact>().Transitioning)
                    {
                        //return;
                    }
                }

                foreach (Transform artifact in _artifactContainer)
                {
                    bool canAdvance = artifact.GetComponent<Artifact>().CanAdvance(_artifactLookup);
                    DebugLogger.Log(artifact.name + " can advance", canAdvance);
                    if (canAdvance)
                    {
                        _pendingAnimation = true;
                        artifact.GetComponent<Artifact>().Advance();
                        return;
                    }
                }
                _pendingAnimation = false;
            }
        }

        private void ConstructLookup()
        {
            foreach (Transform artifact in _artifactContainer)
            {
                artifact.GetComponent<Artifact>().ParsePredicates();
                _artifactLookup.Add(artifact.name, artifact.gameObject);
            }
        }
    }
}
