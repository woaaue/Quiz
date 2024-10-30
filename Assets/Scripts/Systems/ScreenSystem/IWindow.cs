using System;

public interface IWindow
{
    public void Show(Action callback);
    public void Hide();
}
