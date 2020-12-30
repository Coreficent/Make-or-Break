namespace Coreficent.Main
{
    using Coreficent.Artifact;
    using Coreficent.Utility;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        [SerializeField] private Transform _artifactContainer;


        private ArtifactExecutor _artifactExecutor = new ArtifactExecutor();

        // initializer
        private void Start()
        {
            SanityCheck.Check(this, _artifactContainer);

            _artifactExecutor.Initialize(_artifactContainer);

            DebugLogger.Start(this);
        }

        // game loop
        private void Update()
        {
            _artifactExecutor.Run();
        }

    }
}
