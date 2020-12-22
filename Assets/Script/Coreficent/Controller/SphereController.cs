namespace Coreficent.Transform
{
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
            if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5))
            {
                LerpTo(Vector3.back, Vector3.up);
            }
            if (Input.GetKey(KeyCode.Alpha0) || Input.GetKey(KeyCode.Keypad0))
            {
                LerpTo(Vector3.forward, Vector3.up);
            }
            if (Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.Keypad8))
            {
                LerpTo(Vector3.up, Vector3.forward);
            }
            if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            {
                LerpTo(Vector3.down, Vector3.back);
            }
            if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
            {
                LerpTo(Vector3.left, Vector3.up);
            }
            if (Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Keypad6))
            {
                LerpTo(Vector3.right, Vector3.up);
            }

            _direction.x = Input.GetAxis(_horizontalControl);
            _direction.y = Input.GetAxis(_verticalControl);
            transform.rotation *= Quaternion.AngleAxis(_direction.normalized.x * RotationSpeed * Time.deltaTime, transform.InverseTransformDirection(Vector3.down));
            transform.rotation *= Quaternion.AngleAxis(_direction.normalized.y * RotationSpeed * Time.deltaTime, transform.InverseTransformDirection(Vector3.right));
        }

        private void LerpTo(Vector3 forward, Vector3 upwards)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward, upwards), RotationSpeed * 2.0f * Time.deltaTime);
        }
    }
}
