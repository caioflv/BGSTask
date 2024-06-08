using System.Linq;
using UnityEngine;

partial class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _interactionPivot;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private float _interactionRadius = 2;
    private bool _isInteracting;

    public IInteractable _selectedInteractable;

    private void Refresh(Collider2D collider)
    {
        if (!collider && !_isInteracting)
            return;

        Vector2 position = collider ? collider.transform.position : Vector2.zero;
        _selectedInteractable = collider ? collider.GetComponent<IInteractable>() : null;

        _isInteracting = _selectedInteractable != null;

        EventController.InteractableElement?.Invoke(_isInteracting, position);
    }

    private void CheckNearElements()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_interactionPivot.position, _interactionRadius, _interactionLayer);
        Collider2D target = null;

        if (colliders.Length > 0)
        {
            colliders.OrderBy(x => (x.transform.position - _interactionPivot.position).magnitude);
            target = colliders[0];
        }

        Refresh(target);
    }
}
