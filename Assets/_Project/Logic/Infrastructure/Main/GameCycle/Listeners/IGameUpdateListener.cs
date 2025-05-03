public interface IGameUpdateListener : IGameListener
{
    void OnUpdate(float deltaTime);
}