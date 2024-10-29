using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/PurchaseSetting", fileName = "PurchaseSetting", order = 1)]
public sealed class PurchaseSetting : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public PurchaseType PurchaseType { get; private set; }
    [field: SerializeField] public RewardType RewardType { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
    [field: SerializeField] public float Price { get; private set; }
}
