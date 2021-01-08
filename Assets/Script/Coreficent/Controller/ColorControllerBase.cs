namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class ColorControllerBase : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private Color _color = Color.clear;

        protected void Start()
        {
            SanityCheck.Check(this, _material);

            DebugLogger.Start(this);
        }

        protected void Update()
        {
            _material.SetColor("_Color", _color);
        }

        private void OnApplicationQuit()
        {
            _material.SetColor("_Color", Color.white);
        }
    }
}
