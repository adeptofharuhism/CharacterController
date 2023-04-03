using Assets.CodeBase.Constants;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.Input;

namespace Assets.CodeBase.Infrastructure.States
{
    public class BootstrapState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services) {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() {
            _sceneLoader.Load(SceneNames.Initial, OnLoaded);
        }

        public void Exit() {

        }

        private void OnLoaded() {
            _stateMachine.Enter<LoadLevelScene, string>(SceneNames.Test);
        }

        private void RegisterServices() {
            _services.RegisterSingle<IStateMachine>(_stateMachine);
            _services.RegisterSingle(PrepareInputService());
        }

        private IInputService PrepareInputService() {
            IInputService inputService = new InputService();
            inputService.Initialize();

            return inputService;
        }
    }
}