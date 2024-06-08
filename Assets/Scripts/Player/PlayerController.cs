using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    [SerializeField] private InteractionController _interactionController;

    //Input System
    private InputActions _actions;

    private void Awake()
    {
        _actions = new InputActions();

        _actions.Player.Movement.performed += MovementAction;
        _actions.Player.Movement.canceled += MovementAction;
        _actions.Player.Interaction.performed += InteractionAction;

        _actions.Enable();
    }

    private void MovementAction(UnityEngine.InputSystem.InputAction.CallbackContext value)
    {
        _direction = value.ReadValue<Vector2>();
    }

    private void InteractionAction(UnityEngine.InputSystem.InputAction.CallbackContext value)
    {
        _interactionController.SelectedInteractable?.Interact();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition((Vector2)transform.position + _direction * _speed * Time.fixedDeltaTime);
    }
}
