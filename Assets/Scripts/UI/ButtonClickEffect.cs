using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEffect : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImage;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            _iconImage.DOKill(true);
            _iconImage.GetComponent<RectTransform>().DOScale(Vector3.one , 0);
            _iconImage.GetComponent<RectTransform>().DOPunchScale(Vector3.one * -0.3f, 0.2f, 2, 1);
        });
    }
}

