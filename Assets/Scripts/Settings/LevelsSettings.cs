using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/LevelsSettings", fileName = "LevelsSettings", order = 1)]
public sealed class LevelsSettings : ScriptableObject
{
    [field: SerializeField] public List<LevelSettings> LevelsSetting { get; private set; }

#if UNITY_EDITOR

    [Button("Generate identifiers for levels")]
    public void GenerateId()
    {
        LevelsSetting.ForEach(levelSetting =>
        {
            if (string.IsNullOrEmpty(levelSetting.Id))
            {
                levelSetting.SetId();
            }
        });
    }

#endif 
}
