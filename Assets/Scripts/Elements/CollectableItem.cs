using BGSTask;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BGSTask
{
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] private Transform _self;
        [SerializeField] private Collider2D _collider;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Item _item;

        //The collectible item is dropped from the chest.
        //When the player is close to it, the CollectItem function will automatically be called.

        private void OnEnable()
        {
            _collider.enabled = false;
            Invoke(nameof(EnableCollision), 1);
        }

        private void EnableCollision()
        {
            _collider.enabled = true;
        }

        public void SetInfo(Item item)
        {
            _item = item;
            _spriteRenderer.sprite = Resources.Load<Sprite>($"Icons/{item.Type}/{item.Code}");
        }

        public void CollectItem()
        {
            _collider.enabled = false;

            _self.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutBack).OnComplete(() =>
            {
                EventController.CollectedItem?.Invoke(_item);

                Destroy(_self.gameObject);
            });
        }
    }

}

