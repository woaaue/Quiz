using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quiz/PurchaseSettings", fileName = "PurchaseSettings", order = 1)]
public sealed class PurchaseSettings : ScriptableObject
{
    [field: SerializeField] public List<PurchaseSetting> Settings { get; private set; }

    public List<PurchaseSetting> GetPurchasesForType(PurchaseType purchaseType) => Settings.Where(purchaseSetting => purchaseSetting.PurchaseType == purchaseType).ToList();
}
