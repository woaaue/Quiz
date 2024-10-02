using System;
using System.Collections.Generic;

[Serializable]
public sealed class LocalizationItem
{
    public string Key;
    public string Text;
    public List<string> Tags;
}
