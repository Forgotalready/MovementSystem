using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerConfig _playerConfig;

    public override void InstallBindings()
    {
        Container
                .BindInterfacesAndSelfTo<MovementController>()
                .FromNew()
                .AsSingle();

        Container
                .Bind<Inventory>()
                .FromNew()
                .AsSingle()
                .WithArguments(_player);

        Container
                .Bind<PlayerConfig>()
                .FromInstance(_playerConfig)
                .AsSingle();

        Container
                .BindInterfacesTo<CameraRotator>()
                .FromComponentOn(_player)
                .AsCached();

        Container
                .BindInterfacesTo<DetectionComponent>()
                .FromComponentOn(_player)
                .AsCached();

        Container
                .BindInterfacesTo<PlayerMovement>()
                .FromComponentOn(_player)
                .AsCached();

        Container
                .BindInterfacesTo<PlayerSaveComponent>()
                .FromComponentOn(_player)
                .AsCached();
    }
}