using UnityEngine;

namespace CodeBase.Logic
{
    public class Asteroid : MonoBehaviour
    {
        private Vector3 _planetPosition;
        private float _orbitRadius;
        private float _orbitSpeed;
        private float _rotationSpeed;
        private float _angle;

        public void Initialize(Vector3 planetPosition, float orbitRadius, float orbitSpeed, float rotationSpeed, float size)
        {
            _planetPosition = planetPosition;
            _orbitRadius = orbitRadius;
            _orbitSpeed = orbitSpeed;
            _rotationSpeed = rotationSpeed;
            _angle = Random.Range(0f, 360f);

            SetSize(size);
        }

        private void SetSize(float size)
        {
            transform.localScale = Vector3.one * size;
        }

        public void UpdateMovement()
        {
            UpdateOrbit();
            UpdateRotation();
        }

        private void UpdateOrbit()
        {
            _angle += _orbitSpeed * Time.deltaTime;
            Vector3 newPosition = _planetPosition + new Vector3(Mathf.Cos(_angle) * _orbitRadius, Mathf.Sin(_angle) * _orbitRadius, 0);
            transform.position = newPosition;
        }

        private void UpdateRotation()
        {
            transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }
    }
}