using UnityEngine;
using Zenject;

public class InfrastructureInstaller : MonoInstaller
{
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
                .Bind<SaveGameComposite>()
                .FromComponentOn(gameObject)
                .AsSingle();

        Container
                .Bind<GameCycle>()
                .FromComponentInHierarchy(gameObject)
                .AsSingle();
    }
}