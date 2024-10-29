using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class Purchase : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _coinIcon;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private GameObject _freePurchase;
    [SerializeField] private TextMeshProUGUI _countMoney;
    [SerializeField] private TextMeshProUGUI _description;

    private string _id;

    public void Setup(PurchaseSetting setting)
    {
        if (setting.PurchaseType == PurchaseType.Ad)
        {
            _price.gameObject.SetActive(false);
            _freePurchase.gameObject.SetActive(true);
        }

        if (setting.RewardType == RewardType.AdRemove)
        {
            _coinIcon.gameObject.SetActive(false);
            _countMoney.gameObject.SetActive(false);
            _description.gameObject.SetActive(true);

            _description.text = LocalizationProvider.GetText(LocalizationItemType.UI, setting.Id);
        }

        _id = setting.Id;
        _icon.sprite = setting.Icon;
        _price.text = setting.Price.ToString();     
        _countMoney.text = setting.Value.ToString();
    }
}
