using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.States;

namespace Assets.CodeBase.Infrastructure
{
    public class Game
    {
        private GameStateMachine _gameStateMachine;

        public GameStateMachine StateMachine => _gameStateMachine;

        public Game(ICoroutineRunner coroutineRunner) {
            _gameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
        }
    }
}