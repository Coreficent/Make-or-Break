namespace Coreficent.Controller
{
    using Coreficent.Logic;
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
            if (Color.clear != _color || Color.white != _color)
            {
                SetColor(_color);
            }
            /*
            Artifact artifact = null;
            artifact = GetComponent<Artifact>();
            if (!artifact)
            {
                artifact = transform.parent.GetComponent<Artifact>();
            }
            if (artifact && artifact.CurrentState.Contains("Hide"))
            {
                DebugLogger.Log("no action");
            }
            else
            {
                SetColor(_color);
            }
            */
        }

        private void OnApplicationQuit()
        {
            if (_material.shader.name == "Skybox/Cubemap")
            {
                SetColor(Color.gray);
            }
            else
            {
                SetColor(Color.white);
            }
        }

        private void SetColor(Color color)
        {
            if (_material.shader.name == "Coreficent/Anime" || _material.shader.name == "Coreficent/PostProcessor")
            {
                _material.SetColor("_Color", color);
            }
            else if (_material.shader.name == "Skybox/Cubemap")
            {
                _material.SetColor("_Tint", color);
            }
        }
    }
}
