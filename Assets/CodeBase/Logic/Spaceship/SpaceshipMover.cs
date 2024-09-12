using CodeBase.Services.Input;
using CodeBase.SO;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Spaceship
{
    public class SpaceshipMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        [SerializeField] private Transform[] _enginesFire;

        private float _rotationSpeed;
        private float _acceleration;
        private float _backwardAcceleration;
        private float _maxSpeed;
        private float _driftFactor;

        private IInputService _input;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _input = inputService;
        }

        public void SetStats(ShipStaticData data)
        {
            _rotationSpeed = data.RotationSpeed;
            _acceleration = data.Acceleration;
            _backwardAcceleration = data.BackwardAcceleration;
            _maxSpeed = data.MaxSpeed;
            _driftFactor = data.DriftFactor;
            _rigidBody.drag = data.Drag;
            _rigidBody.angularDrag = data.AngularDrag;
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
            ApplyDrift();
            ClampSpeed();
        }

        private void HandleMovement()
        {
            float verticalInput = _input.Axis.y;
            float currentAcceleration = (verticalInput > 0) ? _acceleration : _backwardAcceleration;

            SeActiveEnginesFire(verticalInput > 0);

            Vector2 inputDirection = transform.up * verticalInput * currentAcceleration;
            _rigidBody.AddForce(inputDirection, ForceMode.Force);
        }

        private void HandleRotation()
        {
            float horizontalInput = _input.Axis.x;
            float rotationInput = -horizontalInput * _rotationSpeed;
            _rigidBody.AddTorque(new Vector3(0, 0, rotationInput) * Time.fixedDeltaTime, ForceMode.Force);
        }

        private void ApplyDrift()
        {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(_rigidBody.velocity, transform.up);
            Vector2 lateralVelocity = transform.right * Vector2.Dot(_rigidBody.velocity, transform.right);
            _rigidBody.velocity = forwardVelocity + lateralVelocity * _driftFactor;
        }

        private void ClampSpeed()
        {
            if (_rigidBody.velocity.magnitude > _maxSpeed)
            {
                _rigidBody.velocity = _rigidBody.velocity.normalized * _maxSpeed;
            }
        }

        private void SeActiveEnginesFire(bool state)
        {
            for (int i = 0; i < _enginesFire.Length; i++)
            {
                Transform engine = _enginesFire[i];

                if (state)
                {
                    engine.transform.localScale = Vector3.one;
                    engine.gameObject.SetActive(true);
                }
                else
                {
                    engine.DOScale(Vector3.zero, 0.5f).OnComplete(() => engine.gameObject.SetActive(false));
                }
            }
        }
    }
}