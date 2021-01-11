namespace Coreficent.Interface
{
    using Coreficent.Logic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class Restart : MonoBehaviour
    {

        public void RestartGame()
        {
            Executor.Singleton.ArtifactLookup.Clear();
            Executor.Singleton._artifacts.Clear();
            ArtifactButton.ArtifactButtons.Clear();
            ArtifactButton.Round = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

