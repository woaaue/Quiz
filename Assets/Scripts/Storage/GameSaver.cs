using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public sealed class GameSaver : MonoBehaviour
{
    [SerializeField] private List<OperationPack> _packs;

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            StartCoroutine(SaveGameRoutine());
        }
        else
        {
            StopCoroutine(SaveGameRoutine());
        }
    }

    private IEnumerator SaveGameRoutine()
    {
        if (_packs == null || !_packs.Any())
        {
            yield break;
        }

        foreach (var pack in _packs) 
        {
            pack.Begin();

            while (!pack.IsDone)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
