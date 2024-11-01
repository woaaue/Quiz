using Zenject;

public sealed class GameSceneContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PoolService>().AsSingle();
    }
}
