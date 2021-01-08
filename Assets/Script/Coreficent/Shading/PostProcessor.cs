namespace Coreficent.Shading
{
    using Coreficent.Utility;
    using UnityEngine;

    public class PostProcessor : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private Color _tint;

        protected void Start()
        {
            SanityCheck.Check(this, _material, _tint);
        }

        protected void Update()
        {

        }

        protected void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            _material.SetColor("_Color", _tint);

            Graphics.Blit(source, destination, _material);
        }
    }
}
