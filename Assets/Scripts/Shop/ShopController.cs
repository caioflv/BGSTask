using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGSTask
{
    //Responsible for carrying all items available to the player, in addition to intermediating the purchase and sale of items

    public class ShopController : Controller
    {
        [SerializeField] private RectTransform _self;

        [SerializeField] private Transform _itemContent;
        [SerializeField] private Transform _categorieScreen;

        [SerializeField] private BuyOrSellPopup _buyOrSellPopup;

        [SerializeField] private TextMeshProUGUI _coinCount;

        private string _selectedCategorie = "Hood";
        private Item _selectedItem;

        public override void Init()
        {
            EventController.ToggleShop += OnToggle;

            RefreshCoinCount();
        }

        public void OnToggle(bool mode)
        {
            _self.DOKill(true);

            if (mode)
            {
                OpenWardrop(_selectedCategorie);

                _self.gameObject.SetActive(true);
                _self.DOAnchorPosX(-960, 0.3f).SetEase(Ease.OutBack);
            }
            else
            {
                _self.DOAnchorPosX(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    _self.gameObject.SetActive(mode);
                });
            }
        }

        #region Wardrobe
        public void OpenWardrop(string categorie)
        {
            _selectedCategorie = categorie;

            ToggleCategorieScreen(true);

            RefreshItemContent();

            int count = 0;
            foreach (var item in GameControllers.Instance.ItemController.ItemDictionary[_selectedCategorie])
            {
                Button t = _itemContent.GetChild(count).GetComponent<Button>();

                bool itemOwned = GameControllers.Instance.InventoryController.ContainsItem(item);

                t.GetComponent<ShopButton>().SetInfo(item, itemOwned, !itemOwned);

                t.onClick.RemoveAllListeners();
                t.onClick.AddListener(() => 
                {
                    if (!itemOwned)
                    {
                        OpenBuyPopUp(item);
                        return;
                    }

                    EventController.SetPlayerSkinPart(item.Type, item.Code);
                });

                t.gameObject.SetActive(true);

                count++;
            }
        }

        private void OpenBuyPopUp(Item item)
        {
            _selectedItem = item;

            float playerTotalCoin = GameControllers.Instance.InventoryController.GetTotalCoin();

            string desc = playerTotalCoin < item.Price ? 
             $"{item.Name}\nYou need {item.Price} coin(s) to buy." : $"Buy {item.Name} for {item.Price} ?";

            _buyOrSellPopup.Toggle(true, playerTotalCoin >= item.Price, desc);
            
            _buyOrSellPopup.ConfirmCallback += TryToBuyItem;
        }

        private void TryToBuyItem(bool confirm)
        {
            _buyOrSellPopup.ConfirmCallback -= TryToBuyItem;

            if (!confirm)
                return;

            float coin = GameControllers.Instance.InventoryController.GetTotalCoin();
            if (coin >= _selectedItem.Price)
            {
                EventController.ItemBought?.Invoke(_selectedItem);
                RefreshCoinCount();
                OpenWardrop(_selectedCategorie);
            }
        }
        #endregion

        #region Sell
        public void OpenSell()
        {
            ToggleCategorieScreen(false);

            RefreshItemContent();

            if (!GameControllers.Instance.InventoryController.Items.ContainsKey("Collectable"))
                return;

            int count = 0;
            foreach (var item in GameControllers.Instance.InventoryController.Items["Collectable"])
            {
                Button t = _itemContent.GetChild(count).GetComponent<Button>();

                t.GetComponent<ShopButton>().SetInfo(item, true, true);

                t.onClick.RemoveAllListeners();
                t.onClick.AddListener(() =>
                {
                    OpenSellPopUp(item);
                });

                t.gameObject.SetActive(true);

                count++;
            }
        }

        private void OpenSellPopUp(Item item)
        {
            _selectedItem = item;

            string desc = $"Sell {item.Name} for {item.Price} ?";

            _buyOrSellPopup.Toggle(true, true, desc);

            _buyOrSellPopup.ConfirmCallback += TryToSellItem;
        }

        private void TryToSellItem(bool confirm)
        {
            _buyOrSellPopup.ConfirmCallback -= TryToSellItem;

            if (!confirm)
                return;

            EventController.ItemSold?.Invoke(_selectedItem);
            RefreshCoinCount();
            OpenSell();
        }
        #endregion

        private void RefreshItemContent()
        {
            foreach (Transform item in _itemContent)
            {
                item.gameObject.SetActive(false);
            }
        }
        private void RefreshCoinCount()
        {
            _coinCount.text = GameControllers.Instance.InventoryController.GetTotalCoin().ToString();
        }

        private void ToggleCategorieScreen(bool mode)
        {
            _categorieScreen.DOKill(true);
            _categorieScreen.DOMoveX(mode ? 1070 : 1280, 0.3f).SetEase(mode ? Ease.OutBack : Ease.InBack);
        }
    }
}

