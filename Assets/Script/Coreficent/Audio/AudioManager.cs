namespace Coreficent.Audio
{
    using Coreficent.Utility;
    using System.Text.RegularExpressions;
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Singleton = null;

        public AudioSource Seed;
        public AudioSource Plant;
        public AudioSource Mushroom;
        public AudioSource Generics;
        public AudioSource Fire;

        protected void Start()
        {
            Singleton = this;

            SanityCheck.Check(Seed);
        }

        public void Play(string name)
        {
            string trim = Regex.Replace(name, @"[\d-]", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty).ToLower();

            if (trim == "seed")
            {
                Seed.Play();
            }
            else if (trim == "tree")
            {
                Plant.Play();
            }
            else if (trim == "mushroom")
            {
                Mushroom.Play();
            }
            else
            {
                Generics.Play();
            }
        }
    }
}
