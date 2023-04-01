using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.Input;
using System;
using System.Collections.Generic;

namespace Assets.CodeBase.Infrastructure.States
{
    public class GameStateMachine : StateMachine<IGameExitableState>
    {
        public GameStateMachine(SceneLoader sceneLoader, AllServices services) {
            _states = new Dictionary<Type, IGameExitableState> {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelScene)] = new LoadLevelScene(this, sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this, services.Single<IInputService>()),
            };
        }
    }
}