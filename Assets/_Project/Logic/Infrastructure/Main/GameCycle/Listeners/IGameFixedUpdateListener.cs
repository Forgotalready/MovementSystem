public interface IGameFixedUpdateListener : IGameListener
{
    void OnFixedUpdate(float deltaTime);
}