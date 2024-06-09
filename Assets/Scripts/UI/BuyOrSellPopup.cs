using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGSTask
{
    //Responsible for presenting a screen for purchasing and selling items.
    //The confirm and cancel buttons have a callback to inform the store of the choice made by the player

    public class BuyOrSellPopup : MonoBehaviour
    {
        [SerializeField] private RectTransform _self;
        [SerializeField] private RectTransform _card;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        [SerializeField] private TextMeshProUGUI _description;

        public Action<bool> ConfirmCallback;

        private void Awake()
        {
            _confirmButton.onClick.AddListener(() =>
            {
                ConfirmCallback?.Invoke(true);
                Toggle(false);
            });

            _cancelButton.onClick.AddListener(() =>
            {
                ConfirmCallback?.Invoke(false);
                Toggle(false);
            });

        }

        public void Toggle(bool mode, bool allowed = false, string description = null)
        {
            _description.text = description;
            _confirmButton.gameObject.SetActive(allowed);

            _card.DOKill(true);

            if (mode)
            {
                _self.gameObject.SetActive(mode);
                _card.gameObject.SetActive(mode);
                _card.DOScale(Vector3.one * 0.75f, 0);

                _card.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            }
            else
            {
                _card.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    _self.gameObject.SetActive(mode);
                    _card.gameObject.SetActive(mode);
                });
            }
        }
    }
}

