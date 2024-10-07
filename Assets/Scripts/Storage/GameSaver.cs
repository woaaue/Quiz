using UnityEngine;
using System.Collections.Generic;

public sealed class GameSaver : MonoBehaviour
{
    [SerializeField] private List<Operation> _operations;

    private void OnApplicationFocus(bool isFocus)
    {
        if (!isFocus)
        {
            SaveGame();
        }
    }

    private void SaveGame()
    {
        _operations.ForEach(operation =>
        {
            operation.Begin();
        });
    }

    //TO DO: add asynchronous saves in the future if needed
}
