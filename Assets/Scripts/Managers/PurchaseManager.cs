using UnityEngine;

public sealed class PurchaseManager : MonoBehaviour
{
    [SerializeField] private PurchasePool _pool;
    [SerializeField] private Transform _conteiner;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var purchaseSettings = SettingsProvider.Get<PurchaseSettings>().Settings;

        foreach (var purchaseSetting in purchaseSettings)
        {
            var purchase = _pool.GetElement();
            purchase.transform.SetParent(_conteiner, false);

            purchase.Setup(purchaseSetting);
        }
    }
}
