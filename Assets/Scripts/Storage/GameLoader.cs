using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public sealed class GameLoader : MonoBehaviour
{
    [SerializeField] private List<OperationPack> _packs;

    public event Action<float> ProgressChanged;

    private void Awake()
    {
#if UNITY_EDITOR

        Application.targetFrameRate = 60;

#endif

        StartCoroutine(LoadGameRoutine());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator LoadGameRoutine()
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

            ProgressChanged?.Invoke(CheckCurrentProgress());
        }

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single); //TO DO: edit this, cringe
    }

    private float CheckCurrentProgress()
    {
        return _packs.Count(pack => pack.Progress == 1f) / (float)_packs.Count;
    }
}
