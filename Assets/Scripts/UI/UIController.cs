using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BGSTask
{
    public class UIController : Controller
    {
        [SerializeField] private Transform _shopCamera;
        [SerializeField] private RectTransform _interactionIcon;

        public override void Init()
        {
            EventController.ToggleShop += ToggleShop;
            EventController.InteractableElement += ToggleInteractionIcon;
        }

        private void ToggleShop(bool mode)
        {
            _shopCamera.gameObject.SetActive(mode);
        }

        private void ToggleInteractionIcon(bool active, Vector2 position)
        {
            if (active != _interactionIcon.gameObject.activeSelf)
            {
                PopIconEffect(_interactionIcon, active);
            }

            if (active)
            {
                _interactionIcon.position = Camera.main.WorldToScreenPoint(position);
            }
        }

        private void PopIconEffect(RectTransform rect, bool mode)
        {
            rect.DOKill(true);

            rect.localScale = mode ? Vector3.zero : Vector3.one;
            rect.gameObject.SetActive(true);

            if (mode)
            {
                rect.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            }
            else
            {
                rect.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    rect.gameObject.SetActive(mode);
                });
            }
        }
    }

}
