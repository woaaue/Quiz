using UnityEngine;

public abstract class Operation : MonoBehaviour
{
    public abstract float Progress { get; }
    public bool IsDone { get; private set; }

    public void Begin()
    {
        OnBegin();
    }

    public void SetStateDone()
    {
        IsDone = true;
    }

    protected abstract void OnBegin();
}
