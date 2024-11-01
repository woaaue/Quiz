using UnityEngine;
using UnityEngine.Pool;

public class PooledMonoBehaviourFactory<T> where T : MonoBehaviour
{
    private readonly ObjectPool<T> _pool;
    private protected Transform _parent;

    public PooledMonoBehaviourFactory(T prefab, Transform parent, int capacity)
    {
        _parent = parent;

        _pool = new ObjectPool<T>(() =>
        {
            return Object.Instantiate(prefab, _parent);
        }, actionOnRelease: instance =>
        {
            instance.gameObject.SetActive(false);
        }, actionOnDestroy: instance =>
        {
            Object.Destroy(instance.gameObject);
        }, defaultCapacity: capacity);
    }

    public virtual T Get()
    {
        return _pool.Get();
    }

    public virtual void Release(T obj)
    {
        _pool.Release(obj);
    }

    public void Dispose()
    {
        _pool.Dispose();
    }
}
