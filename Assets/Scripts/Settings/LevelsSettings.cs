using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/LevelsSettings", fileName = "LevelsSettings", order = 1)]
public sealed class LevelsSettings : ScriptableObject
{
    [field: SerializeField] public List<LevelSettings> LevelsSetting { get; private set; }

#if UNITY_EDITOR

    public void FillSettings()
    {
        var counter = 0;

        LevelsSetting.ForEach(levelSetting =>
        {
            if (string.IsNullOrEmpty(levelSetting.Id))
            {
                levelSetting.SetIdAndNumber(++counter);
            }
        });
    }

#endif 
}
