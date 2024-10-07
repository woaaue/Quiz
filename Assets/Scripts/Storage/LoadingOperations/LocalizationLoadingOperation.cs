using Zenject;

public sealed class LocalizationLoadingOperation : Operation
{
    private const string PATH = "Localization";

    public override float Progress => _progress;

    private float _progress;
    [Inject] private readonly UserInfo _userInfo;

    protected override void OnBegin()
    {
        LocalizationProvider.SetLanguage(_userInfo.LanguageSettings.SaveLanguage);
        LocalizationProvider.InitializeAsync(PATH).ContinueWith(_ =>
        {
            _progress = 1f;
            SetStateDone();
        });
    }
}
