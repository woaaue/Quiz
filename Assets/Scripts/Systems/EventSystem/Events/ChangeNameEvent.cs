public sealed class ChangeNameEvent
{ 
    public readonly string Name;

    public ChangeNameEvent(string text)
    {
        Name = text;
    }
}
