public sealed class HidePopupEvent
{
    public readonly bool IsActive;

    public HidePopupEvent(bool isActive) 
    {
        IsActive = isActive;
    }
}
