using Cinemachine;
using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Sceneloader;
using CodeBase.Logic;
using CodeBase.Logic.Spaceship;
using CodeBase.Services.StaticData;
using CodeBase.SO;
using CodeBase.UI.Elements;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<LevelStaticData>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uIFactory;
        private readonly IStaticDataService _staticData;
        private LevelStaticData _levelToLoad;

        public LoadLevelState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain, IGameFactory gameFactory, IUIFactory uIFactory, IStaticDataService staticData)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uIFactory = uIFactory;
            _staticData = staticData;
        }

        public void Enter(LevelStaticData level)
        {
            _levelToLoad = level;

            _loadingCurtain.Show();
            _uIFactory.WarmUp();
            _gameFactory.WarmUp();

            _sceneLoader.Load(_levelToLoad.LevelKey, OnLoadedAsync);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private async void OnLoadedAsync()
        {
            await InitUiRoot();
            await InitGameWorld(_levelToLoad);
            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitGameWorld(LevelStaticData levelStaticData)
        {
            GameObject player = await InitPlayerSpaceship(levelStaticData);
            await InitHud(player);
            await InitPlanetSpawners(levelStaticData);
            CameraFollow(player.transform);
        }

        private void CameraFollow(Transform target)
        {
            CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
            brain.ActiveVirtualCamera.Follow = target;
        }

        private async Task InitUiRoot()
        {
            await _uIFactory.CreateUIRoot();
        }

        private async Task InitHud(GameObject ship)
        {
            GameObject hud = await _uIFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>().Construct(ship.GetComponent<IHealth>());
        }

        private async Task<GameObject> InitPlayerSpaceship(LevelStaticData levelData)
        {
            return await _gameFactory.CreateSpaceship(levelData.InitialPlayerShipPosition, Enums.ShipTypeId.SmallSpaceship);
        }

        private async Task InitPlanetSpawners(LevelStaticData levelData)
        {
            foreach (PlanetData spawnerData in levelData.Planets)
            {
                await _gameFactory.CreatePlanet(spawnerData.Position, spawnerData.TypeId);
            }
        }
    }
}