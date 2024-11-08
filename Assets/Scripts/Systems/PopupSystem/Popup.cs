using System;

public class Popup<T> : PopupBase where T : PopupBaseSettings
{
    public override void Setup(PopupBaseSettings settings)
    {
        if (settings is T typedSettings)
        {
            Setup(typedSettings);
        }
        else
        {
            throw new Exception($"Incorrect settings type provided {typeof(T)}");
        }
    }

    public virtual void Setup(T settings)
    {
        
    }
}
