using System;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public sealed class PoolObject<T> where T : MonoBehaviour
{   
    private T _prefab;
    private List<T> _pool;
    private bool _isAutoExpand;
    private Transform _container;

    public PoolObject(T prefab, Transform container, int poolSize, bool isAutoExpand = false)
    {
        _prefab = prefab;
        _container = container;
        _isAutoExpand = isAutoExpand;

        CreatePool(poolSize);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateElement();
        }
    }

    private T CreateElement(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(_prefab, _container);

        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);

        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);

                return true;
            }
        }

        element = null;

        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }

        if (_isAutoExpand)
        {
            return CreateElement(true);
        }

        throw new Exception($"There are no free items in the type pool: {typeof(T)}, auto-expansion for this pool has been switched off");
    }
}
