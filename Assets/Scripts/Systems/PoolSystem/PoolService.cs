using System;
using Zenject;
using UnityEngine;

public interface IPoolService
{
    public T Get<T>() where T : MonoBehaviour;
    public void Release<T>(T obj);
}

public sealed class PoolService : IPoolService
{
    private ThemePoolView _themePool;
    private PurchasePoolView _purchasePool;
    private LanguagePoolView _languagePool;

    [Inject]
    public void Construct(ThemePoolView themePool, PurchasePoolView purchasePool, LanguagePoolView languagePool)
    {
        _themePool = themePool;
        _purchasePool = purchasePool;
        _languagePool = languagePool;

        Initialize();
    }

    public T Get<T>() where T : MonoBehaviour
    {
        var type = typeof(T);

        return type switch
        {
            _ when type == typeof(Theme) => _themePool as T,
            _ when type == typeof(Purchase) => _purchasePool.Get() as T,
            _ when type == typeof(Language) => _languagePool.Get() as T,
            _ => throw new ArgumentException($"Unsupported type: {type}")
        };
    }

    public void Release<T>(T obj)
    {
        switch (obj)
        {
            case Theme theme:
                _themePool.Release(theme);
                break;

            case Purchase purchase:
                _purchasePool.Release(purchase);
                break;

            default:
                throw new ArgumentException($"Unsupported type: {typeof(T)}");
        }
    }

    private void Initialize()
    {
        _themePool.Init();
        _purchasePool.Init();
        _languagePool.Init();
    }
}
