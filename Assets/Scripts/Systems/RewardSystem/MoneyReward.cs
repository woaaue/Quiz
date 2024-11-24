using Zenject;

public sealed class MoneyReward : RewardBase
{
    [Inject] private UserInfo _userInfo;

    public override void GetReward(int value)
    {
        _userInfo.UserData.IncreaseValue(value);
    }
}
