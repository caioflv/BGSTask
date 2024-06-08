using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private Collider2D _collider;

    [SerializeField] private Animator _animator;
    [SerializeField] private float _refreshDelay;

    private WaitForSeconds _refreshTime;


    private void Awake()
    {
        _refreshTime = new WaitForSeconds(_refreshDelay);
    }

    public void Interact()
    {
        ToggleInteraction(false);

        _animator.Play("ChestOpen", 0, 0);

        StartCoroutine(RefreshChest());
    }

    public void ToggleInteraction(bool mode)
    {
        _collider.enabled = mode;
    }

    private IEnumerator RefreshChest()
    {
        yield return _refreshTime;

        _animator.Play("ChestIdle", 0, 0);

        ToggleInteraction(true);
    }
}
