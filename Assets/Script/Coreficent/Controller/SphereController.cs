namespace Coreficent.Transform
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SphereController : MonoBehaviour
    {
        public float RotationSpeed = 100.0f;

        private readonly string _verticalControl = "Vertical";
        private readonly string _horizontalControl = "Horizontal";

        private Vector3 _direction = new Vector3();

        private void Update()
        {
            ControlRotation();
        }

        private void ControlRotation()
        {
            _direction.x = Input.GetAxis(_horizontalControl);
            _direction.y = Input.GetAxis(_verticalControl);
            transform.rotation *= Quaternion.AngleAxis(_direction.normalized.x * RotationSpeed * Time.deltaTime, transform.InverseTransformDirection(Vector3.down));
            transform.rotation *= Quaternion.AngleAxis(_direction.normalized.y * RotationSpeed * Time.deltaTime, transform.InverseTransformDirection(Vector3.right));
        }
    }
}
