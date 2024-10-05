using Zenject;

public sealed class ProjectContext : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UserInfo>().AsSingle();
    }
}
