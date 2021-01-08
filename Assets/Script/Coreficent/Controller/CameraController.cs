namespace Coreficent.Controller
{
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public Vector3 Location = new Vector3(0.0f, 0.0f, -15f);

        protected void Update()
        {
            transform.position = transform.InverseTransformVector(Location);
        }
    }
}
