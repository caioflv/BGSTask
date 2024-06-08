using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [SerializeField] private RectTransform _interactionIcon;

    private void Awake()
    {
        EventController.InteractableElement += ToggleInteractionIcon;
    }

    private void ToggleInteractionIcon(bool active, Vector2 position)
    {
        if (active != _interactionIcon.gameObject.activeSelf)
        {
            PopEffect(_interactionIcon, active);
        }

        if (active)
        {
            _interactionIcon.position = Camera.main.WorldToScreenPoint(position);
        }
    }

    private void PopEffect(RectTransform rect, bool mode)
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
