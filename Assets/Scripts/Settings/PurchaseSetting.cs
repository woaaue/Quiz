using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/PurchaseSetting", fileName = "PurchaseSetting", order = 1)]
public sealed class PurchaseSetting : ScriptableObject
{
    [field: SerializeField] public PurchaseData PurchaseData { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}

[Serializable]
public sealed class PurchaseData
{
    public string Id;
    public float Price;
    public PurchaseReward Reward;
    public PurchaseType PurchaseType;
    public PurchaseMethodType PurchaseMethodType;
}

[Serializable]
public sealed class PurchaseReward
{
    public float Value;
    public RewardType Type;
}
