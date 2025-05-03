public interface IGameLateUpdateListener : IGameListener
{
    void OnLateUpdate(float deltaTime);
}