namespace Coreficent.Main
{
    using Coreficent.Logic;
    using Coreficent.Utility;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        [SerializeField] private Transform _artifactContainer;

        // initializer
        private void Start()
        {
            SanityCheck.Check(this, _artifactContainer);

            Executor.Singleton.Initialize(_artifactContainer);

            DebugLogger.Start(this);
        }

        // game loop
        private void Update()
        {
            // Executor.Singleton.Run();
        }
    }
}
