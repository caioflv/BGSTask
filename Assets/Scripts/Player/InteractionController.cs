using System.Linq;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private float _interactionRadius = 2;
    private bool _active;

    public IInteractable SelectedInteractable;

    private void Refresh(Collider2D collider)
    {
        if (!collider && !_active)
            return;

        Vector2 position = collider ? collider.transform.position : Vector2.zero;
        SelectedInteractable = collider ? collider.GetComponent<IInteractable>() : null;

        _active = SelectedInteractable != null;

        EventController.InteractableElement?.Invoke(_active, position);
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_pivot.position, _interactionRadius, _collisionLayer);
        Collider2D target = null;

        if (colliders.Length > 0)
        {
            colliders.OrderBy(x => (x.transform.position - _pivot.position).magnitude);
            target = colliders[0];
        }

        Refresh(target);
    }
}
