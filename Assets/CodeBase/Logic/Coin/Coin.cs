using UnityEngine;

namespace CodeBase.Logic 
{ 
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeedX = 140;
        [SerializeField] private float _rotationSpeedY = 80;
        [SerializeField] private float _rotationSpeedZ = 140;

        private void Update()
        {
            RotateCoin();
        }

        private void RotateCoin()
        {
            float rotationX = _rotationSpeedX * Time.deltaTime;
            float rotationY = _rotationSpeedY * Time.deltaTime;
            float rotationZ = _rotationSpeedZ * Time.deltaTime;

            transform.Rotate(rotationX, rotationY, rotationZ);
        }
    }
}