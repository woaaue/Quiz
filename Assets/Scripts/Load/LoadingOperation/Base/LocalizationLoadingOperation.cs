public sealed class LocalizationLoadingOperation : LoadingOperation
{
    public override float Progress => _progress;

    private float _progress;
    private const string PATH = "Localization";

    protected override void OnBegin()
    {
        LocalizationProvider.InitializeAsync(PATH).ContinueWith(_ =>
        {
            _progress = 1f;
            SetStateDone();
        });
    }
}
