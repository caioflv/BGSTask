public interface IInteractable
{
    //Interface that provides the ability to receive interaction from the player.
    //Used by NPCs and items.

    abstract void Interact();
    abstract void ToggleInteraction(bool mode);
}
