using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGSTask
{
    //Button used to display wardrobe items, items to be sold and category buttons.

    public partial class ShopButton : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Image _padlockImage;
        [SerializeField] private RectTransform _pricePivot;
        [SerializeField] private TextMeshProUGUI _priceText;

        public void SetInfo(Item item, bool itemOwned, bool showPrice)
        {
            _iconImage.sprite = Resources.Load<Sprite>($"Icons/{item.Type}/{item.Code}");
            _padlockImage.enabled = !itemOwned;
            _pricePivot.gameObject.SetActive(showPrice);
            _priceText.text = item.Price.ToString();
        }
    }
}

