namespace Assets.CodeBase.Character.Animation
{
    public interface IAnimationStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        CharacterAnimationState State { get; }
    }
}
