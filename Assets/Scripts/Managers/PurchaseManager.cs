using Zenject;
using UnityEngine;

public sealed class PurchaseManager : MonoBehaviour
{
    [SerializeField] private Transform _conteiner;

    private PoolService _poolService;

    [Inject]
    public void Construct(PoolService poolService)
    {
        _poolService = poolService;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var purchaseSettings = SettingsProvider.Get<PurchaseSettings>().Settings;

        foreach (var purchaseSetting in purchaseSettings)
        {
            var purchase = _poolService.Get<Purchase>();
            purchase.transform.SetParent(_conteiner, false);

            purchase.Setup(purchaseSetting);
        }
    }
}
