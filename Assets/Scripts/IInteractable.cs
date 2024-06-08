public interface IInteractable
{
    abstract void Interact();
    virtual void ToggleInteraction(bool mode) { }
}
