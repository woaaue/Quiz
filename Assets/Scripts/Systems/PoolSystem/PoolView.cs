using UnityEngine;

public abstract class PoolView<T> : MonoBehaviour where T : MonoBehaviour
{
    public abstract int PoolCapacity { get; }

    [SerializeField] private Transform _parent;
    [SerializeField] private T _prefab;

    private protected PooledMonoBehaviourFactory<T> _factory;

    public void Init()
    {
        _factory = new PooledMonoBehaviourFactory<T>(_prefab, _parent, PoolCapacity);
    }

    public virtual T Get()
    {
        return _factory.Get();
    }

    public virtual void Release(T obj)
    {
        _factory.Release(obj);
    }

    public virtual void Dispose()
    {
        _factory.Dispose();
    }

    protected virtual void OnDestroy()
    {
        Dispose();
    }
}