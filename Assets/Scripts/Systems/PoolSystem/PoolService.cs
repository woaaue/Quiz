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
    private LevelPoolView _levelPool;
    private LevelsPoolView _levelsPool;
    private PurchasePoolView _purchasePool;
    private RankSelectorPoolView _rankSelectorPool;

    [Inject]
    public void Construct(ThemePoolView themePool, PurchasePoolView purchasePool, RankSelectorPoolView rankSelectorPool, LevelsPoolView levelsPoolView, LevelPoolView levelPoolView)
    {
        _themePool = themePool;
        _purchasePool = purchasePool;
        _rankSelectorPool = rankSelectorPool;
        _levelPool = levelPoolView;
        _levelsPool = levelsPoolView;

        Initialize();
    }

    public T Get<T>() where T : MonoBehaviour
    {
        var type = typeof(T);

        return type switch
        {
            _ when type == typeof(Theme) => _themePool.Get() as T,
            _ when type == typeof(LevelView) => _levelPool.Get() as T,
            _ when type == typeof(Purchase) => _purchasePool.Get() as T,
            _ when type == typeof(RankLevelsView) => _levelsPool.Get() as T,
            _ when type == typeof(RankSelector) => _rankSelectorPool.Get() as T,
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

            case RankSelector selector:
                _rankSelectorPool.Release(selector);
                break;

            case LevelView levelView:
                _levelPool.Release(levelView);
                break;

            case RankLevelsView rankLevelsView:
                _levelsPool.Release(rankLevelsView);
                break;

            default:
                throw new ArgumentException($"Unsupported type: {typeof(T)}");
        }
    }

    private void Initialize()
    {
        _themePool.Init();
        _purchasePool.Init();
        _rankSelectorPool.Init();
        _levelsPool.Init();
        _levelPool.Init();
    }
}
