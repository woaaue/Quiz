using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class Purchase : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private GameObject _freePurchase;
    [SerializeField] private TextMeshProUGUI _countMoney;

    private string _id;

    public void Setup(PurchaseSetting setting)
    {
        _id = setting.PurchaseData.Id;
        _icon.sprite = setting.Icon;
        _price.text = setting.PurchaseData.Price.ToString();
        _countMoney.text = setting.PurchaseData.Reward.Value.ToString();

        if (setting.PurchaseData.PurchaseMethodType == PurchaseMethodType.WatchAd)
        {
            _price.gameObject.SetActive(false);
            _freePurchase.gameObject.SetActive(true);
        }

        if (setting.PurchaseData.Reward.Type == RewardType.AdRemove)
        {
            _countMoney.text = LocalizationProvider.GetText(LocalizationItemType.UI, setting.PurchaseData.Id);
        }
    }
}
