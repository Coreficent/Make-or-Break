namespace Coreficent.Main
{
    using Coreficent.Logic;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : MonoBehaviour
    {
        [SerializeField] private float _gameSpeed = 1.0f;
        [SerializeField] private List<Transform> _artifactContainer = new List<Transform>();

        // initializer
        private void Start()
        {
            SanityCheck.Check(this, _artifactContainer, _gameSpeed);

            Executor.Singleton.Initialize(_artifactContainer);

            Time.timeScale = _gameSpeed;

            DebugLogger.Start(this);
        }

        // game loop
        private void Update()
        {
            // Executor.Singleton.Run();
        }
    }
}
