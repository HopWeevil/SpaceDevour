using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private float _initialScale = 1.7f;
        [SerializeField] private float _finalScale = 1f;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private float _shakeDuration = 0.2f;
        [SerializeField] private float _shakeStrength = 1f;
        [SerializeField] private Ease _scaleEaseIn = Ease.OutBack;
        [SerializeField] private Ease _scaleEaseOut = Ease.InBack;

        public void SetDamageAmount(int amount)
        {
            _damageText.text = $"-{amount.ToString()}";
        }

        public void PlayAnimation()
        {
            Sequence damageSequence = DOTween.Sequence();

            damageSequence
                .Append(_damageText.transform.DOScale(_initialScale, _scaleDuration).SetEase(_scaleEaseIn))
                .Join(_damageText.DOFade(1f, _fadeDuration))
                .Append(_damageText.transform.DOScale(_finalScale, _scaleDuration).SetEase(_scaleEaseOut))
                .Join(_damageText.DOFade(0f, _fadeDuration))
                .Join(_damageText.transform.DOShakePosition(_shakeDuration, _shakeStrength))
                .OnComplete(() => Destroy(_damageText.gameObject));
        }
    }
}