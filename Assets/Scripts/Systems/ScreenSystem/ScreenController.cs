using UnityEngine;
using JetBrains.Annotations;

public sealed class ScreenController : MonoBehaviour
{
    [SerializeField] private Screen _startScreen;

    private bool _isSwitching;
    private Screen _currentScreen;
    private Screen _previousScreen;

    [UsedImplicitly]
    public void SwitchScreen(Screen newScreen)
    {
        if (_isSwitching)
            return;

        if (_currentScreen != newScreen)
        {
            _isSwitching = true;

            _previousScreen = _currentScreen;
            _currentScreen = newScreen;

            _currentScreen.transform.SetAsLastSibling();
            _currentScreen.Show(OnScreenShown);
        }
    }

    private void Start()
    {
        _currentScreen = _startScreen;
    }

    private void OnScreenShown()
    {
        _previousScreen.Hide();
        _isSwitching = false;
    }
}
