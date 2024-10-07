using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public sealed class OperationPack : Operation
{
    public override float Progress => _progress;

    [SerializeField] private ExecutionMethod _executionMethod;
    [SerializeField] private List<Operation> _operations;

    private float _progress;

    protected override void OnBegin()
    {
        StartCoroutine(_executionMethod switch
        { 
            ExecutionMethod.Parallel => ParallelExecutionRoutine(),
            ExecutionMethod.Sequently => SequentlyExecutionRoutine(),
            _ => null
        });
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator ParallelExecutionRoutine()
    {
        var operationCount = _operations.Count;
        _operations.ForEach(operation => operation.Begin());

        while (_operations.Any(operation => !operation.IsDone))
        {
            _progress = _operations.Sum(operation => operation.Progress) / operationCount;
            yield return new WaitForEndOfFrame();
        }

        _progress = 1f;
        SetStateDone();

        yield break;
    }

    private IEnumerator SequentlyExecutionRoutine()
    {
        var operationCount = _operations.Count;
        var completedOperationsCount = 0;

        foreach (var operation in _operations)
        {
            operation.Begin();

            while (!operation.IsDone)
            {
                _progress = (operation.Progress + completedOperationsCount) /operationCount;
                yield return new WaitForEndOfFrame();
            }

            ++completedOperationsCount;
        }

        _progress = 1f;

        yield break;
    }
}
