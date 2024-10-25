using UnityEngine;

public sealed class ThemePool : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private Theme _prefab;
    [SerializeField] private bool _isAutoExpand;

    private PoolObject<Theme> _pool;

    public Theme GetElement() => _pool.GetFreeElement();

    private void Start()
    {
        _pool = new PoolObject<Theme>(_prefab, gameObject.transform, _size, _isAutoExpand);
    }
}
