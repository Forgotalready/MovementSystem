using Zenject;

public class UIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
                .BindInterfacesAndSelfTo<UIController>()
                .FromNew()
                .AsSingle();

        Container
                .BindInterfacesAndSelfTo<ElementFinder>()
                .FromNew()
                .AsSingle();

        Container
                .Bind<SaveView>()
                .FromComponentInHierarchy()
                .AsSingle();

        Container
                .BindInterfacesAndSelfTo<SavePresenter>()
                .FromNew()
                .AsSingle();
    }
}