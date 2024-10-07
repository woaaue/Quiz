using Zenject;

public sealed class UserInfoLoadingOperation : Operation
{
    public override float Progress => _progress;

    private float _progress;
    private DataLoad _dataLoad;
    [Inject] UserInfo _userInfo;

    protected override void OnBegin()
    {
        _dataLoad = new DataLoad();
        _dataLoad.Load<UserInfo>().ContinueWith(_ =>
        {
            _userInfo = _.Result;
            _progress = 1f;
            
            SetStateDone();
        }); 
    }
}