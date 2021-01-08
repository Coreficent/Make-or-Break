namespace Coreficent.Controller
{
    using Coreficent.Utility;
    using UnityEngine;

    public class ColorController : MonoBehaviour
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
            if (!Color.clear.Equals(_color))
            {
                _material.SetColor("_Color", _color);
            }
        }
    }
}
