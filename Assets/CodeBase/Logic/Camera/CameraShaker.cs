using Cinemachine;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Camera
{
    public class CameraShaker : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineBasicMultiChannelPerlin _noise;
        private Coroutine _shakeCoroutine;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void Shake(float intensity = 1.5f, float frequency = 1f, float duration = 0.5f)
        {
            if (_shakeCoroutine != null)
            {
                StopCoroutine(_shakeCoroutine);
            }

            _shakeCoroutine = StartCoroutine(ShakeRoutine(intensity, frequency, duration));
        }

        private IEnumerator ShakeRoutine(float intensity, float frequency, float duration)
        {
            _noise.m_AmplitudeGain = intensity;
            _noise.m_FrequencyGain = frequency;

            yield return new WaitForSeconds(duration);

            _noise.m_AmplitudeGain = 0f;
            _noise.m_FrequencyGain = 0f;
            _shakeCoroutine = null;
        }
    }
}
