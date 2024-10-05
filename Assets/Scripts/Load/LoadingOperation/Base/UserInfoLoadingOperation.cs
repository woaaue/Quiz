public sealed class UserInfoLoadingOperation : LoadingOperation
{
    public override float Progress => _progress;

    private float _progress;

    protected override void OnBegin()
    {
        throw new System.NotImplementedException();
    }
}
