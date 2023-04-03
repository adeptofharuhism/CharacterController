namespace Assets.CodeBase.Infrastructure.Properties
{
    public interface IAnimationEventUser
    {
        void OnAnimationEnterEvent();
        void OnAnimationExitEvent();
        void OnAnimationTransitEvent();
    }
}
