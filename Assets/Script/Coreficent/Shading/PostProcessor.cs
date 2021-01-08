namespace Coreficent.Shading
{
    using Coreficent.Utility;
    using UnityEngine;

    public class PostProcessor : MonoBehaviour
    {
        [SerializeField] private Material _material;

        protected void Start()
        {
            SanityCheck.Check(this, _material);
        }

        protected void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, _material);
        }
    }
}
