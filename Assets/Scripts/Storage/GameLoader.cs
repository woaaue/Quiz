using System;
using UnityEngine;
using System.Linq;
using System.Collections;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public sealed class GameLoader : MonoBehaviour
{
    [SerializeField] private List<OperationPack> _packs;

    [UsedImplicitly]
    public void LoadGame()
    {
        StartCoroutine(LoadGameRoutine());
    }

    private void Awake()
    {

#if UNITY_EDITOR

        Application.targetFrameRate = 60;

#endif

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

            yield return null;

            while (!pack.IsDone)
            {
                yield return new WaitForEndOfFrame();
            }

            EventSystem.Invoke(new ProgressLoadSignal(CheckCurrentProgress()));
        }

        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single); //TO DO: edit this, cringe
    }

    private float CheckCurrentProgress()
    {
        return _packs.Count(pack => pack.Progress == 1f) / (float)_packs.Count;
    }
}
