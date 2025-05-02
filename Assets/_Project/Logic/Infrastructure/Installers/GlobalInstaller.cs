using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IStrorage>()
                .To<FileStorage>()
                .FromNew()
                .AsSingle()
                .WithArguments("Save");
        
        Container
                .Bind<SaveService>()
                .FromNew()
                .AsSingle();
    }
}