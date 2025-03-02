using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
                .BindInterfacesAndSelfTo<MovementController>()
                .FromNew()
                .AsSingle();

        Container
                .Bind<Inventory>()
                .FromNew()
                .AsSingle();
    }
}