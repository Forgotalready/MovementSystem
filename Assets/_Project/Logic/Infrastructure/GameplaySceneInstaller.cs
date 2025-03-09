using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject _player;

    public override void InstallBindings()
    {
        Container
                .BindInterfacesAndSelfTo<MovementController>()
                .FromNew()
                .AsSingle();

        Container
                .BindInterfacesAndSelfTo<UIController>()
                .FromNew()
                .AsSingle();

        Container
                .BindInterfacesAndSelfTo<ElementFinder>()
                .FromNew()
                .AsSingle();

        Container
                .Bind<Inventory>()
                .FromNew()
                .AsSingle()
                .WithArguments(_player);
    }
}