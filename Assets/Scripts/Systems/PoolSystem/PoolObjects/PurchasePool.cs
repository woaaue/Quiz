using UnityEngine;

public sealed class PurchasePool : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private Purchase _prefab;
    [SerializeField] private bool _isAutoExpand;

    private PoolObject<Purchase> _pool;

    public Purchase GetElement() => _pool.GetFreeElement();

    private void Start()
    {
        _pool = new PoolObject<Purchase>(_prefab, gameObject.transform, _size, _isAutoExpand);
    }
}
