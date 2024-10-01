using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Quiz/ThemeSettings", fileName = "ThemeSettings", order = 1)]
public sealed class ThemeSettings : ScriptableObject
{
    [field: SerializeField] public ThemeType Type { get; private set; }
    [field: SerializeField] public LevelsSettings Levels { get; private set; }

#if UNITY_EDITOR

    [Button("FillLevelsQustions")]
    private void FillQuestions()
    {
        Levels.LevelsSetting.ForEach(levelSetting =>
        {
            if (levelSetting.QuestionSettings.Count == 0)
            {
                levelSetting.SetQuestionSettings(Type);
            }
        });
    }

#endif
}
