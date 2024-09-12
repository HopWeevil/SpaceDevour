using UnityEngine;
using System.Collections;
using CodeBase.SO;

namespace CodeBase.Logic.Spaceship
{
    public class ShipResourceCollector : MonoBehaviour
    {
        [SerializeField] private LineRenderer _hookLineRenderer;
        [SerializeField] private Transform _grabCircle;
        [SerializeField] private GameObject _hookTip;
        [SerializeField] private float _detectionInterval = 0.5f;

        private float _grabRadius;
        private float _hookSpeed;

        private Transform _targetCoin;
        private bool _isCollecting = false;

        private void Start()
        {
            InitializeHook();
            InitializeCircle(_grabRadius);
            StartCoroutine(DetectionRoutine());
        }

        private IEnumerator DetectionRoutine()
        {
            while (true)
            {
                if (!_isCollecting)
                {
                    DetectResources();
                }
                yield return new WaitForSeconds(_detectionInterval);
            }
        }

        public void SetStats(ShipStaticData data)
        {
            _hookSpeed = data.HookSpeed;
            _grabRadius = data.ResourceGrabRadius;
        }

        private void InitializeCircle(float radius)
        {
            _grabCircle.localScale = new Vector3(radius * 2, radius * 2);
        }

        private void InitializeHook()
        {
            _hookLineRenderer.positionCount = 2;
            _hookLineRenderer.SetPosition(1, transform.position);
            _hookTip.SetActive(false);
        }

        private void DetectResources()
        {
            Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, _grabRadius);

            foreach (Collider collider in nearbyObjects)
            {
                if (collider.TryGetComponent(out Coin coin))
                {
                    _targetCoin = coin.transform;
                    StartCoroutine(CollectCoin());
                    break;
                }
            }
        }

        private IEnumerator CollectCoin()
        {
            _isCollecting = true;
            _hookTip.SetActive(true);
            Vector3 hookPosition = transform.position;
            Vector3 coinPosition = _targetCoin.position;

            while (MoveHook(ref hookPosition, coinPosition))
            {
                yield return null;
            }

            Destroy(_targetCoin.gameObject);

            while (MoveHook(ref hookPosition, transform.position))
            {
                yield return null;
            }

            ResetHookAfterCollection();
        }

        private bool MoveHook(ref Vector3 hookPosition, Vector3 targetPosition)
        {
            _hookLineRenderer.SetPosition(0, transform.position);

            hookPosition = Vector3.MoveTowards(hookPosition, targetPosition, _hookSpeed * Time.deltaTime);
            _hookLineRenderer.SetPosition(1, hookPosition);

            if (_hookTip != null)
            {
                RotateHookTipTowards(targetPosition, targetPosition == transform.position);
                _hookTip.transform.position = hookPosition;
            }

            return Vector3.Distance(hookPosition, targetPosition) > 0.1f;
        }

        private void ResetHookAfterCollection()
        {
            _hookLineRenderer.SetPosition(0, transform.position);
            _hookLineRenderer.SetPosition(1, transform.position);
            _hookTip.SetActive(false);
            _isCollecting = false;
        }

        private void RotateHookTipTowards(Vector3 targetPosition, bool isReturningToShip)
        {
            Vector3 direction = targetPosition - _hookTip.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float rotationOffset = isReturningToShip ? 270f : 90f;
            _hookTip.transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _grabRadius);
        }
    }
}