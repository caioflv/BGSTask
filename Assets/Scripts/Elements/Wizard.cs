using BGSTask;
using UnityEngine;

namespace BGSTask
{
    //An NPC who has the function of opening the item shop.

    public class Wizard : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            EventController.ToggleShop?.Invoke(true);
        }

        public void ToggleInteraction(bool mode)
        {
        }
    }
}

