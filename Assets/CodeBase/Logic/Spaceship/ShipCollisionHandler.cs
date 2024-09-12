using Cinemachine;
using CodeBase.Infrastructure.Factories;
using CodeBase.Logic.Camera;
using CodeBase.Services.Notification;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Spaceship
{
    [RequireComponent(typeof(IHealth))]
    public class ShipCollisionHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;

        private IPopupMessageService _messageService;
        private CameraShaker _cameraShaker;
        private IUIFactory _uIFactory;
        private IHealth _health;

        [Inject]
        private void Construct(IUIFactory uIFactory, IPopupMessageService messageService)
        {
            _uIFactory = uIFactory;
            _messageService = messageService;
        }

        private void Start()
        {
            _cameraShaker = GetCameraShaker();
            _health = GetComponent<IHealth>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Asteroid asteroid))
            {
                HandleAsteroidCollision();
                PlayCollisionEffect(collision.contacts[0].point);
            }

            if (collision.gameObject.TryGetComponent(out Planet planet))
            {
                HandlePlanetCollision(planet);
            }
        }

        private void HandlePlanetCollision(Planet planet)
        {
            _messageService.ShowMessage($"We reached the {planet.Name} planet!", Color.white);
        }

        private void HandleAsteroidCollision()
        {
            //Можна зробити конфігурацію для астероїду, або зберігати шкоду в астероїді
            int damage = Random.Range(5, 20);

            _health.TakeDamage(damage);
            _uIFactory.CreateDamagePopup(transform.position, damage);
            _cameraShaker.Shake();
            
        }

        public void PlayCollisionEffect(Vector3 collisionPoint)
        {
            Instantiate(_effectPrefab, collisionPoint, Quaternion.identity);
        }

        private CameraShaker GetCameraShaker()
        {
            CinemachineVirtualCamera camera = UnityEngine.Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
            return camera.GetComponent<CameraShaker>();
        }
    }
}