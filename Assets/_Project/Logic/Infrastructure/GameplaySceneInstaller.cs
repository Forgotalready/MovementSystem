using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject _player;

    public override void InstallBindings()
    {
        // TODO() Придумать куда вставить блокировку курсора.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Container
                .Bind<ControllersEventBus>()
                .FromNew()
                .AsSingle();

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