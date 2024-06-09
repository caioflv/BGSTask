using BGSTask;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private Collider2D _collider;

    [SerializeField] private Animator _animator;
    [SerializeField] private float _refreshDelay;

    private WaitForSeconds _refreshTime;

    //The chest is an interactable element. Its function is to provide the player with collectable items.
    //After a period of time, it can be opened again.

    private void Awake()
    {
        _refreshTime = new WaitForSeconds(_refreshDelay);

        _animator.Play("ChestIdle", 0, UnityEngine.Random.Range(0f, 1f));
    }

    public void Interact()
    {
        ToggleInteraction(false);

        _animator.Play("ChestOpen", 0, 0);

        EventController.SpawnCollectableItem?.Invoke(2, transform.position);

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
