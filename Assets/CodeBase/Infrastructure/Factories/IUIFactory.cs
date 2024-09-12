using CodeBase.UI.Elements;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IUIFactory
    {
        Task CreateUIRoot();
        Task<GameObject> CreateHud();
        Task<PopupMessage> CreatePopupMessage(Color color, string text);
        Task<DamagePopup> CreateDamagePopup(Vector3 at, int damageAmount, float heightOffset = 1f);
        Task WarmUp();
        void CleanUp();
    }
}