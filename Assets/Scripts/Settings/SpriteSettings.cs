using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/SpriteSettings", fileName = "SpriteSettings", order = 1)]
public sealed class SpriteSettings : ScriptableObject
{
    [field: SerializeField] public List<Sprite> Sprites { get; private set; }

    public Sprite GetThemeSprite(ThemeType themeType) => Sprites.First(sprite => sprite.name == themeType.ToString());
}
