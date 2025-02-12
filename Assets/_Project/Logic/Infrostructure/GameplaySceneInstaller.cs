using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
                .BindInterfacesAndSelfTo<MovementController>()
                .FromNew()
                .AsSingle();
    }
}