namespace Assets.CodeBase.Infrastructure.States
{
    public class LoadLevelScene : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelScene(GameStateMachine stateMachine, SceneLoader sceneLoader) {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string nextScene) {
            _sceneLoader.Load(nextScene, OnLoaded);
        }

        public void Exit() {

        }

        private void OnLoaded() {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}