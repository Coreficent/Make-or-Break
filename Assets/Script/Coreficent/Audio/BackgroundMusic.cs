namespace Coreficent.Audio
{
    using Coreficent.Utility;
    using UnityEngine;

    public class BackgroundMusic : MonoBehaviour
    {
        public AudioSource Music;

        private static BackgroundMusic _backgroundMusic = null;

        protected void Start()
        {
            SanityCheck.Check(this, Music, Music);

            Music.Play();
        }

        protected void Awake()
        {
            if (!_backgroundMusic)
            {
                _backgroundMusic = this;
                DontDestroyOnLoad(gameObject);
                return;
            }
            else if (_backgroundMusic == this)
            {
                return;
            }
            Destroy(gameObject);
        }
    }
}
