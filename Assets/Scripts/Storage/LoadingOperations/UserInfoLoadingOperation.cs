using Zenject;

public sealed class UserInfoLoadingOperation : Operation
{
    public override float Progress => _progress;

    [Inject] private UserInfo _userInfo;

    private float _progress;
    private DataLoad _dataLoad;

    protected override void OnBegin()
    {
        _dataLoad = new DataLoad();
        _dataLoad.Load<UserInfo>().ContinueWith(_ =>
        {
            _userInfo = _.Result;
            _userInfo.UserProfile.ChangeCountExecution();
            _progress = 1f;
            
            SetStateDone();
        }); 
    }
}