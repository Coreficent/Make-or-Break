namespace Coreficent.Main
{
    using Coreficent.Logic;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        [SerializeField] private List<Transform> _artifactContainer = new List<Transform>();

        // initializer
        private void Start()
        {
            SanityCheck.Check(this, _artifactContainer);

            Executor.Singleton.Initialize(_artifactContainer);

            Time.timeScale = 5.0f;

            DebugLogger.Start(this);
        }

        // game loop
        private void Update()
        {
            // Executor.Singleton.Run();
        }
    }
}
