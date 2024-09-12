using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;
using CodeBase.UI.Elements;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly DiContainer _container;
        private Canvas _uiRoot;

        public UIFactory(DiContainer container, IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
            _container = container;
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.PopupMessage);
            await _assets.Load<GameObject>(AssetAddress.DamagePopup);
        }

        public void CleanUp()
        {
            _assets.CleanUp();
        }

        public async Task<PopupMessage> CreatePopupMessage(Color color, string text)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.PopupMessage);
            PopupMessage message = Object.Instantiate(prefab, _uiRoot.transform).GetComponent<PopupMessage>();
            message.SetColor(color);
            message.SetText(text);
            return message;
        }

        public async Task<DamagePopup> CreateDamagePopup(Vector3 at, int damageAmount, float heightOffset = 1f)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.DamagePopup);
            DamagePopup message = Object.Instantiate(prefab, new Vector3(at.x, at.y+ heightOffset, at.z -1), Quaternion.identity).GetComponent<DamagePopup>();
            message.SetDamageAmount(damageAmount);
            message.PlayAnimation();
            return message;
        }

        public async Task CreateUIRoot()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.UIRootPath);
            _uiRoot = Object.Instantiate(prefab).GetComponent<Canvas>();
        }

        public async Task<GameObject> CreateHud()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetAddress.HudPath);
            GameObject hud = Object.Instantiate(prefab);
            _container.InjectGameObject(hud);
            return hud;
        }
    }
}