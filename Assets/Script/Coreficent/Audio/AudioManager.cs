namespace Coreficent.Audio
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Singleton = null;

        protected void awake()
        {
            Singleton = this;
        }
    }
}
