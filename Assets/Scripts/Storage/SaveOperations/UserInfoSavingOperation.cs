using Zenject;

public sealed class UserInfoSavingOperation : Operation
{
    public override float Progress => _progress;

    private float _progress;
    private DataSave _dataSave;
    [Inject] private UserInfo _userInfo;

    protected override void OnBegin()
    {
        _dataSave = new DataSave();

        _dataSave.Save(_userInfo).ContinueWith(_ =>
        {
            _progress = 1f;
            SetStateDone();
        });
    }
}
