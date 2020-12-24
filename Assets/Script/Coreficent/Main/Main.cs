namespace Coreficent.Main
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        public GameObject Tree;

        private Dictionary<string, GameObject> _artifactLookup = new Dictionary<string, GameObject>();

        // initializer
        void Start()
        {
            DebugLogger.Log("main initialized");
            SanityCheck.Check(this, Tree);
            DebugLogger.Log(Tree.name);
        }

        // game loop
        void Update()
        {

        }
    }
}
