using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

partial class PlayerController : MonoBehaviour
{
    private Vector2 _direction;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _skin;

    //Input System
    private InputActions _actions;

    private void Awake()
    {
        _actions = new InputActions();

        _actions.Player.Movement.performed += OnMovementAction;
        _actions.Player.Movement.canceled += OnMovementAction;
        _actions.Player.Interaction.performed += OnInteractionAction;

        _actions.Enable();

        EventController.TogglePlayerController += OnTogglePlayerController;
    }

    private void OnTogglePlayerController(bool mode)
    {
        this.enabled = mode;

        if (mode)
        {
            _actions.Enable();
        }
        else
        {
            _actions.Disable();
            EventController.InteractableElement?.Invoke(false, Vector2.zero);
        }
    }

    private void OnMovementAction(UnityEngine.InputSystem.InputAction.CallbackContext value)
    {
        _direction = value.ReadValue<Vector2>();

        _animator.SetBool("Walk", _direction != Vector2.zero);

        _skin.localScale = new Vector3(_direction.x == 0 ? _skin.localScale.x : _direction.x > 0 ? 1 : -1, 1, 1);
    }

    private void OnInteractionAction(UnityEngine.InputSystem.InputAction.CallbackContext value)
    {
        _selectedInteractable?.Interact();
    }

    private void FixedUpdate()
    {
        //Player Movement
        _rb.MovePosition((Vector2)transform.position + _direction * _speed * Time.fixedDeltaTime);

        //Interactable Controller
        CheckNearElements();
    }
}
